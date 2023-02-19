import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { WorkspaceRoutingModule } from './workspace-routing.module';
import { LayoutComponent } from './layout/layout.component';
import { BlogComponent } from './blog/blog.component';
import { EntityComponent } from './entity/entity.component';
import { ComponentsModule } from 'src/app/components/components.module';
import { ShareModule } from 'src/app/share/share.module';


@NgModule({
  declarations: [
    LayoutComponent,
    BlogComponent,
    EntityComponent
  ],
  imports: [
    ComponentsModule,
    ShareModule,
    WorkspaceRoutingModule
  ]
})
export class WorkspaceModule { }
