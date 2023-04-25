import { SystemLogsItemDto } from '../system-logs/system-logs-item-dto.model';
export interface SystemLogsItemDtoPageList {
  count: number;
  data?: SystemLogsItemDto[];
  pageIndex: number;

}
