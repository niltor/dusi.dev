import { Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { Observable } from 'rxjs';
import { EntityModelFilterDto } from '../models/entity-model/entity-model-filter-dto.model';
import { EntityModelAddDto } from '../models/entity-model/entity-model-add-dto.model';
import { EntityModelUpdateDto } from '../models/entity-model/entity-model-update-dto.model';
import { EntityModelItemDtoPageList } from '../models/entity-model/entity-model-item-dto-page-list.model';
import { EntityModel } from '../models/entity-model/entity-model.model';

/**
 * 实体模型类
 */
@Injectable({ providedIn: 'root' })
export class EntityModelService extends BaseService {
  /**
   * 筛选
   * @param data EntityModelFilterDto
   */
  filter(data: EntityModelFilterDto): Observable<EntityModelItemDtoPageList> {
    const url = `/api/admin/EntityModel/filter`;
    return this.request<EntityModelItemDtoPageList>('post', url, data);
  }

  /**
   * 新增
   * @param data EntityModelAddDto
   */
  add(data: EntityModelAddDto): Observable<EntityModel> {
    const url = `/api/admin/EntityModel`;
    return this.request<EntityModel>('post', url, data);
  }

  /**
   * 更新
   * @param id 
   * @param data EntityModelUpdateDto
   */
  update(id: string, data: EntityModelUpdateDto): Observable<EntityModel> {
    const url = `/api/admin/EntityModel/${id}`;
    return this.request<EntityModel>('put', url, data);
  }

  /**
   * 详情
   * @param id 
   */
  getDetail(id: string): Observable<EntityModel> {
    const url = `/api/admin/EntityModel/${id}`;
    return this.request<EntityModel>('get', url);
  }

  /**
   * ⚠删除
   * @param id 
   */
  delete(id: string): Observable<EntityModel> {
    const url = `/api/admin/EntityModel/${id}`;
    return this.request<EntityModel>('delete', url);
  }

}
