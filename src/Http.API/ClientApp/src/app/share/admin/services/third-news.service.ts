import { Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { Observable } from 'rxjs';
import { ThirdNewsFilterDto } from '../models/third-news/third-news-filter-dto.model';
import { ThirdNewsAddDto } from '../models/third-news/third-news-add-dto.model';
import { ThirdNewsUpdateDto } from '../models/third-news/third-news-update-dto.model';
import { ThirdNewsBatchUpdateDto } from '../models/third-news/third-news-batch-update-dto.model';
import { ThirdNewsItemDtoPageList } from '../models/third-news/third-news-item-dto-page-list.model';
import { ThirdNews } from '../models/third-news/third-news.model';

/**
 * 资讯管理
 */
@Injectable({ providedIn: 'root' })
export class ThirdNewsService extends BaseService {
  /**
   * 筛选
   * @param data ThirdNewsFilterDto
   */
  filter(data: ThirdNewsFilterDto): Observable<ThirdNewsItemDtoPageList> {
    const url = `/api/admin/ThirdNews/filter`;
    return this.request<ThirdNewsItemDtoPageList>('post', url, data);
  }

  /**
   * 新增
   * @param data ThirdNewsAddDto
   */
  add(data: ThirdNewsAddDto): Observable<ThirdNews> {
    const url = `/api/admin/ThirdNews`;
    return this.request<ThirdNews>('post', url, data);
  }

  /**
   * 更新
   * @param id 
   * @param data ThirdNewsUpdateDto
   */
  update(id: string, data: ThirdNewsUpdateDto): Observable<ThirdNews> {
    const url = `/api/admin/ThirdNews/${id}`;
    return this.request<ThirdNews>('put', url, data);
  }

  /**
   * 详情
   * @param id 
   */
  getDetail(id: string): Observable<ThirdNews> {
    const url = `/api/admin/ThirdNews/${id}`;
    return this.request<ThirdNews>('get', url);
  }

  /**
   * ⚠删除
   * @param id 
   */
  delete(id: string): Observable<ThirdNews> {
    const url = `/api/admin/ThirdNews/${id}`;
    return this.request<ThirdNews>('delete', url);
  }

  /**
   * 批量操作
   * @param data ThirdNewsBatchUpdateDto
   */
  batchUpdate(data: ThirdNewsBatchUpdateDto): Observable<boolean> {
    const url = `/api/admin/ThirdNews/batchUpdate`;
    return this.request<boolean>('put', url, data);
  }

}
