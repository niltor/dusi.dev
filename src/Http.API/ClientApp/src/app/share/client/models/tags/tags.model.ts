import { User } from '../user/user.model';
import { Blog } from '../blog/blog.model';
/**
 * 标签
 */
export interface Tags {
  id: string;
  createdTime: Date;
  updatedTime: Date;
  /**
   * 软删除
   */
  isDeleted: boolean;
  /**
   * 标签名称
   */
  name: string;
  /**
   * 标签颜色
   */
  color?: string | null;
  /**
   * 用户账户
   */
  user?: User | null;
  /**
   * 所属博客
   */
  blogs?: Blog[] | null;

}
