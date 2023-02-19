/**
 * 用户账户添加时请求结构
 */
export interface UserAddDto {
  /**
   * 用户名
   */
  userName?: string | null;
  /**
   * 密码
   */
  password?: string | null;
  email?: string | null;

}
