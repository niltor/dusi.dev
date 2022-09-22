import { AccessModifier } from '../enum/access-modifier.model';
import { MemberType } from '../enum/member-type.model';
/**
 * 实体属性查询筛选
 */
export interface EntityMemberFilterDto {
  pageIndex?: number | null;
  pageSize?: number | null;
  /**
   * 排序
   */
  orderBy?:  | null;
  /**
   * 属性名
   */
  name?: string | null;
  /**
   * 属性注释内容
   */
  comment?: string | null;
  /**
   * 访问修饰符
   */
  accessModifier?: AccessModifier | null;
  /**
   * 是否必须
   */
  isRequired?: boolean | null;
  /**
   * 是否为枚举
   */
  isEnum?: boolean | null;
  /**
   * 是否为列表
   */
  isList?: boolean | null;
  /**
   * 是否为自定义对象
   */
  isObject?: boolean | null;
  /**
   * 是否可赋值
   */
  canSet?: boolean | null;
  /**
   * 是否要初始化
   */
  needInit?: boolean | null;
  /**
   * 属性的类型
   */
  memberType?: MemberType | null;
  constraintId?: string | null;
  objectTypeId?: string | null;
  entityModelId?: string | null;

}
