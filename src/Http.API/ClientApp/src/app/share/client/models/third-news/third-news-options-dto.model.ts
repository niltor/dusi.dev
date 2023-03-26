import { EnumDictionary } from '../enum-dictionary.model';
/**
 * 枚举类型选项
 */
export interface ThirdNewsOptionsDto {
  newsType?: EnumDictionary[] | null;
  techType?: EnumDictionary[] | null;

}
