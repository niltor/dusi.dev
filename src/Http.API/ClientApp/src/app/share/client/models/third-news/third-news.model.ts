import { NewsSource } from '../enum/news-source.model';
import { NewsType } from '../enum/news-type.model';
import { NewsTags } from '../news-tags.model';
import { TechType } from '../enum/tech-type.model';
/**
 * 第三方资讯
 */
export interface ThirdNews {
  id: string;
  createdTime: Date;
  updatedTime: Date;
  /**
   * 软删除
   */
  isDeleted: boolean;
  authorName?: string | null;
  authorAvatar?: string | null;
  title: string;
  description?: string | null;
  url?: string | null;
  thumbnailUrl?: string | null;
  provider?: string | null;
  datePublished?: Date | null;
  content?: string | null;
  category?: string | null;
  identityId?: string | null;
  type?: NewsSource | null;
  newsType?: NewsType | null;
  newsTags?: NewsTags[] | null;
  techType?: TechType | null;

}
