import { Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { Observable } from 'rxjs';
import { EntityMemberFilterDto } from '../models/entity-member/entity-member-filter-dto.model';
import { EntityMemberAddDto } from '../models/entity-member/entity-member-add-dto.model';
import { EntityMemberUpdateDto } from '../models/entity-member/entity-member-update-dto.model';
import { EntityMemberItemDtoPageList } from '../models/entity-member/entity-member-item-dto-page-list.model';
import { EntityMember } from '../models/entity-member/entity-member.model';

/**
 * 实体属性
 */
@Injectable({ providedIn: 'root' })
export class EntityMemberService extends BaseService {
  /**
   * 筛选
   * @param data EntityMemberFilterDto
   */
  filter(data: EntityMemberFilterDto): Observable<EntityMemberItemDtoPageList> {
    const url = `/api/admin/EntityMember/filter`;
    return this.request<EntityMemberItemDtoPageList>('post', url, data);
  }

  /**
   * 新增
   * @param data EntityMemberAddDto
   */
  add(data: EntityMemberAddDto): Observable<EntityMember> {
    const url = `/api/admin/EntityMember`;
    return this.request<EntityMember>('post', url, data);
  }

  /**
   * 更新
   * @param id 
   * @param data EntityMemberUpdateDto
   */
  update(id: string, data: EntityMemberUpdateDto): Observable<EntityMember> {
    const url = `/api/admin/EntityMember/${id}`;
    return this.request<EntityMember>('put', url, data);
  }

  /**
   * 详情
   * @param id 
   */
  getDetail(id: string): Observable<EntityMember> {
    const url = `/api/admin/EntityMember/${id}`;
    return this.request<EntityMember>('get', url);
  }

  /**
   * ⚠删除
   * @param id 
   */
  delete(id: string): Observable<EntityMember> {
    const url = `/api/admin/EntityMember/${id}`;
    return this.request<EntityMember>('delete', url);
  }

}
