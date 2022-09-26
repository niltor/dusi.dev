/**
 * 角色列表元素
 */
export interface RoleItemDto {
  name?: string | null;
  /**
   * 角色名，系统标识
   */
  nameValue?: string | null;
  /**
   * 是否系统内置,系统内置不可删除
   */
  isSystem: boolean;
  /**
   * 图标
   */
  icon?: string | null;
  id: string;
  createdTime: Date;
  updatedTime: Date;
  /**
   * 软删除
   */
  isDeleted: boolean;

}
