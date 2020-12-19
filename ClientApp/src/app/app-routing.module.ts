import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PlayerComponent } from './player/player.component'; 

const rutas: Routes=[
{path:'game/:link', component: PlayerComponent },

]

@NgModule({
  imports: [RouterModule.forRoot(rutas)],
  exports: [ RouterModule ]
  
})

export class AppRoutingModule { }
