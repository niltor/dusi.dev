import { NewsSource } from '../enum/news-source.model';
import { NewsType } from '../enum/news-type.model';
import { NewsTags } from '../news-tags.model';
import { TechType } from '../enum/tech-type.model';
export interface ThirdNewsUpdateDto {
  title?: string | null;
  description?: string | null;
  thumbnailUrl?: string | null;
  content?: string | null;
  category?: string | null;
  type?: NewsSource | null;
  newsType?: NewsType | null;
  newsTags?: NewsTags[] | null;
  techType?: TechType | null;

}
