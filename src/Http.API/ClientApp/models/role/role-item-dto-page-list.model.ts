import { RoleItemDto } from '../role-item-dto.model';
export interface RoleItemDtoPageList {
  count: number;
  data?: RoleItemDto[] | null;
  pageIndex: number;

}
