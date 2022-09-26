import { AccessModifier } from '../enum/access-modifier.model';
/**
 * 实体模型类查询筛选
 */
export interface EntityModelFilterDto {
  pageIndex?: number | null;
  pageSize?: number | null;
  /**
   * 排序
   */
  orderBy?:  | null;
  /**
   * 实体类名
   */
  name?: string | null;
  /**
   * 实体注释内容
   */
  comment?: string | null;
  /**
   * 访问修饰符
   */
  accessModifier?: AccessModifier | null;
  parentEntityId?: string | null;
  entityLibraryId?: string | null;

}
