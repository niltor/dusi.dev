import { NgModule } from '@angular/core';
import { HomeRoutingModule } from './home-routing.module';
import { LoginComponent } from './login/login.component';
import { IndexComponent } from './index/index.component';
import { ShareModule } from 'src/app/share/share.module';
import { NewsComponent } from './news/news.component';
import { BlogComponent } from './blog/blog.component';
import { AboutComponent } from './about/about.component';
import { BlogDetailComponent } from './blog-detail/blog-detail.component';
import { MarkdownModule, MarkedOptions, MarkedRenderer } from 'ngx-markdown';


@NgModule({
  declarations: [
    LoginComponent,
    IndexComponent,
    NewsComponent,
    BlogComponent,
    AboutComponent,
    BlogDetailComponent
  ],
  imports: [
    ShareModule,
    HomeRoutingModule,
    MarkdownModule.forRoot({
      markedOptions: {
        provide: MarkedOptions,
        useFactory: markedOptionsFactory
      }
    })

  ]
})
export class HomeModule { }
export function markedOptionsFactory(): MarkedOptions {
  const renderer = new MarkedRenderer();
  renderer.blockquote = (text: string) => {
    return '<blockquote class="blockquote"><p>' + text + '</p></blockquote>';
  };
  renderer.code = (code: string) => {
    return '<code class="inline-code">' + code + '</code>'
  }

  return {
    renderer: renderer,
    gfm: true,
    breaks: false,
    pedantic: false,
    smartLists: true,
    smartypants: false,
  };
}