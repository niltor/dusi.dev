import { EntityMemberConstraintItemDto } from '../entity-member-constraint-item-dto.model';
export interface EntityMemberConstraintItemDtoPageList {
  count: number;
  data?: EntityMemberConstraintItemDto[] | null;
  pageIndex: number;

}
