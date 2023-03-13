import { Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { Observable } from 'rxjs';
import { TagsFilterDto } from '../models/tags/tags-filter-dto.model';
import { TagsAddDto } from '../models/tags/tags-add-dto.model';
import { TagsUpdateDto } from '../models/tags/tags-update-dto.model';
import { TagsItemDtoPageList } from '../models/tags/tags-item-dto-page-list.model';
import { Tags } from '../models/tags/tags.model';

/**
 * 标签
 */
@Injectable({ providedIn: 'root' })
export class TagsService extends BaseService {
  /**
   * 筛选
   * @param data TagsFilterDto
   */
  filter(data: TagsFilterDto): Observable<TagsItemDtoPageList> {
    const url = `/api/admin/Tags/filter`;
    return this.request<TagsItemDtoPageList>('post', url, data);
  }

  /**
   * 新增
   * @param data TagsAddDto
   */
  add(data: TagsAddDto): Observable<Tags> {
    const url = `/api/admin/Tags`;
    return this.request<Tags>('post', url, data);
  }

  /**
   * 更新
   * @param id 
   * @param data TagsUpdateDto
   */
  update(id: string, data: TagsUpdateDto): Observable<Tags> {
    const url = `/api/admin/Tags/${id}`;
    return this.request<Tags>('put', url, data);
  }

  /**
   * 详情
   * @param id 
   */
  getDetail(id: string): Observable<Tags> {
    const url = `/api/admin/Tags/${id}`;
    return this.request<Tags>('get', url);
  }

  /**
   * ⚠删除
   * @param id 
   */
  delete(id: string): Observable<Tags> {
    const url = `/api/admin/Tags/${id}`;
    return this.request<Tags>('delete', url);
  }

}
