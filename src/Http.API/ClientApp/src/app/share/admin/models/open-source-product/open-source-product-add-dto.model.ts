/**
 * 开源作品添加时请求结构
 */
export interface OpenSourceProductAddDto {
  /**
   * 标题
   */
  title: string;
  /**
   * project url address
   */
  projectUrl: string;
  /**
   * 描述
   */
  description: string;
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
