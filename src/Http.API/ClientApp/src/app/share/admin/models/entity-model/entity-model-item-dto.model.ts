import { AccessModifier } from '../enum/access-modifier.model';
import { CodeLanguage } from '../enum/code-language.model';
/**
 * 实体模型类列表元素
 */
export interface EntityModelItemDto {
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
   * 编程语言
   */
  codeLanguage?: CodeLanguage | null;
  /**
   * 语言版本
   */
  languageVersion?: string | null;
  id: string;
  createdTime: Date;
  updatedTime: Date;

}
