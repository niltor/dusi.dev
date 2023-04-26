import { Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { Observable } from 'rxjs';
import { OpenSourceProductFilterDto } from '../models/open-source-product/open-source-product-filter-dto.model';
import { OpenSourceProductItemDtoPageList } from '../models/open-source-product/open-source-product-item-dto-page-list.model';

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
    const url = `/api/OpenSourceProduct/filter`;
    return this.request<OpenSourceProductItemDtoPageList>('post', url, data);
  }

}
