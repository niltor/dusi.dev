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
  entityLibraryId?: string | null;

}
