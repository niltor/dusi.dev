import { NgModule } from '@angular/core';
import { AccountRoutingModule } from './account-routing.module';
import { InfoComponent } from './info/info.component';
import { PasswordComponent } from './password/password.component';
import { LayoutComponent } from './layout/layout.component';
import { ComponentsModule } from 'src/app/components/components.module';
import { ShareModule } from 'src/app/share/share.module';


@NgModule({
  declarations: [
    InfoComponent,
    PasswordComponent,
    LayoutComponent
  ],
  imports: [
    ComponentsModule,
    ShareModule,
    AccountRoutingModule
  ]
})
export class AccountModule { }
