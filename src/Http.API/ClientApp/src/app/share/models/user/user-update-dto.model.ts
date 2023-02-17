import { Sex } from '../enum/sex.model';
/**
 * 系统用户更新时请求结构
 */
export interface UserUpdateDto {
  userName?: string | null;
  /**
   * 真实姓名
   */
  realName?: string | null;
  email?: string | null;
  emailConfirmed?: boolean | null;
  /**
   * 手机号
   */
  phoneNumber?: string | null;
  phoneNumberConfirmed?: boolean | null;
  twoFactorEnabled?: boolean | null;
  lockoutEnd?: Date | null;
  lockoutEnabled?: boolean | null;
  accessFailedCount?: number | null;
  /**
   * 最后登录时间
   */
  lastLoginTime?: Date | null;
  /**
   * 密码重试次数
   */
  retryCount?: number | null;
  /**
   * 头像url
   */
  avatar?: string | null;
  /**
   * 身份证号
   */
  idNumber?: string | null;
  /**
   * 性别
   */
  sex?: Sex | null;

}
