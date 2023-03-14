/**
 * 目录列表元素
 */
export interface CatalogItemDto {
  /**
   * 目录名称
   */
  name?: string | null;
  /**
   * 层级
   */
  level: number;
  parentId?: string | null;
  id: string;
  createdTime: Date;

}
