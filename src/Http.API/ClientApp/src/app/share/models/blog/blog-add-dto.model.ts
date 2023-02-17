import { LanguageType } from '../enum/language-type.model';
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
   * 作者
   */
  authors?: string | null;

}
