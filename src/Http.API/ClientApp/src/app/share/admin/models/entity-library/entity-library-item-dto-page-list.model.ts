import { EntityLibraryItemDto } from '../entity-library/entity-library-item-dto.model';
export interface EntityLibraryItemDtoPageList {
  count: number;
  data?: EntityLibraryItemDto[] | null;
  pageIndex: number;

}
