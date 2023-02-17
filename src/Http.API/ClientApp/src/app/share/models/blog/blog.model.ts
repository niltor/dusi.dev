import { LanguageType } from '../enum/language-type.model';
import { User } from '../user/user.model';
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
   * 用户账户
   */
  user?: User | null;

}
