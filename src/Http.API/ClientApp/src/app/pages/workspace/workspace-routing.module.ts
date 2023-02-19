import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from 'src/app/auth/auth.guard';
import { BlogComponent } from './blog/blog.component';
import { EntityComponent } from './entity/entity.component';
import { LayoutComponent } from './layout/layout.component';

const routes: Routes = [
  {
    path: 'workspace',
    canActivate: [AuthGuard],
    canActivateChild: [AuthGuard],
    component: LayoutComponent,
    children: [
      { path: '', redirectTo: 'blog', pathMatch: 'full' },
      { path: 'blog', component: BlogComponent },
      { path: 'entity', component: EntityComponent }
    ]
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class WorkspaceRoutingModule { }
