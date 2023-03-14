import { Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { Observable } from 'rxjs';
import { CatalogFilterDto } from '../models/catalog/catalog-filter-dto.model';
import { CatalogAddDto } from '../models/catalog/catalog-add-dto.model';
import { CatalogUpdateDto } from '../models/catalog/catalog-update-dto.model';
import { CatalogItemDtoPageList } from '../models/catalog/catalog-item-dto-page-list.model';
import { Catalog } from '../models/catalog/catalog.model';

/**
 * 目录
 */
@Injectable({ providedIn: 'root' })
export class CatalogService extends BaseService {
  /**
   * 筛选
   * @param data CatalogFilterDto
   */
  filter(data: CatalogFilterDto): Observable<CatalogItemDtoPageList> {
    const url = `/api/Catalog/filter`;
    return this.request<CatalogItemDtoPageList>('post', url, data);
  }

  /**
   * 获取树型结构
   */
  getTree(): Observable<Catalog[]> {
    const url = `/api/Catalog/tree`;
    return this.request<Catalog[]>('get', url);
  }

  /**
   * 新增
   * @param data CatalogAddDto
   */
  add(data: CatalogAddDto): Observable<Catalog> {
    const url = `/api/Catalog`;
    return this.request<Catalog>('post', url, data);
  }

  /**
   * 更新
   * @param id 
   * @param data CatalogUpdateDto
   */
  update(id: string, data: CatalogUpdateDto): Observable<Catalog> {
    const url = `/api/Catalog/${id}`;
    return this.request<Catalog>('put', url, data);
  }

  /**
   * 详情
   * @param id 
   */
  getDetail(id: string): Observable<Catalog> {
    const url = `/api/Catalog/${id}`;
    return this.request<Catalog>('get', url);
  }

  /**
   * ⚠删除
   * @param id 
   */
  delete(id: string): Observable<Catalog> {
    const url = `/api/Catalog/${id}`;
    return this.request<Catalog>('delete', url);
  }

}
