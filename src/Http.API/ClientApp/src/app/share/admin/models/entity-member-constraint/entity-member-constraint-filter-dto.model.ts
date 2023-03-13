/**
 * 属性的约束查询筛选
 */
export interface EntityMemberConstraintFilterDto {
  pageIndex: number;
  pageSize: number;
  /**
   * 排序
   */
  orderBy?: any | null;
  /**
   * 字符串最长长度
   */
  maxLength?: number | null;
  /**
   * 字符串最短长度
   */
  minLength?: number | null;
  /**
   * 固定长度
   */
  length?: number | null;
  /**
   * 数值最小
   */
  min?: number | null;
  /**
   * 数值最大
   */
  max?: number | null;
  entityMemberId?: string | null;

}
