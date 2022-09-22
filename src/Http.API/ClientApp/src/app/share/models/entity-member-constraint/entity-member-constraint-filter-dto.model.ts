/**
 * 属性的约束查询筛选
 */
export interface EntityMemberConstraintFilterDto {
  pageIndex?: number | null;
  pageSize?: number | null;
  /**
   * 排序
   */
  orderBy?:  | null;
  entityMemberId?: string | null;

}
