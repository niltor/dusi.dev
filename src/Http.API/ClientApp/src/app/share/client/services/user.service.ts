import { Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { Observable } from 'rxjs';
import { LoginDto } from '../models/user/login-dto.model';
import { UserAddDto } from '../models/user/user-add-dto.model';
import { UserUpdateDto } from '../models/user/user-update-dto.model';
import { AuthResult } from '../models/user/auth-result.model';
import { User } from '../models/user/user.model';

/**
 * 用户账户
 */
@Injectable({ providedIn: 'root' })
export class UserService extends BaseService {
  /**
   * 登录获取Token
   * @param data LoginDto
   */
  login(data: LoginDto): Observable<AuthResult> {
    const url = `/api/User/login`;
    return this.request<AuthResult>('post', url, data);
  }

  /**
   * 新增
   * @param data UserAddDto
   */
  signUp(data: UserAddDto): Observable<User> {
    const url = `/api/User`;
    return this.request<User>('post', url, data);
  }

  /**
   * 更新
   * @param id 
   * @param data UserUpdateDto
   */
  update(id: string, data: UserUpdateDto): Observable<User> {
    const url = `/api/User/${id}`;
    return this.request<User>('put', url, data);
  }

  /**
   * 详情
   * @param id 
   */
  getDetail(id: string): Observable<User> {
    const url = `/api/User/${id}`;
    return this.request<User>('get', url);
  }

  /**
   * ⚠删除
   * @param id 
   */
  delete(id: string): Observable<User> {
    const url = `/api/User/${id}`;
    return this.request<User>('delete', url);
  }

  /**
   * 修改密码
   * @param password 
   */
  changeMyPassword(password?: string): Observable<boolean> {
    const url = `/api/User/password?password=${password}`;
    return this.request<boolean>('put', url);
  }

}
