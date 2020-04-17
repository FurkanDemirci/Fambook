import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from 'src/app/shared/shared.module';
import { EditComponent } from './pages/edit/edit.component';
import { ProfileRoutingModule } from './profile.routing';

@NgModule({
  declarations: [
    EditComponent,
  ],
  imports: [
    CommonModule,
    ProfileRoutingModule,
    SharedModule,
  ],
})
export class ProfileModule { }
