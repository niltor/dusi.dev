/**
 * 标签更新时请求结构
 */
export interface TagsUpdateDto {
  /**
   * 标签名称
   */
  name: string;
  /**
   * 标签颜色
   */
  color?: string | null;

}
