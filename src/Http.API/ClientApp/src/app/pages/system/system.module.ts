import { NgModule } from '@angular/core';
import { SystemRoutingModule } from './system-routing.module';
import { ShareModule } from 'src/app/share/share.module';
import { ComponentsModule } from 'src/app/components/components.module';
import { RoleModule } from './role/role.module';
import { UserModule } from './user/user.module';
import { ThirdNewsModule } from './third-news/third-news.module';
import { MemberModule } from './member/member.module';
import { OpenSourceProductModule } from './open-source-product/open-source-product.module';

@NgModule({
  declarations: [],
  imports: [
    ComponentsModule,
    ShareModule,
    SystemRoutingModule,
    RoleModule,
    UserModule,
    MemberModule,
    ThirdNewsModule,
    OpenSourceProductModule
  ],
  exports: [
  ]
})
export class SystemModule { }
