import { ThirdVideoItemDto } from '../third-video/third-video-item-dto.model';
export interface ThirdVideoItemDtoPageList {
  count: number;
  data?: ThirdVideoItemDto[];
  pageIndex: number;

}
