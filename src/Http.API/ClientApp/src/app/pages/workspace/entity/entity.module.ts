import { NgModule } from '@angular/core';
import { EntityRoutingModule } from './entity-routing.module';
import { ShareModule } from 'src/app/share/share.module';
import { EntityModelModule } from './entity-model/entity-model.module';
import { EntityLibraryModule } from './entity-library/entity-library.module';

@NgModule({
  declarations: [],
  imports: [
    ShareModule,
    EntityModelModule,
    EntityLibraryModule,
    EntityRoutingModule
  ]
})
export class EntityModule { }
