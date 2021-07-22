import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Response } from '../models/response';
import { TokenResponse } from '../models/token.response';
import { User } from '../models/user';
import { UserService } from '../services/user.service';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.css']
})
export class SignupComponent implements OnInit {
  
  user: User = new User();

  exists: boolean = false;

  invalidTouched(field): boolean {
    return !field.valid && field.touched;
  }

  validateUser(user: User): boolean {
    return user.username && user.username !== '' && !this.exists && user.password && user.password !== '' && user.name && user.name !== '' && user.confirmPassword === user.password;
  }

  constructor(private userService: UserService, private router: Router) { }

  ngOnInit(): void {
    if (this.userService.isAuthenticated())
      this.router.navigate(['']);
    console.log(this.user.username);
  }

  confirmarPassword(password: any, confirmPassword: any): boolean {
    return password.touched && confirmPassword.touched && (this.user.password !== this.user.confirmPassword || !confirmPassword.valid);
  }

  async validateUsername(): Promise<boolean> {
    var result = await this.userService.validateUsername(this.user);
    this.exists = result.result;
    return result.result;
  }


  async onSubmit(): Promise<void> {
    if (!this.validateUser(this.user))
      return;
    var resp: Response<TokenResponse> = await this.userService.newUser(this.user);
    if (resp.error)
      console.log(resp.errors);
    this.router.navigate(['']);
  }
}
