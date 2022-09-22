/**
 * 角色添加时请求结构
 */
export interface RoleAddDto {
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

}
