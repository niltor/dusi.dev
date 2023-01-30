import { AccessModifier } from '../enum/access-modifier.model';
import { MemberType } from '../enum/member-type.model';
/**
 * 实体属性查询筛选
 */
export interface EntityMemberFilterDto {
  pageIndex: number;
  pageSize: number;
  /**
   * 排序
   */
  orderBy?: any | null;
  /**
   * 属性名
   */
  name?: string | null;
  /**
   * 属性注释内容
   */
  comment?: string | null;
  /**
   * 默认值，根据类型推断
   */
  defaultValue?: string | null;
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
  dictionaryKeyType?: MemberType | null;
  /**
   * 属性的类型
   */
  dictionaryValueType?: MemberType | null;
  /**
   * 属性的类型
   */
  memberType?: MemberType | null;
  objectTypeId?: string | null;
  constraintId?: string | null;
  entityModelId?: string | null;

}
