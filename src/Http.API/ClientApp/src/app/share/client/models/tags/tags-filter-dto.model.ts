/**
 * 标签查询筛选
 */
export interface TagsFilterDto {
  pageIndex: number;
  pageSize: number;
  /**
   * 排序
   */
  orderBy?: any | null;
  /**
   * 标签名称
   */
  name?: string | null;
  /**
   * 标签颜色
   */
  color?: string | null;
  userId?: string | null;

}
