import { ActionType } from '../enum/action-type.model';
import { SystemUser } from '../system-user.model';
/**
 * 系统日志添加时请求结构
 */
export interface SystemLogsAddDto {
  /**
   * 操作人名称
   */
  actionUserName: string;
  /**
   * 操作对象名称
   */
  targetName: string;
  /**
   * 操作路由
   */
  route: string;
  actionType?: ActionType | null;
  /**
   * 描述
   */
  description?: string | null;
  /**
   * 系统用户
   */
  systemUser?: SystemUser | null;
  systemUserId: string;

}
