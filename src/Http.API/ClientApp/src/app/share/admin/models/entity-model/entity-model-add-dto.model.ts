import { CodeLanguage } from '../enum/code-language.model';
/**
 * 实体模型类添加时请求结构
 */
export interface EntityModelAddDto {
  /**
   * 实体类名
   */
  name: string;
  /**
   * 实体注释内容
   */
  comment: string;
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
   * 所属实体库
   */
  entityLibraryId: string;

}
