import { Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { Observable } from 'rxjs';
import { ThirdNewsFilterDto } from '../models/third-news/third-news-filter-dto.model';
import { ThirdNewsItemDtoPageList } from '../models/third-news/third-news-item-dto-page-list.model';
import { ThirdNews } from '../models/third-news/third-news.model';

/**
 * 资讯
 */
@Injectable({ providedIn: 'root' })
export class ThirdNewsService extends BaseService {
  /**
   * 筛选
   * @param data ThirdNewsFilterDto
   */
  filter(data: ThirdNewsFilterDto): Observable<ThirdNewsItemDtoPageList> {
    const url = `/api/ThirdNews/filter`;
    return this.request<ThirdNewsItemDtoPageList>('post', url, data);
  }

  /**
   * 详情
   * @param id 
   */
  getDetail(id: string): Observable<ThirdNews> {
    const url = `/api/ThirdNews/${id}`;
    return this.request<ThirdNews>('get', url);
  }

}
