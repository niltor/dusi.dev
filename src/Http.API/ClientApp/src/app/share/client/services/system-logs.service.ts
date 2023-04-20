import { Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { Observable } from 'rxjs';
import { SystemLogsFilterDto } from '../models/system-logs/system-logs-filter-dto.model';
import { SystemLogsAddDto } from '../models/system-logs/system-logs-add-dto.model';
import { SystemLogsUpdateDto } from '../models/system-logs/system-logs-update-dto.model';
import { SystemLogsItemDtoPageList } from '../models/system-logs/system-logs-item-dto-page-list.model';
import { SystemLogs } from '../models/system-logs/system-logs.model';

/**
 * 系统日志
 */
@Injectable({ providedIn: 'root' })
export class SystemLogsService extends BaseService {
  /**
   * 筛选
   * @param data SystemLogsFilterDto
   */
  filter(data: SystemLogsFilterDto): Observable<SystemLogsItemDtoPageList> {
    const url = `/api/SystemLogs/filter`;
    return this.request<SystemLogsItemDtoPageList>('post', url, data);
  }

  /**
   * 新增
   * @param data SystemLogsAddDto
   */
  add(data: SystemLogsAddDto): Observable<SystemLogs> {
    const url = `/api/SystemLogs`;
    return this.request<SystemLogs>('post', url, data);
  }

  /**
   * 更新
   * @param id 
   * @param data SystemLogsUpdateDto
   */
  update(id: string, data: SystemLogsUpdateDto): Observable<SystemLogs> {
    const url = `/api/SystemLogs/${id}`;
    return this.request<SystemLogs>('put', url, data);
  }

  /**
   * 详情
   * @param id 
   */
  getDetail(id: string): Observable<SystemLogs> {
    const url = `/api/SystemLogs/${id}`;
    return this.request<SystemLogs>('get', url);
  }

  /**
   * ⚠删除
   * @param id 
   */
  delete(id: string): Observable<SystemLogs> {
    const url = `/api/SystemLogs/${id}`;
    return this.request<SystemLogs>('delete', url);
  }

}
