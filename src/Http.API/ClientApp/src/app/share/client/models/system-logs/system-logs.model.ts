import { ActionType } from '../enum/action-type.model';
import { SystemUser } from '../system-user.model';
/**
 * 系统日志
 */
export interface SystemLogs {
  id: string;
  createdTime: Date;
  updatedTime: Date;
  /**
   * 软删除
   */
  isDeleted: boolean;
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

}
