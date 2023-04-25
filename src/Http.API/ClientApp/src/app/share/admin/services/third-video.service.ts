import { Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { Observable } from 'rxjs';
import { ThirdVideoFilterDto } from '../models/third-video/third-video-filter-dto.model';
import { ThirdVideoAddDto } from '../models/third-video/third-video-add-dto.model';
import { ThirdVideoUpdateDto } from '../models/third-video/third-video-update-dto.model';
import { ThirdVideoItemDtoPageList } from '../models/third-video/third-video-item-dto-page-list.model';
import { ThirdVideo } from '../models/third-video/third-video.model';

/**
 * 三方视频
 */
@Injectable({ providedIn: 'root' })
export class ThirdVideoService extends BaseService {
  /**
   * 筛选
   * @param data ThirdVideoFilterDto
   */
  filter(data: ThirdVideoFilterDto): Observable<ThirdVideoItemDtoPageList> {
    const url = `/api/admin/ThirdVideo/filter`;
    return this.request<ThirdVideoItemDtoPageList>('post', url, data);
  }

  /**
   * 新增
   * @param data ThirdVideoAddDto
   */
  add(data: ThirdVideoAddDto): Observable<ThirdVideo> {
    const url = `/api/admin/ThirdVideo`;
    return this.request<ThirdVideo>('post', url, data);
  }

  /**
   * 更新
   * @param id 
   * @param data ThirdVideoUpdateDto
   */
  update(id: string, data: ThirdVideoUpdateDto): Observable<ThirdVideo> {
    const url = `/api/admin/ThirdVideo/${id}`;
    return this.request<ThirdVideo>('put', url, data);
  }

  /**
   * 详情
   * @param id 
   */
  getDetail(id: string): Observable<ThirdVideo> {
    const url = `/api/admin/ThirdVideo/${id}`;
    return this.request<ThirdVideo>('get', url);
  }

  /**
   * ⚠删除
   * @param id 
   */
  delete(id: string): Observable<ThirdVideo> {
    const url = `/api/admin/ThirdVideo/${id}`;
    return this.request<ThirdVideo>('delete', url);
  }

}
