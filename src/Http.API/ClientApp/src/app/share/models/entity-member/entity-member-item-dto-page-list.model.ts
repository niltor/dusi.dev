import { EntityMemberItemDto } from '../entity-member-item-dto.model';
export interface EntityMemberItemDtoPageList {
  count: number;
  data?: EntityMemberItemDto[] | null;
  pageIndex: number;

}
