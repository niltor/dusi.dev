import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from 'src/app/auth/auth.guard';
import { AdminLayoutComponent } from 'src/app/components/admin-layout/admin-layout.component';
import { LoginComponent } from './login/login.component';

const routes: Routes = [
    { path: 'system/login', pathMatch: 'full', component: LoginComponent },
    {
        path: 'system',
        component: AdminLayoutComponent,
        // data: { reuse: true },
        canActivate: [AuthGuard],
        canActivateChild: [AuthGuard],
        children: []
    }
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class SystemRoutingModule { }
