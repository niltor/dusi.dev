import { ThirdNews } from './third-news/third-news.model';
/**
 * 新闻标签
 */
export interface NewsTags {
  id: string;
  createdTime: Date;
  updatedTime: Date;
  /**
   * 软删除
   */
  isDeleted: boolean;
  name: string;
  color?: string | null;
  /**
   * 第三方资讯
   */
  thirdNews?: ThirdNews | null;

}
