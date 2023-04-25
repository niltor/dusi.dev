/**
 * 三方视频查询筛选
 */
export interface ThirdVideoFilterDto {
  pageIndex: number;
  /**
   * 默认最大1000
   */
  pageSize: number;
  /**
   * 排序
   */
  orderBy?: any | null;
  title?: string | null;

}
