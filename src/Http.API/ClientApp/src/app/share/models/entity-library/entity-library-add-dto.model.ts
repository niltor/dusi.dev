/**
 * 实体库添加时请求结构
 */
export interface EntityLibraryAddDto {
  name?: string | null;
  /**
   * 库描述
   */
  description?: string | null;
  /**
   * 是否公开
   */
  isPublic: boolean;

}
