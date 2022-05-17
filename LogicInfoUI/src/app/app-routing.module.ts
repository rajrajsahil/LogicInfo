import { Component, NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { UnitMasterComponent } from './unit-master/unit-master.component';

const routes: Routes = [
  {
    path: 'unitMaster',
    component: UnitMasterComponent
  },
  {
    path: '',
    pathMatch: 'full',
    redirectTo: 'unitMaster'
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
