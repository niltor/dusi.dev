import { OpenSourceProductItemDto } from '../open-source-product/open-source-product-item-dto.model';
export interface OpenSourceProductItemDtoPageList {
  count: number;
  data?: OpenSourceProductItemDto[];
  pageIndex: number;

}
