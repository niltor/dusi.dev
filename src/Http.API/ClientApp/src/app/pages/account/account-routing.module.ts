import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from 'src/app/auth/auth.guard';
import { InfoComponent } from './info/info.component';
import { PasswordComponent } from './password/password.component';

const routes: Routes = [
  {
    path: 'account',
    canActivate: [AuthGuard],
    canActivateChild: [AuthGuard],
    children: [
      { path: '', redirectTo: 'info', pathMatch: 'full' },
      { path: 'info', component: InfoComponent },
      { path: 'password', component: PasswordComponent }
    ]
  },

];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AccountRoutingModule { }
