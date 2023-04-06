import { CodeLanguage } from '../enum/code-language.model';
/**
 * 实体模型类查询筛选
 */
export interface EntityModelFilterDto {
  pageIndex: number;
  /**
   * 默认最大1000
   */
  pageSize: number;
  /**
   * 排序
   */
  orderBy?: any | null;
  /**
   * 实体类名
   */
  name?: string | null;
  /**
   * 编程语言
   */
  codeLanguage?: CodeLanguage | null;
  /**
   * 语言版本
   */
  languageVersion?: string | null;
  /**
   * 所属实体库
   */
  entityLibraryId?: string | null;

}
