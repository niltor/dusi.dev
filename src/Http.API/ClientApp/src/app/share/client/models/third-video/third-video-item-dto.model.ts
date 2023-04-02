/**
 * 三方视频列表元素
 */
export interface ThirdVideoItemDto {
  title: string;
  description?: string | null;
  thumbnailUrl?: string | null;
  originalUrl: string;
  source?: string | null;
  id: string;
  createdTime: Date;
  updatedTime: Date;
  /**
   * 唯一标识
   */
  identity: string;

}
