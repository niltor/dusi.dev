import { EntityMemberItemDto } from '../entity-member/entity-member-item-dto.model';
export interface EntityMemberItemDtoPageList {
  count: number;
  data?: EntityMemberItemDto[];
  pageIndex: number;

}
