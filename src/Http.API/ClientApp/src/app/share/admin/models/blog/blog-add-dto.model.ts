import { LanguageType } from '../enum/language-type.model';
/**
 * 博客添加时请求结构
 */
export interface BlogAddDto {
  languageType?: LanguageType | null;
  /**
   * 标题
   */
  title: string;
  /**
   * 描述
   */
  description?: string | null;
  /**
   * 内容
   */
  content: string;
  /**
   * 是否公开
   */
  isPublic: boolean;
  /**
   * 是否原创
   */
  isOriginal: boolean;
  /**
   * 所属目录
   */
  catalogId: string;
  /**
   * 标签
   */
  tagIds?: string[] | null;

}
