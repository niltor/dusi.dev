/**
 * 实体库查询筛选
 */
export interface EntityLibraryFilterDto {
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
   * 库名称
   */
  name?: string | null;

}
