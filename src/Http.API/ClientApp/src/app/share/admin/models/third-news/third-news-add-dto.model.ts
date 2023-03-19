import { NewsSource } from '../enum/news-source.model';
import { NewsType } from '../enum/news-type.model';
import { NewsTags } from '../news-tags.model';
import { TechType } from '../enum/tech-type.model';
export interface ThirdNewsAddDto {
  title: string;
  description?: string | null;
  content?: string | null;
  type?: NewsSource | null;
  newsType?: NewsType | null;
  newsTags?: NewsTags[] | null;
  techType?: TechType | null;

}
