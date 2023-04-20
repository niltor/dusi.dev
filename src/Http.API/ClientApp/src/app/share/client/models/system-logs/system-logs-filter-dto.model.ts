import { ActionType } from '../enum/action-type.model';
/**
 * 系统日志查询筛选
 */
export interface SystemLogsFilterDto {
  pageIndex: number;
  /**
   * 默认最大1000
   */
  pageSize: number;
  /**
   * 排序
   */
  orderBy?: any | null;
  /**
   * 操作人名称
   */
  actionUserName?: string | null;
  /**
   * 操作对象名称
   */
  targetName?: string | null;
  /**
   * 操作路由
   */
  route?: string | null;
  actionType?: ActionType | null;
  /**
   * 描述
   */
  description?: string | null;
  systemUserId?: string | null;

}
