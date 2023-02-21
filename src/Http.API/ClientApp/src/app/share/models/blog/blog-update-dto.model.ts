import { LanguageType } from '../enum/language-type.model';
/**
 * 博客更新时请求结构
 */
export interface BlogUpdateDto {
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
   * 作者
   */
  authors?: string | null;
  /**
   * 所属目录
   */
  catalogId: string;
  /**
   * 标签
   */
  tags?: string[] | null;

}
