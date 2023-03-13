import { EntityMember } from './entity-member.model';
/**
 * 属性的约束
 */
export interface EntityMemberConstraint {
  id: string;
  createdTime: Date;
  updatedTime: Date;
  /**
   * 软删除
   */
  isDeleted: boolean;
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
  /**
   * 实体属性
   */
  entityMember?: EntityMember | null;
  entityMemberId: string;

}
