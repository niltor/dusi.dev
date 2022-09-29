import { Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { Observable } from 'rxjs';
import { SystemRoleFilterDto } from '../models/system-role/system-role-filter-dto.model';
import { SystemRoleAddDto } from '../models/system-role/system-role-add-dto.model';
import { SystemRoleUpdateDto } from '../models/system-role/system-role-update-dto.model';
import { SystemRoleItemDtoPageList } from '../models/system-role/system-role-item-dto-page-list.model';
import { SystemRole } from '../models/system-role/system-role.model';

/**
 * SystemRole
 */
@Injectable({ providedIn: 'root' })
export class SystemRoleService extends BaseService {
  /**
   * filter
   * @param data SystemRoleFilterDto
   */
  filter(data: SystemRoleFilterDto): Observable<SystemRoleItemDtoPageList> {
    const url = `/api/SystemRole/filter`;
    return this.request<SystemRoleItemDtoPageList>('post', url, data);
  }

  /**
   * add
   * @param data SystemRoleAddDto
   */
  add(data: SystemRoleAddDto): Observable<SystemRole> {
    const url = `/api/SystemRole`;
    return this.request<SystemRole>('post', url, data);
  }

  /**
   * update
   * @param id string
   * @param data SystemRoleUpdateDto
   */
  update(id: string, data: SystemRoleUpdateDto): Observable<SystemRole> {
    const url = `/api/SystemRole/${id}`;
    return this.request<SystemRole>('put', url, data);
  }

  /**
   * getDetail
   * @param id string
   */
  getDetail(id: string): Observable<SystemRole> {
    const url = `/api/SystemRole/${id}`;
    return this.request<SystemRole>('get', url);
  }

  /**
   * delete
   * @param id string
   */
  delete(id: string): Observable<SystemRole> {
    const url = `/api/SystemRole/${id}`;
    return this.request<SystemRole>('delete', url);
  }

}
