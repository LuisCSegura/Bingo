import { Component } from '@angular/core';
import { User } from '../interfaces/user.interface';
import {AuthService } from '../login/auth.service';


@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {
  public user: User;
  public logged_in: boolean;
  constructor( private _authService: AuthService) {
    this.refresh()
  }
  refresh(){
    this.user= JSON.parse(localStorage.getItem('user'));
    this.logged_in=false;
    if(this.user && this._authService.isAuthenticated()){
      this.logged_in=true;
    }
  }
  logout() {
      this._authService.singout();
      this.refresh();
  }
  isExpanded = false;

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }
}
