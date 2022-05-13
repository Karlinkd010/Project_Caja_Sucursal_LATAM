import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NipComponent } from './pages/nip/nip.component';
import { TarjetaComponent } from './pages/tarjeta/tarjeta.component';
import { MaterialDesingModule } from '../material/material-desing.module';
import { AuthRoutingModule } from './auth-routing.module';

@NgModule({
  declarations: [
    NipComponent,
    TarjetaComponent
  ],
  imports: [
    CommonModule,
    AuthRoutingModule,
    MaterialDesingModule
  ]
})
export class AuthModule { }
