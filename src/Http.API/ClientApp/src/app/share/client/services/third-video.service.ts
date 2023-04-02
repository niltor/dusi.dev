import { Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { Observable } from 'rxjs';
import { ThirdVideoFilterDto } from '../models/third-video/third-video-filter-dto.model';
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
    const url = `/api/ThirdVideo/filter`;
    return this.request<ThirdVideoItemDtoPageList>('post', url, data);
  }

  /**
   * 详情
   * @param id 
   */
  getDetail(id: string): Observable<ThirdVideo> {
    const url = `/api/ThirdVideo/${id}`;
    return this.request<ThirdVideo>('get', url);
  }

}
