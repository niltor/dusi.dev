/**
 * 实体库查询筛选
 */
export interface EntityLibraryFilterDto {
  pageIndex: number;
  pageSize: number;
  /**
   * 排序
   */
  orderBy?: any | null;
  /**
   * 库名称
   */
  name?: string | null;
  /**
   * 库描述
   */
  description?: string | null;
  /**
   * 是否公开
   */
  isPublic?: boolean | null;
  userId?: string | null;

}
