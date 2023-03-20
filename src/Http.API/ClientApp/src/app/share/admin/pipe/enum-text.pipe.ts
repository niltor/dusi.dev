// ���ļ��Զ����ɣ��ᱻ���Ǹ���
import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'enumText'
})
export class EnumTextPipe implements PipeTransform {
  transform(value: unknown, type: string): unknown {
    let result = '';
    switch (type) {
      case 'LanguageType':
        {
          switch (value) {
            case 0: result = '中文'; break;
            case 1: result = '英文'; break;
            default: break;
          }
        }
        break;
      case 'NewsStatus':
        {
          switch (value) {
            case 0: result = '默认状态'; break;
            case 1: result = '公开'; break;
            case 2: result = '内部'; break;
            default: break;
          }
        }
        break;
      case 'NewsType':
        {
          switch (value) {
            case 1: result = '大公司'; break;
            case 2: result = '开源'; break;
            case 3: result = '语言及框架'; break;
            case 4: result = '大数据和AI'; break;
            case 5: result = 'DevOps'; break;
            case 6: result = '其它'; break;
            default: break;
          }
        }
        break;
      case 'TechType':
        {
          switch (value) {
            case 1: result = '常规资讯'; break;
            case 2: result = '发布或更新'; break;
            case 3: result = '重点关注'; break;
            default: break;
          }
        }
        break;

      default:
        break;
    }
    return result;
  }
}