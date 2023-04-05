import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from 'src/app/auth/auth.guard';
import { IndexComponent } from './index/index.component';
import { DetailComponent } from './detail/detail.component';
import { EditComponent } from './edit/edit.component';
import { LayoutComponent } from '../../layout/layout.component';

const routes: Routes = [
  {
    path: '',
    component: LayoutComponent,
    canActivate: [AuthGuard],
    children:
      [
        {
          path: 'entitylib',
          canActivateChild: [AuthGuard],
          children: [
            { path: '', pathMatch: 'full', redirectTo: 'index' },
            { path: 'index', component: IndexComponent },
            { path: 'detail/:id', component: DetailComponent },
            { path: 'edit/:id', component: EditComponent },
          ]
        }
      ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class EntityLibraryRoutingModule { }
