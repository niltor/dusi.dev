/**
 * 角色查询筛选
 */
export interface RoleFilterDto {
  pageIndex?: number | null;
  pageSize?: number | null;
  /**
   * 排序
   */
  orderBy?:  | null;
  name?: string | null;
  /**
   * 角色名，系统标识
   */
  nameValue?: string | null;
  /**
   * 是否系统内置,系统内置不可删除
   */
  isSystem?: boolean | null;

}
