import { Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { Observable } from 'rxjs';
import { OpenSourceProductFilterDto } from '../models/open-source-product/open-source-product-filter-dto.model';
import { OpenSourceProductAddDto } from '../models/open-source-product/open-source-product-add-dto.model';
import { OpenSourceProductUpdateDto } from '../models/open-source-product/open-source-product-update-dto.model';
import { OpenSourceProductItemDtoPageList } from '../models/open-source-product/open-source-product-item-dto-page-list.model';
import { OpenSourceProduct } from '../models/open-source-product/open-source-product.model';

/**
 * 开源作品
 */
@Injectable({ providedIn: 'root' })
export class OpenSourceProductService extends BaseService {
  /**
   * 筛选
   * @param data OpenSourceProductFilterDto
   */
  filter(data: OpenSourceProductFilterDto): Observable<OpenSourceProductItemDtoPageList> {
    const url = `/api/admin/OpenSourceProduct/filter`;
    return this.request<OpenSourceProductItemDtoPageList>('post', url, data);
  }

  /**
   * 新增
   * @param data OpenSourceProductAddDto
   */
  add(data: OpenSourceProductAddDto): Observable<OpenSourceProduct> {
    const url = `/api/admin/OpenSourceProduct`;
    return this.request<OpenSourceProduct>('post', url, data);
  }

  /**
   * 更新
   * @param id 
   * @param data OpenSourceProductUpdateDto
   */
  update(id: string, data: OpenSourceProductUpdateDto): Observable<OpenSourceProduct> {
    const url = `/api/admin/OpenSourceProduct/${id}`;
    return this.request<OpenSourceProduct>('put', url, data);
  }

  /**
   * 详情
   * @param id 
   */
  getDetail(id: string): Observable<OpenSourceProduct> {
    const url = `/api/admin/OpenSourceProduct/${id}`;
    return this.request<OpenSourceProduct>('get', url);
  }

  /**
   * ⚠删除
   * @param id 
   */
  delete(id: string): Observable<OpenSourceProduct> {
    const url = `/api/admin/OpenSourceProduct/${id}`;
    return this.request<OpenSourceProduct>('delete', url);
  }

}
