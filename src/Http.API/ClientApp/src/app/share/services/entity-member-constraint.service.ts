import { Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { Observable } from 'rxjs';
import { EntityMemberConstraintFilterDto } from '../models/entity-member-constraint/entity-member-constraint-filter-dto.model';
import { EntityMemberConstraintAddDto } from '../models/entity-member-constraint/entity-member-constraint-add-dto.model';
import { EntityMemberConstraintUpdateDto } from '../models/entity-member-constraint/entity-member-constraint-update-dto.model';
import { EntityMemberConstraintItemDtoPageList } from '../models/entity-member-constraint/entity-member-constraint-item-dto-page-list.model';
import { EntityMemberConstraint } from '../models/entity-member-constraint/entity-member-constraint.model';

/**
 * 属性的约束
 */
@Injectable({ providedIn: 'root' })
export class EntityMemberConstraintService extends BaseService {
  /**
   * 筛选
   * @param data EntityMemberConstraintFilterDto
   */
  filter(data: EntityMemberConstraintFilterDto): Observable<EntityMemberConstraintItemDtoPageList> {
    const url = `/api/EntityMemberConstraint/filter`;
    return this.request<EntityMemberConstraintItemDtoPageList>('post', url, data);
  }

  /**
   * 新增
   * @param data EntityMemberConstraintAddDto
   */
  add(data: EntityMemberConstraintAddDto): Observable<EntityMemberConstraint> {
    const url = `/api/EntityMemberConstraint`;
    return this.request<EntityMemberConstraint>('post', url, data);
  }

  /**
   * 更新
   * @param id 
   * @param data EntityMemberConstraintUpdateDto
   */
  update(id: string, data: EntityMemberConstraintUpdateDto): Observable<EntityMemberConstraint> {
    const url = `/api/EntityMemberConstraint/${id}`;
    return this.request<EntityMemberConstraint>('put', url, data);
  }

  /**
   * 详情
   * @param id 
   */
  getDetail(id: string): Observable<EntityMemberConstraint> {
    const url = `/api/EntityMemberConstraint/${id}`;
    return this.request<EntityMemberConstraint>('get', url);
  }

  /**
   * ⚠删除
   * @param id 
   */
  delete(id: string): Observable<EntityMemberConstraint> {
    const url = `/api/EntityMemberConstraint/${id}`;
    return this.request<EntityMemberConstraint>('delete', url);
  }

}
