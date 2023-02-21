import { LanguageType } from '../enum/language-type.model';
/**
 * 博客添加时请求结构
 */
export interface BlogAddDto {
  /**
   * 标题
   */
  translateTitle?: string | null;
  /**
   * 翻译内容
   */
  translateContent?: string | null;
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
  tags?: string[] | null;

}
