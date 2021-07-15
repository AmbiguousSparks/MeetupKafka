import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Response } from '../models/response';
import { TokenResponse } from '../models/token.response';
import { User } from '../models/user';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  private _host = window.location.origin + window.location.pathname;

  constructor(private httpClient: HttpClient) {
    console.log(this._host);
  }

  newUser(user: User): Observable<Response<User>> {
    const request = JSON.stringify(user);
    let headers = new HttpHeaders();
    headers.append('Content-Type', 'application/json');

    return this.httpClient.post<Response<User>>(`${this._host}api/Users/New`, request, {
      headers
    });
  }

  login(user: User): Observable<Response<TokenResponse>> {
    const request = JSON.stringify(user);
    let headers = new HttpHeaders().set('Content-Type', 'application/json;charset=utf-8');
    let options = {
      headers
    };
    return this.httpClient.post<Response<TokenResponse>>(`${this._host}api/Users/Login`, request, options);
  }
}
