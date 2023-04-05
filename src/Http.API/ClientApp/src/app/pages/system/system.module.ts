import { NgModule } from '@angular/core';
import { SystemRoutingModule } from './system-routing.module';
import { ShareModule } from 'src/app/share/share.module';
import { ComponentsModule } from 'src/app/components/components.module';
import { RoleModule } from './role/role.module';
import { UserModule } from './user/user.module';
import { ThirdNewsModule } from './third-news/third-news.module';
import { MemberModule } from './member/member.module';

@NgModule({
  declarations: [],
  imports: [
    ComponentsModule,
    ShareModule,
    SystemRoutingModule,
    RoleModule,
    UserModule,
    MemberModule,
    ThirdNewsModule
  ],
  exports: [
  ]
})
export class SystemModule { }
