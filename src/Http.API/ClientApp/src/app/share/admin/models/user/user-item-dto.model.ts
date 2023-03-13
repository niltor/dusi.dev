/**
 * 用户账户列表元素
 */
export interface UserItemDto {
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
