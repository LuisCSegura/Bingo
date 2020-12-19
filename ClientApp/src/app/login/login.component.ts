import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import {AuthService } from './auth.service';
import { User}from '../interfaces/user.interface';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {

  public usr;

  constructor(public http: HttpClient, @Inject('BASE_URL') public baseUrl: string, private _authService: AuthService) {

    this.usr = {
      "email": "",
      "password": "",
      "name":""
    }
  }
  login() {
    if (this.usr.email == "" || this.usr.password == "") {
      alert("You must provide the required credentials")
    } else {
      this._authService.signin(this.usr);
    }
  }

}