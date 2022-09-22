/**
 * 实体库查询筛选
 */
export interface EntityLibraryFilterDto {
  pageIndex?: number | null;
  pageSize?: number | null;
  /**
   * 排序
   */
  orderBy?:  | null;
  name?: string | null;
  /**
   * 是否公开
   */
  isPublic?: boolean | null;

}
