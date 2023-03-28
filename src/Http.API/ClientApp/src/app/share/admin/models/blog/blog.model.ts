import { LanguageType } from '../enum/language-type.model';
import { BlogType } from '../enum/blog-type.model';
import { User } from '../user/user.model';
import { Catalog } from '../catalog.model';
import { Tags } from '../tags.model';
/**
 * 博客
 */
export interface Blog {
  id: string;
  createdTime: Date;
  updatedTime: Date;
  /**
   * 软删除
   */
  isDeleted: boolean;
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
   * 作者
   */
  authors: string;
  /**
   * 标题
   */
  translateTitle?: string | null;
  /**
   * 翻译内容
   */
  translateContent?: string | null;
  languageType?: LanguageType | null;
  blogType?: BlogType | null;
  /**
   * 是否审核
   */
  isAudit: boolean;
  /**
   * 是否公开
   */
  isPublic: boolean;
  /**
   * 是否原创
   */
  isOriginal: boolean;
  /**
   * 用户账户
   */
  user?: User | null;
  /**
   * 目录
   */
  catalog?: Catalog | null;
  tags?: Tags[] | null;

}
