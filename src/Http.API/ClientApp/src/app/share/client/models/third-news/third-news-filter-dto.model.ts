import { NewsSource } from '../enum/news-source.model';
import { NewsType } from '../enum/news-type.model';
import { TechType } from '../enum/tech-type.model';
import { NewsStatus } from '../enum/news-status.model';
export interface ThirdNewsFilterDto {
  pageIndex: number;
  /**
   * 默认最大1000
   */
  pageSize: number;
  /**
   * 排序
   */
  orderBy?: any | null;
  authorName?: string | null;
  title?: string | null;
  category?: string | null;
  type?: NewsSource | null;
  newsType?: NewsType | null;
  techType?: TechType | null;
  /**
   * 第三方资讯状态
   */
  newsStatus?: NewsStatus | null;
  startDate?: Date | null;
  endDate?: Date | null;
  /**
   * 是否本周
   */
  onlyWeek?: boolean | null;
  /**
   * 是否被分类
   */
  isClassified?: boolean | null;

}
