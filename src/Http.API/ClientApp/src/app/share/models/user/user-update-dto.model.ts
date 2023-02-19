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

}
