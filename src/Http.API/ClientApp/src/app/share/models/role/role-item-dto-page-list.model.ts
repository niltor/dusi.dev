import { RoleItemDto } from '../role/role-item-dto.model';
export interface RoleItemDtoPageList {
  count: number;
  data?: RoleItemDto[] | null;
  pageIndex: number;

}
