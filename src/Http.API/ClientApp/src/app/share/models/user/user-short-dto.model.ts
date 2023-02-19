/**
 * 用户账户概要
 */
export interface UserShortDto {
  /**
   * 用户名
   */
  userName?: string | null;
  email?: string | null;
  emailConfirmed: boolean;
  id: string;
  createdTime: Date;
  updatedTime: Date;

}
