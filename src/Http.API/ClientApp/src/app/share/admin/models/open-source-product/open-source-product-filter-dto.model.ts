/**
 * 开源作品查询筛选
 */
export interface OpenSourceProductFilterDto {
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
   * 标题
   */
  title?: string | null;
  /**
   * 标签
   */
  tags?: string | null;

}
