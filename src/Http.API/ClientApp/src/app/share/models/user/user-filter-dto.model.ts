import { Sex } from '../enum/sex.model';
/**
 * 系统用户查询筛选
 */
export interface UserFilterDto {
  pageIndex: number;
  pageSize: number;
  /**
   * 排序
   */
  orderBy?: any | null;
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
