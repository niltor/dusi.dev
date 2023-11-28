using System.Security.Policy;
using System.Text.RegularExpressions;
using Application.Services;
using Share.Models.EntityModelDtos;

namespace Application.Manager;

public class EntityModelManager : DomainManagerBase<EntityModel, EntityModelUpdateDto, EntityModelFilterDto, EntityModelItemDto>, IEntityModelManager
{
    private readonly IEntityLibraryManager _libraryManager;
    private readonly OpenAIClient openAIClient;

    public EntityModelManager(DataStoreContext storeContext, IEntityLibraryManager libraryManager, OpenAIClient openAIClient) : base(storeContext)
    {
        _libraryManager = libraryManager;
        this.openAIClient = openAIClient;
    }

    public override async Task<EntityModel> UpdateAsync(EntityModel entity, EntityModelUpdateDto dto)
    {
        if (dto.EntityLibraryId != null)
        {
            EntityLibrary? lib = await _libraryManager.GetCurrentAsync(dto.EntityLibraryId.Value);
            if (lib != null)
            {
                entity.EntityLibrary = lib;
            }
        }
        return await base.UpdateAsync(entity, dto);
    }

    public override async Task<PageList<EntityModelItemDto>> FilterAsync(EntityModelFilterDto filter)
    {
        Queryable = Queryable.WhereNotNull(filter.Name, q => q.Name.Contains(filter.Name!) || q.Comment.Contains(filter.Name!));
        Queryable = Queryable.WhereNotNull(filter.CodeLanguage, q => q.CodeLanguage == filter.CodeLanguage!);
        Queryable = Queryable.WhereNotNull(filter.LanguageVersion, q => q.LanguageVersion == filter.LanguageVersion!);
        Queryable = Queryable.WhereNotNull(filter.EntityLibraryId, q => q.EntityLibrary.Id == filter.EntityLibraryId!);

        return await Query.FilterAsync<EntityModelItemDto>(Queryable);
    }

    public override async Task<EntityModel?> FindAsync(Guid id)
    {
        return await Query.Db.Include(e => e.EntityLibrary)
            .SingleOrDefaultAsync(e => e.Id == id);
    }


    /// <summary>
    /// ����ʵ��
    /// </summary>
    /// <param name="entityName">ʵ������</param>
    /// <param name="description">ʵ������</param>
    public async Task<List<string>?> GenerateEntityAsync(string entityName, string? description = null)
    {
        var res = new List<string>();
        // �ȴӿ�������
        var entity = Query.Db.FirstOrDefault(e => e.Comment.Contains(entityName));
        if (entity == null)
        {
            var data = await openAIClient.GetEntityAsync(entityName, description);
            res = data?.Select(d => d.Message.Content).ToList();
        }
        else
        {
            res.Add(entity.CodeContent!);
        }

        var contents = new List<string>();
        string pattern = @"(?<=```\s*).*?(?=```)";
        res?.ForEach(msg =>
        {
            Match match = Regex.Match(msg, pattern, RegexOptions.Singleline);
            if (match.Success)
            {

                if (match.Value.StartsWith("csharp"))
                {
                    contents.Add(match.Value.Replace("csharp", ""));
                }
                else
                {
                    contents.Add(match.Value);
                }
            }
        });
        return contents;
    }
}
