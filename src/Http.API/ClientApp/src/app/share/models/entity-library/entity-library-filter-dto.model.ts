/**
 * 实体库查询筛选
 */
export interface EntityLibraryFilterDto {
  pageIndex: number;
  pageSize: number;
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
