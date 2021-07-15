import { Component, OnInit } from '@angular/core';
import { User } from '../models/user';
import { UserService } from '../services/user.service';
import { TokenResponse } from '../models/token.response';
import { Response } from '../models/response';
@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  user: User = new User();

  constructor(private userService: UserService) { }

  ngOnInit(): void {
  }

  invalidTouched(field): boolean {
    return !field.valid && field.touched;
  }

  private validateUser(user: any): boolean {
    return user.username !== '' && user.password !== '';
  }

  async onSubmit(): Promise<void> {
    if (!this.validateUser(this.user))
      return;
    var resp: Response<TokenResponse> = await this.userService.login(this.user).toPromise();
    console.log(resp);
  }
}
