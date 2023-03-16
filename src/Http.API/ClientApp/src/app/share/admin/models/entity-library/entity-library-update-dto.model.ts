import { EntityModel } from '../entity-model/entity-model.model';
import { SystemUser } from '../system-user/system-user.model';
/**
 * 实体库更新时请求结构
 */
export interface EntityLibraryUpdateDto {
  /**
   * 库名称
   */
  name: string;
  /**
   * 库描述
   */
  description?: string | null;
  /**
   * 是否公开
   */
  isPublic?: boolean | null;
  /**
   * 包含的实体类
   */
  entityModels?: EntityModel[] | null;
  /**
   * 系统用户
   */
  user?: SystemUser | null;
  userId: string;

}
