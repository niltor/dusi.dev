import { Blog } from './blog/blog.model';
import { User } from './user/user.model';
/**
 * 目录
 */
export interface Catalog {
  id: string;
  createdTime: Date;
  updatedTime: Date;
  /**
   * 软删除
   */
  isDeleted: boolean;
  /**
   * 目录名称
   */
  name?: string | null;
  /**
   * 层级
   */
  level: number;
  /**
   * 子目录
   */
  children?: Catalog[] | null;
  /**
   * 目录
   */
  parent?: Catalog | null;
  parentId?: string | null;
  blogs?: Blog[] | null;
  /**
   * 用户账户
   */
  user?: User | null;

}
