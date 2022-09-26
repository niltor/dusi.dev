import { Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { Observable } from 'rxjs';
import { RoleFilterDto } from '../models/role/role-filter-dto.model';
import { RoleAddDto } from '../models/role/role-add-dto.model';
import { RoleUpdateDto } from '../models/role/role-update-dto.model';
import { RoleItemDtoPageList } from '../models/role/role-item-dto-page-list.model';
import { Role } from '../models/role/role.model';

/**
 * Role
 */
@Injectable({ providedIn: 'root' })
export class RoleService extends BaseService {
  /**
   * filter
   * @param data RoleFilterDto
   */
  filter(data: RoleFilterDto): Observable<RoleItemDtoPageList> {
    const url = `/api/Role/filter`;
    return this.request<RoleItemDtoPageList>('post', url, data);
  }

  /**
   * add
   * @param data RoleAddDto
   */
  add(data: RoleAddDto): Observable<Role> {
    const url = `/api/Role`;
    return this.request<Role>('post', url, data);
  }

  /**
   * update
   * @param id string
   * @param data RoleUpdateDto
   */
  update(id: string, data: RoleUpdateDto): Observable<Role> {
    const url = `/api/Role/${id}`;
    return this.request<Role>('put', url, data);
  }

  /**
   * getDetail
   * @param id string
   */
  getDetail(id: string): Observable<Role> {
    const url = `/api/Role/${id}`;
    return this.request<Role>('get', url);
  }

  /**
   * delete
   * @param id string
   */
  delete(id: string): Observable<Role> {
    const url = `/api/Role/${id}`;
    return this.request<Role>('delete', url);
  }

}
