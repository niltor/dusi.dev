import { Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { Observable } from 'rxjs';
import { EntityMemberFilterDto } from '../models/entity-member/entity-member-filter-dto.model';
import { EntityMemberAddDto } from '../models/entity-member/entity-member-add-dto.model';
import { EntityMemberUpdateDto } from '../models/entity-member/entity-member-update-dto.model';
import { EntityMemberItemDtoPageList } from '../models/entity-member/entity-member-item-dto-page-list.model';
import { EntityMember } from '../models/entity-member/entity-member.model';

/**
 * EntityMember
 */
@Injectable({ providedIn: 'root' })
export class EntityMemberService extends BaseService {
  /**
   * filter
   * @param data EntityMemberFilterDto
   */
  filter(data: EntityMemberFilterDto): Observable<EntityMemberItemDtoPageList> {
    const url = `/api/EntityMember/filter`;
    return this.request<EntityMemberItemDtoPageList>('post', url, data);
  }

  /**
   * add
   * @param data EntityMemberAddDto
   */
  add(data: EntityMemberAddDto): Observable<EntityMember> {
    const url = `/api/EntityMember`;
    return this.request<EntityMember>('post', url, data);
  }

  /**
   * update
   * @param id string
   * @param data EntityMemberUpdateDto
   */
  update(id: string, data: EntityMemberUpdateDto): Observable<EntityMember> {
    const url = `/api/EntityMember/${id}`;
    return this.request<EntityMember>('put', url, data);
  }

  /**
   * getDetail
   * @param id string
   */
  getDetail(id: string): Observable<EntityMember> {
    const url = `/api/EntityMember/${id}`;
    return this.request<EntityMember>('get', url);
  }

  /**
   * delete
   * @param id string
   */
  delete(id: string): Observable<EntityMember> {
    const url = `/api/EntityMember/${id}`;
    return this.request<EntityMember>('delete', url);
  }

}
