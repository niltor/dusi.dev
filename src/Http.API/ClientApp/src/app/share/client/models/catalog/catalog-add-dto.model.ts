/**
 * 目录添加时请求结构
 */
export interface CatalogAddDto {
  /**
   * 目录名称
   */
  name?: string | null;
  parentId?: string | null;

}
