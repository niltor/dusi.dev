/**
 * 属性的约束查询筛选
 */
export interface EntityMemberConstraintFilterDto {
  pageIndex: number;
  pageSize: number;
  /**
   * 排序
   */
  orderBy?:  | null;
  entityMemberId?: string | null;

}
