/**
 * 实体库列表元素
 */
export interface EntityLibraryItemDto {
  /**
   * 库名称
   */
  name: string;
  /**
   * 库描述
   */
  description?: string | null;
  /**
   * 是否公开
   */
  isPublic: boolean;
  id: string;
  createdTime: Date;
  updatedTime: Date;

}
