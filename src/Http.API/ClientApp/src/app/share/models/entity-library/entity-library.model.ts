import { EntityModel } from '../entity-model/entity-model.model';
import { User } from '../user/user.model';
/**
 * 实体库
 */
export interface EntityLibrary {
  id: string;
  createdTime: Date;
  updatedTime: Date;
  /**
   * 软删除
   */
  isDeleted: boolean;
  /**
   * 库名称
   */
  name?: string | null;
  /**
   * 库描述
   */
  description?: string | null;
  /**
   * 是否公开
   */
  isPublic: boolean;
  /**
   * 包含的实体类
   */
  entityModels?: EntityModel[] | null;
  /**
   * 系统用户
   */
  user?: User | null;

}
