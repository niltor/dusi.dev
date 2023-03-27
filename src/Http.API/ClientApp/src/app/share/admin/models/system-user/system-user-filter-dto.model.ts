import { Sex } from '../enum/sex.model';
/**
 * 系统用户查询筛选
 */
export interface SystemUserFilterDto {
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
   * 用户名
   */
  userName?: string | null;
  emailConfirmed?: boolean | null;
  phoneNumberConfirmed?: boolean | null;
  twoFactorEnabled?: boolean | null;
  lockoutEnabled?: boolean | null;
  accessFailedCount?: number | null;
  /**
   * 密码重试次数
   */
  retryCount?: number | null;
  /**
   * 性别
   */
  sex?: Sex | null;

}
