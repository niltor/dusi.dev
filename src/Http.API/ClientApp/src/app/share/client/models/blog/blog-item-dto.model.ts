import { LanguageType } from '../enum/language-type.model';
import { Catalog } from '../catalog/catalog.model';
/**
 * 博客列表元素
 */
export interface BlogItemDto {
  /**
   * 标题
   */
  translateTitle?: string | null;
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
   * 作者
   */
  authors: string;
  id: string;
  createdTime: Date;
  updatedTime: Date;
  /**
   * 目录
   */
  catalog?: Catalog | null;

}
