import { Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { Observable } from 'rxjs';
import { EntityModelFilterDto } from '../models/entity-model/entity-model-filter-dto.model';
import { EntityModelAddDto } from '../models/entity-model/entity-model-add-dto.model';
import { EntityModelUpdateDto } from '../models/entity-model/entity-model-update-dto.model';
import { EntityModelItemDtoPageList } from '../models/entity-model/entity-model-item-dto-page-list.model';
import { EntityModel } from '../models/entity-model/entity-model.model';

/**
 * EntityModel
 */
@Injectable({ providedIn: 'root' })
export class EntityModelService extends BaseService {
  /**
   * filter
   * @param data EntityModelFilterDto
   */
  filter(data: EntityModelFilterDto): Observable<EntityModelItemDtoPageList> {
    const url = `/api/EntityModel/filter`;
    return this.request<EntityModelItemDtoPageList>('post', url, data);
  }

  /**
   * add
   * @param data EntityModelAddDto
   */
  add(data: EntityModelAddDto): Observable<EntityModel> {
    const url = `/api/EntityModel`;
    return this.request<EntityModel>('post', url, data);
  }

  /**
   * update
   * @param id string
   * @param data EntityModelUpdateDto
   */
  update(id: string, data: EntityModelUpdateDto): Observable<EntityModel> {
    const url = `/api/EntityModel/${id}`;
    return this.request<EntityModel>('put', url, data);
  }

  /**
   * getDetail
   * @param id string
   */
  getDetail(id: string): Observable<EntityModel> {
    const url = `/api/EntityModel/${id}`;
    return this.request<EntityModel>('get', url);
  }

  /**
   * delete
   * @param id string
   */
  delete(id: string): Observable<EntityModel> {
    const url = `/api/EntityModel/${id}`;
    return this.request<EntityModel>('delete', url);
  }

}
