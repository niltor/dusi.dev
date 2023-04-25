import { Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { Observable } from 'rxjs';
import { BlogFilterDto } from '../models/blog/blog-filter-dto.model';
import { BlogItemDtoPageList } from '../models/blog/blog-item-dto-page-list.model';

/**
 * 博客
 */
@Injectable({ providedIn: 'root' })
export class BlogService extends BaseService {
  /**
   * 筛选
   * @param data BlogFilterDto
   */
  filter(data: BlogFilterDto): Observable<BlogItemDtoPageList> {
    const url = `/api/admin/Blog/filter`;
    return this.request<BlogItemDtoPageList>('post', url, data);
  }

}
