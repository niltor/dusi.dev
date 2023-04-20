import { LanguageType } from '../enum/language-type.model';
import { BlogType } from '../enum/blog-type.model';
/**
 * 博客更新时请求结构
 */
export interface BlogUpdateDto {
  languageType?: LanguageType | null;
  /**
   * 标题
   */
  title?: string | null;
  /**
   * 描述
   */
  description?: string | null;
  /**
   * 内容
   */
  content?: string | null;
  /**
   * 是否公开
   */
  isPublic?: boolean | null;
  /**
   * 所属目录
   */
  catalogId: string;
  /**
   * 是否原创
   */
  isOriginal?: boolean | null;
  /**
   * 标签
   */
  tagIds?: string[] | null;
  blogType?: BlogType | null;

}
