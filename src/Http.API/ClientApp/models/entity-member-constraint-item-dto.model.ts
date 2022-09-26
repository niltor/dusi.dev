/**
 * 属性的约束列表元素
 */
export interface EntityMemberConstraintItemDto {
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
  entityMemberId: string;
  id: string;
  createdTime: Date;
  updatedTime: Date;
  /**
   * 软删除
   */
  isDeleted: boolean;

}
