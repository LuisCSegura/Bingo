import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { AuthService } from '../login/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {
  public usr;
  public repass;
  constructor(public http: HttpClient, @Inject('BASE_URL') public baseUrl: string, private _authService: AuthService) {
    this.usr = {
      "name": "",
      "email": "",
      "password": ""
    }
  }
  register() {

    if (this.usr.email == "" || this.usr.password == "") {
      alert("Invalid data")
    } else if (this.usr.password != this.repass) {
      alert("Passwords don't match");
    } else {
      this.http.post(this.baseUrl + 'api/auth/register', this.usr).subscribe(() => {
        this._authService.signin(this.usr);
      }, error => console.error(alert("Invalid data")));
    }
  }
}

