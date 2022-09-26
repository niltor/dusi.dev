import { Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { Observable } from 'rxjs';
import { EntityMemberConstraintFilterDto } from '../models/entity-member-constraint/entity-member-constraint-filter-dto.model';
import { EntityMemberConstraintAddDto } from '../models/entity-member-constraint/entity-member-constraint-add-dto.model';
import { EntityMemberConstraintUpdateDto } from '../models/entity-member-constraint/entity-member-constraint-update-dto.model';
import { EntityMemberConstraintItemDtoPageList } from '../models/entity-member-constraint/entity-member-constraint-item-dto-page-list.model';
import { EntityMemberConstraint } from '../models/entity-member-constraint/entity-member-constraint.model';

/**
 * EntityMemberConstraint
 */
@Injectable({ providedIn: 'root' })
export class EntityMemberConstraintService extends BaseService {
  /**
   * filter
   * @param data EntityMemberConstraintFilterDto
   */
  filter(data: EntityMemberConstraintFilterDto): Observable<EntityMemberConstraintItemDtoPageList> {
    const url = `/api/EntityMemberConstraint/filter`;
    return this.request<EntityMemberConstraintItemDtoPageList>('post', url, data);
  }

  /**
   * add
   * @param data EntityMemberConstraintAddDto
   */
  add(data: EntityMemberConstraintAddDto): Observable<EntityMemberConstraint> {
    const url = `/api/EntityMemberConstraint`;
    return this.request<EntityMemberConstraint>('post', url, data);
  }

  /**
   * update
   * @param id string
   * @param data EntityMemberConstraintUpdateDto
   */
  update(id: string, data: EntityMemberConstraintUpdateDto): Observable<EntityMemberConstraint> {
    const url = `/api/EntityMemberConstraint/${id}`;
    return this.request<EntityMemberConstraint>('put', url, data);
  }

  /**
   * getDetail
   * @param id string
   */
  getDetail(id: string): Observable<EntityMemberConstraint> {
    const url = `/api/EntityMemberConstraint/${id}`;
    return this.request<EntityMemberConstraint>('get', url);
  }

  /**
   * delete
   * @param id string
   */
  delete(id: string): Observable<EntityMemberConstraint> {
    const url = `/api/EntityMemberConstraint/${id}`;
    return this.request<EntityMemberConstraint>('delete', url);
  }

}
