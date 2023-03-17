import { LanguageType } from '../enum/language-type.model';
/**
 * 博客更新时请求结构
 */
export interface BlogUpdateDto {
  languageType?: LanguageType | null;
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
   * 是否公开
   */
  isPublic?: boolean | null;
  /**
   * 所属目录
   */
  catalogId?: string | null;
  /**
   * 是否原创
   */
  isOriginal?: boolean | null;
  /**
   * 标签
   */
  tags?: string[] | null;

}
