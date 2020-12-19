import { Component, Inject,HostListener} from '@angular/core';
import { User } from '../interfaces/user.interface';
import { Game } from '../interfaces/game.interface';
import { HttpClient } from '@angular/common/http';
import { GAMES_QUERY } from './queries';
import { CREATE_GAME, UPDATE_GAME, DELETE_GAME} from './mutations';
import { GAME_SUSCRIPTION } from './suscriptions';
import { Apollo } from 'apollo-angular';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {
  public user: User;
  public games: Game[];
  public game: Game;
  public showForm: boolean;
  public showBall:boolean;
  public ball:number;
  public all_nums: number[];
  constructor(public http: HttpClient, @Inject('BASE_URL') public baseUrl: string,private apollo: Apollo) {
    this.user= JSON.parse(localStorage.getItem('user'));
    this.all_nums=[];
    for (let index = 1; index < 91; index++) {
      this.all_nums.push(index);
   }
   this.refresh();
   //SUBSCRIPTION
   this.apollo.subscribe({
    query : GAME_SUSCRIPTION, 
  }).subscribe(result => {
    this.games.forEach(game => {
      if(result.data["gameReceived"]["id"]==game.id){
        game=result.data["gameReceived"];
      }
    });
  });
   }
   filter(){
    this.apollo.watchQuery({
      query: GAMES_QUERY,
      fetchPolicy: 'network-only',
      variables: {}
    }).valueChanges.subscribe(result => {
      this.games = result.data["games"];
    }, error => console.error(error));
   }
   refresh() {
    this.filter();
    this.showForm = false;
    this.showBall=false;
    this.ball=0;
    this.game = {
      id: -1,
      name: '',
      startTime:new Date(),
      link: '',
      playersNumber: 0,
      gettedNumbers: [],
      finished:false,
    };
  }

  save() {
    let mutation = CREATE_GAME;
    const variables = {
      input: { 
        name: this.game.name,
        startTime:this.game.startTime,
        link:this.game.link,
        playersNumber: this.game.playersNumber,
        gettedNumbers: this.game.gettedNumbers,
        finished:this.game.finished, 
      }
    };
    if(this.game.id > 0){
      variables['id'] = this.game.id;
      mutation = UPDATE_GAME;
    }else{
      variables.input['link']=this.genLink();
    }
    this.apollo.mutate({
      mutation: mutation,
      variables: variables
    }).subscribe(() => {
      this.refresh();
    }, error => console.error(error));
  }

  newGame(){
    this.game = {
      id: -1,
      name: '',
      startTime:new Date(),
      link: '',
      playersNumber: 0,
      gettedNumbers: [],
      finished:false,
    };
    this.showForm = true
    this.game["confirmation"]=false;
  }
  edit(game: Game) {
    this.game = {...game};
    this.showForm = true;
    this.game["confirmation"]=false;
  }
  confirmDelete(game: Game){
    this.game = {...game};
    this.showForm = false;
    this.game["confirmation"]=true;
  }

  delete(game: Game){
    this.apollo.mutate({
      mutation: DELETE_GAME,
      variables: { id: game.id }
    }).subscribe(() => {
      this.refresh();
    }, error => console.error(error));
  }

   isGetted(nums:number[],num:number){
     return nums.includes(num);
   }
   isBiggerThan9(num:number){
     if(num>9){
       return true
     }
     return false;
   }
   getRandomBall(g: Game){
    var game = {...g};
    game.gettedNumbers=g.gettedNumbers.map(function(x) {return x;});
    var available_nums: number[]=[];
    var ball: number=0;
    this.all_nums.forEach(num => {
      if(!game.gettedNumbers.includes(num)){
          available_nums.push(num);
      }
    });
    var arraySize = available_nums.length;
    if(arraySize>1){
      var random=Math.floor(Math.random() * (arraySize - 0)) + 0;
      ball= available_nums[random];
    }else{
      ball=  available_nums[0];
      game.finished=true;
    }
    this.addBallToGame(game, ball);
    this.filter();
  }
  addBallToGame(game: Game, ball: number){
    game.gettedNumbers.push(ball);
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
    this.ball=ball;
    this.showBall=true;
  }, error => console.error(error));
  }

   genLink(){
     var repeat=true;
     var newLink=this.baseUrl+'game/0000000000';
     while(repeat){
      var code=this.genCode(7);
      newLink=this.baseUrl+'game/'+code;
      repeat=false;
      this.games.forEach(game => {
        if(game.link==newLink){
          repeat=true;
        }
      });
     }
     return newLink;
   }
   genCode(length: number) {
    var result           = '';
    var characters       = 'abcdefghijklmnopqrstuvwxyz0123456789';
    var charactersLength = characters.length;
    for ( var i = 0; i < length; i++ ) {
       result += characters.charAt(Math.floor(Math.random() * charactersLength));
    }
    return result;
 }

  
}
