import { EntityModelItemDto } from '../entity-model-item-dto.model';
export interface EntityModelItemDtoPageList {
  count: number;
  data?: EntityModelItemDto[] | null;
  pageIndex: number;

}
