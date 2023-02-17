import { Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { Observable } from 'rxjs';
import { BlogFilterDto } from '../models/blog/blog-filter-dto.model';
import { BlogAddDto } from '../models/blog/blog-add-dto.model';
import { BlogUpdateDto } from '../models/blog/blog-update-dto.model';
import { BlogItemDtoPageList } from '../models/blog/blog-item-dto-page-list.model';
import { Blog } from '../models/blog/blog.model';

/**
 * Blog
 */
@Injectable({ providedIn: 'root' })
export class BlogService extends BaseService {
  /**
   * filter
   * @param data BlogFilterDto
   */
  filter(data: BlogFilterDto): Observable<BlogItemDtoPageList> {
    const url = `/api/Blog/filter`;
    return this.request<BlogItemDtoPageList>('post', url, data);
  }

  /**
   * add
   * @param data BlogAddDto
   */
  add(data: BlogAddDto): Observable<Blog> {
    const url = `/api/Blog`;
    return this.request<Blog>('post', url, data);
  }

  /**
   * update
   * @param id string
   * @param data BlogUpdateDto
   */
  update(id: string, data: BlogUpdateDto): Observable<Blog> {
    const url = `/api/Blog/${id}`;
    return this.request<Blog>('put', url, data);
  }

  /**
   * getDetail
   * @param id string
   */
  getDetail(id: string): Observable<Blog> {
    const url = `/api/Blog/${id}`;
    return this.request<Blog>('get', url);
  }

  /**
   * delete
   * @param id string
   */
  delete(id: string): Observable<Blog> {
    const url = `/api/Blog/${id}`;
    return this.request<Blog>('delete', url);
  }

}
