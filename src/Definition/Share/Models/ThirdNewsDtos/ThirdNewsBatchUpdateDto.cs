using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity.CMS;

namespace Share.Models.ThirdNewsDtos;
/// <summary>
/// 批量修改dto
/// </summary>
public class ThirdNewsBatchUpdateDto
{
    /// <summary>
    /// 要修改的对象
    /// </summary>
    public List<Guid> Ids { get; set; } = null!;

    public NewsType? NewsType { get; set; }
    public TechType? TechType { get; set; }
    public NewsStatus? NewsStatus { get; set; }
    /// <summary>
    /// 是否删除
    /// </summary>
    public bool? IsDelete { get; set; } = false;
}
