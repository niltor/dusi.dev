import { UserItemDto } from '../user/user-item-dto.model';
export interface UserItemDtoPageList {
  count: number;
  data?: UserItemDto[];
  pageIndex: number;

}
