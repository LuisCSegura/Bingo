import { Component, Inject,  OnInit, OnDestroy,HostListener} from '@angular/core';
import { Card} from '../interfaces/card.interface';
import { Game} from '../interfaces/game.interface';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { GAME_SUSCRIPTION } from './suscriptions';
import { CREATE_GAME, UPDATE_GAME, DELETE_GAME,JOIN_A_GAME, QUIT_THE_GAME} from './mutations';
import { GAME_BY_LINK_QUERY} from './queries';
import { SignalService } from './signal.service';
import { Apollo } from 'apollo-angular';

@Component({
  selector: 'app-player',
  templateUrl: './player.component.html',
  styleUrls: ['./player.component.css'],
})
export class PlayerComponent implements OnDestroy, OnInit{
  //To quit the game
  @HostListener("window:beforeunload", [ "$event" ])
    beforeUnloadHander(event) {
      this.quitTheGame();
    }
  @HostListener("window:unload", [ "$event" ])
    unloadHandler(event) {
      this.quitTheGame();
    }
  @HostListener("window:reload", [ "$event" ])
    reloadHander(event) {
      this.quitTheGame();
    }

  public joined: boolean;
  public iWin: boolean;
  public game:Game;
  public all_nums: number[];
  public gettedNumbers: number[];
  public cards: Card[];
  public counter: number;
  public showCounterForm: boolean;
  constructor(public http: HttpClient,
     @Inject('BASE_URL') public baseUrl: string,
     private router: Router,
      private signalService: SignalService,
       private apollo: Apollo) {

    this.game = {
      id: -1,
      name: '',
      startTime:new Date(),
      link: this.baseUrl.substring(0, this.baseUrl.length - 1)+this.router.url,
      playersNumber: 0,
      gettedNumbers: [],
      finished:false,
    };
    this.joined=false;
    this.iWin=false;
    this.cards=[];
    this.counter=1;
    this.showCounterForm=true;
    this.all_nums=[];
    this.gettedNumbers=[];
    for (let index = 1; index < 91; index++) {
      this.all_nums.push(index);
   }
   this.joinAGame();

   this.apollo.subscribe({
      query : GAME_SUSCRIPTION, 
    }).subscribe(result => {
      if((!this.game.finished)&&(result.data["gameReceived"]["id"]==this.game.id)){
        this.game=result.data["gameReceived"];
        this.iWinRefresh();
      }
    });
   }
   ngOnInit(){
  }
   ngOnDestroy(){
      this.quitTheGame();
   }
  
   joinAGame(){
    this.apollo.mutate({
      mutation: JOIN_A_GAME,
      variables: {
        link: this.game.link,
      }
    }).subscribe(result => {
      if(result.data["joinAGame"]!=null){
        this.game = result.data["joinAGame"];
        this.joined=true;
      }
      
    }, error => console.error(error));
   }
   quitTheGame(){
     if(this.joined){
      this.joined=false; 
      this.apollo.mutate({
        mutation: QUIT_THE_GAME,
        variables: {
          id: this.game.id
        }
      }).subscribe(result => {
        if(result.data["quitTheGame"]!=null){
          this.joined=false;
        }
      }, error => console.error(error));
     }
   }

   createCards(){
    for (let index = 1; index < this.counter+1; index++) {
      var newCard: Card={
        'id':index,
        'numbers':this.genCardNumbers(),
        'completed':false
      }
      this.cards.push(newCard);
    }
    this.showCounterForm=false;
   }
   iWinRefresh(){
      this.cards.forEach(card => {
        var inThisCard:number[]=[];
        card.numbers.forEach(number => {
          if (this.game.gettedNumbers.includes(number)){
             inThisCard.push(number);
          }
        });
        if (inThisCard.length>=15){
          this.iWin=true;
          this.game.finished=true;
          this.endGame(this.game);
        }
      });
   }
   endGame(game: Game){
    this.apollo.mutate({
      mutation: UPDATE_GAME,
      variables: {
        id:game.id,
        input: { 
          name: game.name,
          startTime:game.startTime,
          link:game.link,
          playersNumber: game.playersNumber,
          gettedNumbers: game.gettedNumbers,
          finished:game.finished,
        }
      }
    }).subscribe(result => {
   }, error => console.error(error));
   }

   genCardNumbers(){
    var array: number[]=[];
    var arraySize = this.all_nums.length;
    for (let index = 0; index < 15;) {
      var random=Math.floor(Math.random() * (arraySize - 1)) + 1;
      if(!array.includes(random)){
        array.push(random);
        index++;
      }
    }
    return array;
   }
   isGetted(num:number){
    return this.game.gettedNumbers.includes(num);
  }
  isBiggerThan9(num:number){
    if(num>9){
      return true
    }
    return false;
  } 



}
