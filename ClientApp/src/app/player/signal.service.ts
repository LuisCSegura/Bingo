import { Injectable,Component, Input, Output, EventEmitter } from '@angular/core';
import {HubConnectionBuilder, HubConnection} from '@aspnet/signalr';
import { Game } from '../interfaces/game.interface';

@Injectable({
  providedIn: 'root'
})
export class SignalService {
public hubConnection: HubConnection;
emNotify: EventEmitter<Game> = new EventEmitter();
  constructor() { 
    console.log("Starting the SignalR service");
    let builder= new HubConnectionBuilder();
    this.hubConnection=builder.withUrl("https://localhost:5001/cnn").build();

    this.hubConnection.on("notifyall", (gam)=>{
      let ga: Game = JSON.parse(gam);
      this.emNotify.emit(ga);

    })
    this.hubConnection.start();
  }
}