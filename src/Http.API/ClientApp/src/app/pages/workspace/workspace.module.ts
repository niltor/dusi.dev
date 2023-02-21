import { NgModule } from '@angular/core';

import { WorkspaceRoutingModule } from './workspace-routing.module';
import { LayoutComponent } from './layout/layout.component';
import { ComponentsModule } from 'src/app/components/components.module';
import { ShareModule } from 'src/app/share/share.module';
import { BlogModule } from './blog/blog.module';


@NgModule({
  declarations: [LayoutComponent],
  imports: [
    ComponentsModule,
    ShareModule,
    WorkspaceRoutingModule,
    BlogModule,
  ]
})
export class WorkspaceModule { }
