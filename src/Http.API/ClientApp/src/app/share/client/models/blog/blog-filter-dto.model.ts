import { LanguageType } from '../enum/language-type.model';
import { BlogType } from '../enum/blog-type.model';
/**
 * 博客查询筛选
 */
export interface BlogFilterDto {
  pageIndex: number;
  /**
   * 默认最大1000
   */
  pageSize: number;
  /**
   * 排序
   */
  orderBy?: any | null;
  languageType?: LanguageType | null;
  /**
   * 标题
   */
  title?: string | null;
  /**
   * 作者
   */
  authors?: string | null;
  /**
   * 是否公开
   */
  isPublic?: boolean | null;
  /**
   * 所属目录
   */
  catalogId?: string | null;
  /**
   * 标签
   */
  tag?: string | null;
  /**
   * 日期
   */
  date?: string | null;
  blogType?: BlogType | null;

}
