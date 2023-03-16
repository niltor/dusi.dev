import { AccessModifier } from '../enum/access-modifier.model';
import { CodeLanguage } from '../enum/code-language.model';
import { EntityMember } from '../entity-member/entity-member.model';
import { EntityLibrary } from '../entity-library/entity-library.model';
import { User } from '../user/user.model';
/**
 * 实体模型类
 */
export interface EntityModel {
  id: string;
  createdTime: Date;
  updatedTime: Date;
  /**
   * 软删除
   */
  isDeleted: boolean;
  /**
   * 实体类名
   */
  name: string;
  /**
   * 实体注释内容
   */
  comment: string;
  /**
   * 访问修饰符
   */
  accessModifier?: AccessModifier | null;
  /**
   * 代码内容
   */
  codeContent?: string | null;
  /**
   * 编程语言
   */
  codeLanguage?: CodeLanguage | null;
  /**
   * 语言版本
   */
  languageVersion: string;
  /**
   * 实体模型类
   */
  parentEntity?: EntityModel | null;
  /**
   * 直属子类
   */
  childrenEntities?: EntityModel[] | null;
  /**
   * 包含的属性
   */
  entityMembers?: EntityMember[] | null;
  /**
   * 实体库
   */
  entityLibrary?: EntityLibrary | null;
  /**
   * 用户账户
   */
  user?: User | null;

}
