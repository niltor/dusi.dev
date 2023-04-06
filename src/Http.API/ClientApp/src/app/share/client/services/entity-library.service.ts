import { Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { Observable } from 'rxjs';
import { EntityLibraryFilterDto } from '../models/entity-library/entity-library-filter-dto.model';
import { EntityLibraryAddDto } from '../models/entity-library/entity-library-add-dto.model';
import { EntityLibraryUpdateDto } from '../models/entity-library/entity-library-update-dto.model';
import { EntityLibraryItemDtoPageList } from '../models/entity-library/entity-library-item-dto-page-list.model';
import { EntityLibrary } from '../models/entity-library/entity-library.model';

/**
 * 实体库
 */
@Injectable({ providedIn: 'root' })
export class EntityLibraryService extends BaseService {
  /**
   * 筛选
   * @param data EntityLibraryFilterDto
   */
  filter(data: EntityLibraryFilterDto): Observable<EntityLibraryItemDtoPageList> {
    const url = `/api/EntityLibrary/filter`;
    return this.request<EntityLibraryItemDtoPageList>('post', url, data);
  }

  /**
   * 新增
   * @param data EntityLibraryAddDto
   */
  add(data: EntityLibraryAddDto): Observable<EntityLibrary> {
    const url = `/api/EntityLibrary`;
    return this.request<EntityLibrary>('post', url, data);
  }

  /**
   * 更新
   * @param id 
   * @param data EntityLibraryUpdateDto
   */
  update(id: string, data: EntityLibraryUpdateDto): Observable<EntityLibrary> {
    const url = `/api/EntityLibrary/${id}`;
    return this.request<EntityLibrary>('put', url, data);
  }

  /**
   * 详情
   * @param id 
   */
  getDetail(id: string): Observable<EntityLibrary> {
    const url = `/api/EntityLibrary/${id}`;
    return this.request<EntityLibrary>('get', url);
  }

  /**
   * ⚠删除
   * @param id 
   */
  delete(id: string): Observable<EntityLibrary> {
    const url = `/api/EntityLibrary/${id}`;
    return this.request<EntityLibrary>('delete', url);
  }

}
