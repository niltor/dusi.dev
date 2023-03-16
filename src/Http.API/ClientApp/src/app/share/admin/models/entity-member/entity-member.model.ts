import { AccessModifier } from '../enum/access-modifier.model';
import { MemberType } from '../enum/member-type.model';
import { EntityMemberConstraint } from '../entity-member-constraint/entity-member-constraint.model';
import { EntityModel } from '../entity-model/entity-model.model';
/**
 * 实体属性
 */
export interface EntityMember {
  id: string;
  createdTime: Date;
  updatedTime: Date;
  /**
   * 软删除
   */
  isDeleted: boolean;
  /**
   * 属性名
   */
  name: string;
  /**
   * 属性注释内容
   */
  comment: string;
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
  isRequired: boolean;
  /**
   * 是否为枚举
   */
  isEnum: boolean;
  /**
   * 是否为列表
   */
  isList: boolean;
  /**
   * 是否为自定义对象
   */
  isObject: boolean;
  /**
   * 是否可赋值
   */
  canSet: boolean;
  /**
   * 是否要初始化
   */
  needInit: boolean;
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
  /**
   * 属性的约束
   */
  constraint?: EntityMemberConstraint | null;
  /**
   * 实体模型类
   */
  objectType?: EntityModel | null;
  objectTypeId?: string | null;
  /**
   * 实体模型类
   */
  entityModel?: EntityModel | null;

}
