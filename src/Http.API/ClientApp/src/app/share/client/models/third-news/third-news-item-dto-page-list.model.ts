import { ThirdNewsItemDto } from '../third-news/third-news-item-dto.model';
export interface ThirdNewsItemDtoPageList {
  count: number;
  data?: ThirdNewsItemDto[];
  pageIndex: number;

}
