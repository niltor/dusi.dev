import { NgModule } from '@angular/core';
import { EntityLibraryRoutingModule } from './entity-library-routing.module';
import { ShareModule } from 'src/app/share/share.module';
import { ComponentsModule } from 'src/app/components/components.module';
import { IndexComponent } from './index/index.component';
import { DetailComponent } from './detail/detail.component';
import { EditComponent } from './edit/edit.component';

@NgModule({
  declarations: [IndexComponent, DetailComponent, EditComponent],
  imports: [
    ComponentsModule,
    ShareModule,
    EntityLibraryRoutingModule
  ]
})
export class EntityLibraryModule { }
