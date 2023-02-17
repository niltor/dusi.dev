import { LanguageType } from '../enum/language-type.model';
export interface BlogFilterDto {
  pageIndex: number;
  pageSize: number;
  /**
   * 排序
   */
  orderBy?: any | null;
  /**
   * 标题
   */
  translateTitle?: string | null;
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
   * 作者
   */
  authors?: string | null;

}
