using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities.CMS;
using Core.Entities.EntityDesign;

namespace Core.Entities;
/// <summary>
/// 用户账户
/// </summary>
[NgPage("system", "member")]
public class User : EntityBase
{

    /// <summary>
    /// 用户名
    /// </summary>
    [MaxLength(40)]
    public required string UserName { get; set; }
    [MaxLength(100)]
    public string? Email { get; set; } = null!;
    public bool EmailConfirmed { get; set; } = false;
    [MaxLength(100)]
    public string PasswordHash { get; set; } = default!;
    [MaxLength(60)]
    public string PasswordSalt { get; set; } = default!;

    public List<EntityLibrary>? EntityLibraries { get; set; }
    public List<EntityModel>? EntityModels { get; set; }
    public List<Blog>? Blogs { get; set; }
    public List<Catalog>? Catalogs { get; set; }
    public List<Tags>? Tags { get; set; }
}
