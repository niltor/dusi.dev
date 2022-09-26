import { Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { Observable } from 'rxjs';
import { LoginDto } from '../models/auth/login-dto.model';
import { AuthResult } from '../models/auth/auth-result.model';

/**
 * Auth
 */
@Injectable({ providedIn: 'root' })
export class AuthService extends BaseService {
  /**
   * login
   * @param data LoginDto
   */
  login(data: LoginDto): Observable<AuthResult> {
    const url = `/api/Auth`;
    return this.request<AuthResult>('post', url, data);
  }

  /**
   * logout
   * @param id string
   */
  logout(id: string): Observable<boolean> {
    const url = `/api/Auth/${id}`;
    return this.request<boolean>('get', url);
  }

}
