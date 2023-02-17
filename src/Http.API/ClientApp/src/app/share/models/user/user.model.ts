import { EntityLibrary } from '../entity-library/entity-library.model';
import { EntityModel } from '../entity-model/entity-model.model';
import { Blog } from '../blog/blog.model';
/**
 * 用户账户
 */
export interface User {
  id: string;
  createdTime: Date;
  updatedTime: Date;
  /**
   * 软删除
   */
  isDeleted: boolean;
  /**
   * 用户名
   */
  userName?: string | null;
  email?: string | null;
  emailConfirmed: boolean;
  passwordHash?: string | null;
  passwordSalt?: string | null;
  entityLibraries?: EntityLibrary[] | null;
  entityModels?: EntityModel[] | null;
  blogs?: Blog[] | null;

}
