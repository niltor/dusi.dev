import { NewsType } from '../enum/news-type.model';
import { TechType } from '../enum/tech-type.model';
import { NewsStatus } from '../enum/news-status.model';
/**
 * 批量修改dto
 */
export interface ThirdNewsBatchUpdateDto {
  /**
   * 要修改的对象
   */
  ids: string[];
  newsType?: NewsType | null;
  techType?: TechType | null;
  /**
   * 第三方资讯状态
   */
  newsStatus?: NewsStatus | null;
  /**
   * 是否删除
   */
  isDelete?: boolean | null;

}
