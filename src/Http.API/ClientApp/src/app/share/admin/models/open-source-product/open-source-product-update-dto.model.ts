/**
 * 开源作品更新时请求结构
 */
export interface OpenSourceProductUpdateDto {
  /**
   * 标题
   */
  title?: string | null;
  /**
   * project url address
   */
  projectUrl?: string | null;
  /**
   * 描述
   */
  description?: string | null;
  /**
   * 缩略图
   */
  thumbnail?: string | null;
  /**
   * 作者
   */
  authors?: string | null;
  /**
   * 标签
   */
  tags?: string | null;

}
