import { Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { Observable } from 'rxjs';
import { BlogFilterDto } from '../models/blog/blog-filter-dto.model';
import { BlogUpdateDto } from '../models/blog/blog-update-dto.model';
import { BlogAddDto } from '../models/blog/blog-add-dto.model';
import { BlogItemDtoPageList } from '../models/blog/blog-item-dto-page-list.model';
import { Blog } from '../models/blog/blog.model';
import { UploadResult } from '../models/blog/upload-result.model';

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
    const url = `/api/Blog/filter`;
    return this.request<BlogItemDtoPageList>('post', url, data);
  }

  /**
   * 详情
   * @param id 
   */
  getDetail(id: string): Observable<Blog> {
    const url = `/api/Blog/${id}`;
    return this.request<Blog>('get', url);
  }

  /**
   * 更新
   * @param id 
   * @param data BlogUpdateDto
   */
  update(id: string, data: BlogUpdateDto): Observable<Blog> {
    const url = `/api/Blog/${id}`;
    return this.request<Blog>('put', url, data);
  }

  /**
   * ⚠删除
   * @param id 
   */
  delete(id: string): Observable<Blog> {
    const url = `/api/Blog/${id}`;
    return this.request<Blog>('delete', url);
  }

  /**
   * 新增
   * @param data BlogAddDto
   */
  add(data: BlogAddDto): Observable<Blog> {
    const url = `/api/Blog`;
    return this.request<Blog>('post', url, data);
  }

  /**
   * 上传图片
   * @param data FormData
   */
  uploadImg(data: FormData): Observable<UploadResult> {
    const url = `/api/Blog/upload`;
    return this.request<UploadResult>('post', url, data);
  }

}
