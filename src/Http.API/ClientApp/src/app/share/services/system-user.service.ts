import { Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { Observable } from 'rxjs';
import { SystemUserFilterDto } from '../models/system-user/system-user-filter-dto.model';
import { SystemUserAddDto } from '../models/system-user/system-user-add-dto.model';
import { SystemUserUpdateDto } from '../models/system-user/system-user-update-dto.model';
import { SystemUserItemDtoPageList } from '../models/system-user/system-user-item-dto-page-list.model';
import { SystemUser } from '../models/system-user/system-user.model';

/**
 * SystemUser
 */
@Injectable({ providedIn: 'root' })
export class SystemUserService extends BaseService {
  /**
   * filter
   * @param data SystemUserFilterDto
   */
  filter(data: SystemUserFilterDto): Observable<SystemUserItemDtoPageList> {
    const url = `/api/SystemUser/filter`;
    return this.request<SystemUserItemDtoPageList>('post', url, data);
  }

  /**
   * add
   * @param data SystemUserAddDto
   */
  add(data: SystemUserAddDto): Observable<SystemUser> {
    const url = `/api/SystemUser`;
    return this.request<SystemUser>('post', url, data);
  }

  /**
   * update
   * @param id string
   * @param data SystemUserUpdateDto
   */
  update(id: string, data: SystemUserUpdateDto): Observable<SystemUser> {
    const url = `/api/SystemUser/${id}`;
    return this.request<SystemUser>('put', url, data);
  }

  /**
   * getDetail
   * @param id string
   */
  getDetail(id: string): Observable<SystemUser> {
    const url = `/api/SystemUser/${id}`;
    return this.request<SystemUser>('get', url);
  }

  /**
   * delete
   * @param id string
   */
  delete(id: string): Observable<SystemUser> {
    const url = `/api/SystemUser/${id}`;
    return this.request<SystemUser>('delete', url);
  }

}
