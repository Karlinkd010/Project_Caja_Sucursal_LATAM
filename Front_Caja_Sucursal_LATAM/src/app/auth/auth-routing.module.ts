import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { NipComponent } from './pages/nip/nip.component';
import { TarjetaComponent } from './pages/tarjeta/tarjeta.component';


const routes:Routes=[
  {
    path:'',
    children:[
      {
        path:'tarjeta',
        component:TarjetaComponent
      },
      {
        path:'nip',
        component:NipComponent
      },
      {
        path:'**',
        component:TarjetaComponent
      },
    ]
  }
];
@NgModule({
  
  imports: [
   RouterModule.forChild(routes)
  ],
  exports: [
    RouterModule
  ]
})
export class AuthRoutingModule { }
