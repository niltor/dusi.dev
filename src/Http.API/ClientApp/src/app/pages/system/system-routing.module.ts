import { NgModule } from '@angular/core';
import { RouteReuseStrategy, RouterModule, Routes } from '@angular/router';
import { AuthGuard } from 'src/app/auth/auth.guard';
import { AdminLayoutComponent } from 'src/app/components/admin-layout/admin-layout.component';
import { CustomRouteReuseStrategy } from 'src/app/custom-route-strategy';

const routes: Routes = [
  {
    path: '',
    component: AdminLayoutComponent,
    canActivate: [AuthGuard],
    canActivateChild: [AuthGuard],
    data: { reuse: true },
    children:
      [
        // { path: '', pathMatch: 'full', redirectTo: 'resource/index' },
      ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
  providers: [{
    provide: RouteReuseStrategy,
    useClass: CustomRouteReuseStrategy
  }]
})
export class SystemRoutingModule { }
