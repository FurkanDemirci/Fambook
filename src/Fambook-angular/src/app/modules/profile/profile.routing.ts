import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { EditComponent } from './pages/edit/edit.component';

const routes: Routes = [
  {
    path: '',
    children: [
      {
        path: 'edit',
        component: EditComponent
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ProfileRoutingModule { }
