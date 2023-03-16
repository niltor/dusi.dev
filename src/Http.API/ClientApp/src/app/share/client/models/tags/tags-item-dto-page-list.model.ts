import { TagsItemDto } from '../tags/tags-item-dto.model';
export interface TagsItemDtoPageList {
  count: number;
  data?: TagsItemDto[];
  pageIndex: number;

}
