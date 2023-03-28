import { LanguageType } from '../enum/language-type.model';
import { Catalog } from '../catalog/catalog.model';
import { Tags } from '../tags/tags.model';
import { BlogType } from '../enum/blog-type.model';
/**
 * 博客列表元素
 */
export interface BlogItemDto {
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
  tags?: Tags[] | null;
  blogType?: BlogType | null;

}
