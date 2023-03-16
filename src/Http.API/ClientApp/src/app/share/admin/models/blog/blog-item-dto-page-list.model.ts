import { BlogItemDto } from '../blog/blog-item-dto.model';
export interface BlogItemDtoPageList {
  count: number;
  data?: BlogItemDto[];
  pageIndex: number;

}
