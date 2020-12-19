import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Apollo } from 'apollo-angular';
import { LOGIN_QUERY } from './queries';
import { User}from '../interfaces/user.interface';
import { AuthResponse}from '../interfaces/authResponse.interface';

const jwtHelper = new JwtHelperService();
@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(public http: HttpClient, @Inject('BASE_URL') public baseUrl: string,private router: Router,private apollo: Apollo) { }

  public signin(usr) {
    usr.email=usr.email.trim().toLowerCase();;
    this.apollo.watchQuery({
      query: LOGIN_QUERY,
      fetchPolicy: 'network-only',
      variables: {
        input: usr
      }
    }).valueChanges.subscribe(result => {
      var response = result.data["login"];
      if(response!=null){
        this.saveToken(response.token);
        usr.name=response.userName;
        usr.id=response.id;
        this.saveUser(usr);
        window.location.href = this.baseUrl;
      }else{
        alert("Invalid Credentials")
      }
    }, error => console.error(console.log(error.message)));
  }

  private saveToken(token) {
    sessionStorage.setItem('token', token);
  }
  private saveUser(user) {
    var usr=JSON.stringify(user);
    localStorage.setItem('user', usr);
  }
  public singout(){
    localStorage.removeItem('user');
    sessionStorage.removeItem('token');
    this.router.navigate(['/login']);
  }
  public isAuthenticated(): boolean {
    const token = sessionStorage.getItem('token');
    if(token){
      return !jwtHelper.isTokenExpired(token);
    }
    else{
      return false;
    }
  }
}
