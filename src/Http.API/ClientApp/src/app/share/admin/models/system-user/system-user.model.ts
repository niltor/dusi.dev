import { SystemRole } from '../system-role/system-role.model';
import { SystemLogs } from '../system-logs/system-logs.model';
import { Sex } from '../enum/sex.model';
/**
 * 系统用户
 */
export interface SystemUser {
  id: string;
  createdTime: Date;
  updatedTime: Date;
  /**
   * 软删除
   */
  isDeleted: boolean;
  /**
   * 用户名
   */
  userName: string;
  /**
   * 真实姓名
   */
  realName?: string | null;
  email?: string | null;
  emailConfirmed: boolean;
  passwordHash: string;
  passwordSalt: string;
  phoneNumber?: string | null;
  phoneNumberConfirmed: boolean;
  twoFactorEnabled: boolean;
  lockoutEnd?: Date | null;
  lockoutEnabled: boolean;
  accessFailedCount: number;
  /**
   * 最后登录时间
   */
  lastLoginTime?: Date | null;
  /**
   * 密码重试次数
   */
  retryCount: number;
  /**
   * 头像url
   */
  avatar?: string | null;
  systemRoles?: SystemRole[] | null;
  systemLogs?: SystemLogs[] | null;
  /**
   * 性别
   */
  sex?: Sex | null;

}
