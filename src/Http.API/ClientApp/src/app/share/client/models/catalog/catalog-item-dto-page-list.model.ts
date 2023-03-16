import { CatalogItemDto } from '../catalog/catalog-item-dto.model';
export interface CatalogItemDtoPageList {
  count: number;
  data?: CatalogItemDto[];
  pageIndex: number;

}
