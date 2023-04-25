/**
 * 三方视频更新时请求结构
 */
export interface ThirdVideoUpdateDto {
  title: string;
  description?: string | null;
  thumbnailUrl?: string | null;
  originalUrl: string;
  source?: string | null;

}
