/**
 * 用户账户添加时请求结构
 */
export interface UserAddDto {
  /**
   * 用户名
   */
  userName: string;
  /**
   * 密码
   */
  password: string;
  email?: string | null;

}
