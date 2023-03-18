import { NewsSource } from '../enum/news-source.model';
import { NewsType } from '../enum/news-type.model';
import { TechType } from '../enum/tech-type.model';
export interface ThirdNewsFilterDto {
  pageIndex: number;
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
  startDate?: Date | null;
  endDate?: Date | null;

}
