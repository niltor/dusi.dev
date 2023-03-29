import { NgModule } from '@angular/core';
import { SystemRoutingModule } from './system-routing.module';
import { ShareModule } from 'src/app/share/share.module';
import { ComponentsModule } from 'src/app/components/components.module';
import { RoleModule } from './role/role.module';
import { UserModule } from './user/user.module';
import { ThirdNewsModule } from './third-news/third-news.module';
import { EntityLibraryModule } from './entity-library/entity-library.module';
import { EntityModelModule } from './entity-model/entity-model.module';

@NgModule({
  declarations: [],
  imports: [
    ComponentsModule,
    ShareModule,
    SystemRoutingModule,
    RoleModule,
    UserModule,
    ThirdNewsModule,
    EntityLibraryModule,
    EntityModelModule
  ],
  exports: [
  ]
})
export class SystemModule { }
