import { NewsSource } from '../enum/news-source.model';
import { NewsType } from '../enum/news-type.model';
import { TechType } from '../enum/tech-type.model';
export interface ThirdNewsItemDto {
  authorName?: string | null;
  authorAvatar?: string | null;
  title: string;
  url?: string | null;
  thumbnailUrl?: string | null;
  provider?: string | null;
  datePublished?: Date | null;
  category?: string | null;
  identityId?: string | null;
  type?: NewsSource | null;
  newsType?: NewsType | null;
  techType?: TechType | null;
  id: string;
  createdTime: Date;
  updatedTime: Date;
  description?: string | null;

}
