import { EntityModelItemDto } from '../entity-model/entity-model-item-dto.model';
export interface EntityModelItemDtoPageList {
  count: number;
  data?: EntityModelItemDto[];
  pageIndex: number;

}
