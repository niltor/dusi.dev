import { UserType } from '../enum/user-type.model';
import { EntityLibrary } from '../entity-library/entity-library.model';
import { EntityModel } from '../entity-model/entity-model.model';
import { Blog } from '../blog/blog.model';
import { Catalog } from '../catalog.model';
import { Tags } from '../tags.model';
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
  userName: string;
  userType?: UserType | null;
  email?: string | null;
  emailConfirmed: boolean;
  passwordHash: string;
  passwordSalt: string;
  entityLibraries?: EntityLibrary[] | null;
  entityModels?: EntityModel[] | null;
  blogs?: Blog[] | null;
  catalogs?: Catalog[] | null;
  tags?: Tags[] | null;

}
