/**
 * 三方视频添加时请求结构
 */
export interface ThirdVideoAddDto {
  title: string;
  description?: string | null;
  thumbnailUrl?: string | null;
  originalUrl: string;
  source?: string | null;

}
