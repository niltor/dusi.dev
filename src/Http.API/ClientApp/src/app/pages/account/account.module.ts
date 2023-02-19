import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AccountRoutingModule } from './account-routing.module';
import { InfoComponent } from './info/info.component';
import { PasswordComponent } from './password/password.component';
import { LayoutComponent } from './layout/layout.component';


@NgModule({
  declarations: [
    InfoComponent,
    PasswordComponent,
    LayoutComponent
  ],
  imports: [
    CommonModule,
    AccountRoutingModule
  ]
})
export class AccountModule { }
