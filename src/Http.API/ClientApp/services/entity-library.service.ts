import { Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { Observable } from 'rxjs';
import { EntityLibraryFilterDto } from '../models/entity-library/entity-library-filter-dto.model';
import { EntityLibraryAddDto } from '../models/entity-library/entity-library-add-dto.model';
import { EntityLibraryUpdateDto } from '../models/entity-library/entity-library-update-dto.model';
import { EntityLibraryItemDtoPageList } from '../models/entity-library/entity-library-item-dto-page-list.model';
import { EntityLibrary } from '../models/entity-library/entity-library.model';

/**
 * EntityLibrary
 */
@Injectable({ providedIn: 'root' })
export class EntityLibraryService extends BaseService {
  /**
   * filter
   * @param data EntityLibraryFilterDto
   */
  filter(data: EntityLibraryFilterDto): Observable<EntityLibraryItemDtoPageList> {
    const url = `/api/EntityLibrary/filter`;
    return this.request<EntityLibraryItemDtoPageList>('post', url, data);
  }

  /**
   * add
   * @param data EntityLibraryAddDto
   */
  add(data: EntityLibraryAddDto): Observable<EntityLibrary> {
    const url = `/api/EntityLibrary`;
    return this.request<EntityLibrary>('post', url, data);
  }

  /**
   * update
   * @param id string
   * @param data EntityLibraryUpdateDto
   */
  update(id: string, data: EntityLibraryUpdateDto): Observable<EntityLibrary> {
    const url = `/api/EntityLibrary/${id}`;
    return this.request<EntityLibrary>('put', url, data);
  }

  /**
   * getDetail
   * @param id string
   */
  getDetail(id: string): Observable<EntityLibrary> {
    const url = `/api/EntityLibrary/${id}`;
    return this.request<EntityLibrary>('get', url);
  }

  /**
   * delete
   * @param id string
   */
  delete(id: string): Observable<EntityLibrary> {
    const url = `/api/EntityLibrary/${id}`;
    return this.request<EntityLibrary>('delete', url);
  }

}
