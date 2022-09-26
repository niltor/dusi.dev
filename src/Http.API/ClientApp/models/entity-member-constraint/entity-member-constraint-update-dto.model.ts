/**
 * 属性的约束更新时请求结构
 */
export interface EntityMemberConstraintUpdateDto {
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
