/**
 * 目录查询筛选
 */
export interface CatalogFilterDto {
  pageIndex: number;
  pageSize: number;
  /**
   * 排序
   */
  orderBy?: any | null;
  /**
   * 目录名称
   */
  name?: string | null;
  /**
   * 层级
   */
  level?: number | null;
  parentId?: string | null;
  userId?: string | null;

}
