/**
 * 用户账户更新时请求结构
 */
export interface UserUpdateDto {
  userName?: string | null;
  /**
   * 密码
   */
  password?: string | null;
  email?: string | null;
  emailConfirmed?: boolean | null;

}
