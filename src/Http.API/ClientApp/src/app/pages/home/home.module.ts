import { NgModule } from '@angular/core';
import { HomeRoutingModule } from './home-routing.module';
import { LoginComponent } from './login/login.component';
import { IndexComponent } from './index/index.component';
import { ShareModule } from 'src/app/share/share.module';
import { NewsComponent } from './news/news.component';
import { EnumTextPipe } from 'src/app/share/admin/pipe/enum-text.pipe';


@NgModule({
  declarations: [
    LoginComponent,
    IndexComponent,
    NewsComponent
  ],
  imports: [
    ShareModule,
    HomeRoutingModule
  ]
})
export class HomeModule { }
