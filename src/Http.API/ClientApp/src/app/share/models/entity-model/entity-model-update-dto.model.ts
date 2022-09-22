import { AccessModifier } from '../enum/access-modifier.model';
import { CodeLanguage } from '../enum/code-language.model';
/**
 * 实体模型类更新时请求结构
 */
export interface EntityModelUpdateDto {
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
   * 代码示例
   */
  codeExample?: string | null;
  /**
   * 编程语言
   */
  codeLanguage?: CodeLanguage | null;
  entityLibraryId: string;

}
