export interface AuthResult {
  id: string;
  /**
   * 用户名
   */
  username?: string | null;
  roles?: string[] | null;
  /**
   * token
   */
  token?: string | null;

}
