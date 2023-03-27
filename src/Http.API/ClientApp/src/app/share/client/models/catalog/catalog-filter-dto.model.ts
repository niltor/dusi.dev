/**
 * 目录查询筛选
 */
export interface CatalogFilterDto {
  pageIndex: number;
  /**
   * 默认最大1000
   */
  pageSize: number;
  /**
   * 排序
   */
  orderBy?: any | null;
  /**
   * 目录名称
   */
  name?: string | null;

}
