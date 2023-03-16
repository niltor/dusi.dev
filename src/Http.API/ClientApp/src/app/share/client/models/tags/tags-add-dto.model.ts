/**
 * 标签添加时请求结构
 */
export interface TagsAddDto {
  /**
   * 标签名称
   */
  name: string;
  /**
   * 标签颜色
   */
  color?: string | null;

}
