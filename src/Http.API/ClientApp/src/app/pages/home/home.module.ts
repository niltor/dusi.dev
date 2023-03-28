import { NgModule } from '@angular/core';
import { HomeRoutingModule } from './home-routing.module';
import { LoginComponent } from './login/login.component';
import { IndexComponent } from './index/index.component';
import { ShareModule } from 'src/app/share/share.module';
import { NewsComponent } from './news/news.component';
import { EnumTextPipe } from 'src/app/share/admin/pipe/enum-text.pipe';
import { BlogComponent } from './blog/blog.component';
import { AboutComponent } from './about/about.component';
import { BlogDetailComponent } from './blog-detail/blog-detail.component';


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
    HomeRoutingModule
  ]
})
export class HomeModule { }
