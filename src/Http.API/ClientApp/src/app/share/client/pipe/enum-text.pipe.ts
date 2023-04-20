// 该文件自动生成，会被覆盖更新
import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'enumText'
})
export class EnumTextPipe implements PipeTransform {
  transform(value: unknown, type: string): unknown {
    let result = '';
    switch (type) {
      case 'ActionType':
{
  switch (value)
  {
    case 0: result = '其它'; break;
    case 1: result = '登录'; break;
    case 2: result = '添加'; break;
    case 3: result = '更新'; break;
    case 4: result = '删除'; break;
    case 5: result = '审核'; break;
    case 6: result = '导入'; break;
    case 7: result = '导出'; break;
    default: '默认'; break;
  }
}
break;
case 'BlogType':
{
  switch (value)
  {
    case 0: result = '资讯'; break;
    case 1: result = '开源和工具'; break;
    case 2: result = '语言及框架'; break;
    case 3: result = 'AI和数据'; break;
    case 4: result = '云与DevOps'; break;
    case 5: result = '见解与分析'; break;
    case 6: result = '其它'; break;
    default: '默认'; break;
  }
}
break;
case 'LanguageType':
{
  switch (value)
  {
    case 0: result = '中文'; break;
    case 1: result = '英文'; break;
    default: '默认'; break;
  }
}
break;
case 'NewsStatus':
{
  switch (value)
  {
    case 0: result = '默认'; break;
    case 1: result = '公开'; break;
    case 2: result = '内部'; break;
    default: '默认'; break;
  }
}
break;
case 'NewsType':
{
  switch (value)
  {
    case 1: result = '风向标'; break;
    case 2: result = '开源和工具'; break;
    case 3: result = '语言及框架'; break;
    case 4: result = 'AI和数据'; break;
    case 5: result = '云与DevOps'; break;
    case 6: result = '其它'; break;
    default: '默认'; break;
  }
}
break;
case 'TechType':
{
  switch (value)
  {
    case 0: result = '未标记'; break;
    case 1: result = '资讯'; break;
    case 2: result = '发布或更新'; break;
    case 3: result = '重点关注'; break;
    default: '默认'; break;
  }
}
break;

      default:
        break;
    }
    return result;
  }
}