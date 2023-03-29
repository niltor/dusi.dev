import { NgModule } from '@angular/core';
import { HomeRoutingModule } from './home-routing.module';
import { LoginComponent } from './login/login.component';
import { IndexComponent } from './index/index.component';
import { ShareModule } from 'src/app/share/share.module';
import { NewsComponent } from './news/news.component';
import { BlogComponent } from './blog/blog.component';
import { AboutComponent } from './about/about.component';
import { BlogDetailComponent } from './blog-detail/blog-detail.component';
import { MarkdownModule } from 'ngx-markdown';


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
    MarkdownModule.forRoot()
  ]
})
export class HomeModule { }
