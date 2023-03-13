import { AccessModifier } from '../enum/access-modifier.model';
import { CodeLanguage } from '../enum/code-language.model';
import { EntityModel } from '../entity-model/entity-model.model';
import { EntityMember } from '../entity-member/entity-member.model';
import { EntityLibrary } from '../entity-library/entity-library.model';
/**
 * 实体模型类添加时请求结构
 */
export interface EntityModelAddDto {
  /**
   * 实体类名
   */
  name?: string | null;
  /**
   * 实体注释内容
   */
  comment?: string | null;
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
  languageVersion?: string | null;
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
  parentEntityId: string;
  entityLibraryId: string;

}
