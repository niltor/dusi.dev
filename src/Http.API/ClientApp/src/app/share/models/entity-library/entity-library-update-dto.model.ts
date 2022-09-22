/**
 * 实体库更新时请求结构
 */
export interface EntityLibraryUpdateDto {
  name?: string | null;
  /**
   * 库描述
   */
  description?: string | null;
  /**
   * 是否公开
   */
  isPublic?: boolean | null;

}
