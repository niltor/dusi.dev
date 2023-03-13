/**
 * 标签列表元素
 */
export interface TagsItemDto {
  /**
   * 标签名称
   */
  name?: string | null;
  /**
   * 标签颜色
   */
  color?: string | null;
  id: string;
  createdTime: Date;
  updatedTime: Date;

}
