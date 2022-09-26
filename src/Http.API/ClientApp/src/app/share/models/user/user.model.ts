import { Role } from '../role/role.model';
import { SexType } from '../enum/sex-type.model';
/**
 * 系统用户
 */
export interface User {
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
  userName?: string | null;
  /**
   * 真实姓名
   */
  realName?: string | null;
  email?: string | null;
  emailConfirmed: boolean;
  passwordHash?: string | null;
  passwordSalt?: string | null;
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
  roles?: Role[] | null;
  /**
   * 身份证号
   */
  idNumber?: string | null;
  /**
   * 性别
   */
  sex?: SexType | null;

}
