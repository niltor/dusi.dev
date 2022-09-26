import { SexType } from '../enum/sex-type.model';
/**
 * 系统用户添加时请求结构
 */
export interface UserAddDto {
  userName?: string | null;
  /**
   * 真实姓名
   */
  realName?: string | null;
  email?: string | null;
  emailConfirmed: boolean;
  /**
   * 手机号
   */
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
  /**
   * 身份证号
   */
  idNumber?: string | null;
  /**
   * 性别
   */
  sex?: SexType | null;

}
