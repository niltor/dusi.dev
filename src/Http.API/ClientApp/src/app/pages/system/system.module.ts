import { NgModule } from '@angular/core';
import { SystemRoutingModule } from './system-routing.module';
import { ShareModule } from 'src/app/share/share.module';
import { ComponentsModule } from 'src/app/components/components.module';
import { RouteReuseStrategy } from '@angular/router';
import { CustomRouteReuseStrategy } from 'src/app/custom-route-strategy';
import { RoleModule } from './role/role.module';
import { UserModule } from './user/user.module';
import { MemberModule } from './member/member.module';
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
    MemberModule,
    EntityLibraryModule,
    EntityModelModule
  ],
  providers: [{
    provide: RouteReuseStrategy,
    useClass: CustomRouteReuseStrategy
  }]
})
export class SystemModule { }
