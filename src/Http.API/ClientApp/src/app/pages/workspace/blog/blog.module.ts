import { NgModule } from '@angular/core';
import { BlogRoutingModule } from './blog-routing.module';
import { ShareModule } from 'src/app/share/share.module';
import { ComponentsModule } from 'src/app/components/components.module';
import { IndexComponent } from './index/index.component';
import { DetailComponent } from './detail/detail.component';
import { AddComponent } from './add/add.component';
import { EditComponent } from './edit/edit.component';
import { SettingComponent } from './setting/setting.component';
import { CatalogModule } from './catalog/catalog.module';
import { TagModule } from './tag/tag.module';
import { MarkdownModule, MarkedOptions, MarkedRenderer } from 'ngx-markdown';

@NgModule({
  declarations: [IndexComponent, DetailComponent, AddComponent, EditComponent, SettingComponent],
  imports: [
    ComponentsModule,
    ShareModule,
    BlogRoutingModule,
    CatalogModule,
    TagModule,
    MarkdownModule.forRoot({
      markedOptions: {
        provide: MarkedOptions,
        useFactory: markedOptionsFactory
      }
    })
  ]
})
export class BlogModule { }

export function markedOptionsFactory(): MarkedOptions {
  const renderer = new MarkedRenderer();
  renderer.blockquote = (text: string) => {
    return '<blockquote class="blockquote"><p>' + text + '</p></blockquote>';
  };

  return {
    renderer: renderer,
    gfm: true,
    breaks: false,
    pedantic: false,
    smartLists: true,
    smartypants: false,
  };
}
