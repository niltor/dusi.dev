/**
 * 三方视频
 */
export interface ThirdVideo {
  id: string;
  createdTime: Date;
  updatedTime: Date;
  /**
   * 软删除
   */
  isDeleted: boolean;
  title: string;
  description?: string | null;
  thumbnailUrl?: string | null;
  originalUrl: string;
  source?: string | null;
  /**
   * 唯一标识
   */
  identity: string;

}
