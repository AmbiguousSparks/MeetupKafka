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
  private _token: TokenResponse;

  public get token(): TokenResponse {
    let jsonuser = localStorage.getItem('user');
    this._token = JSON.parse(jsonuser) as TokenResponse;
    if (this._token && this._token != null && (this.getUtcFullDate(new Date(this._token.validTo)).getTime() <= this.getUtcFullDate(new Date()).getTime()))
      this._token = null;
    return this._token;
  }
  public set token(value: TokenResponse) {
    let jsonuser = JSON.stringify(value);
    localStorage.setItem('user', jsonuser);
  }

  private getUtcFullDate(date: Date): Date {
    return new Date(date.getUTCFullYear(), date.getUTCMonth(), date.getUTCDate(), date.getUTCHours(), date.getUTCMinutes());
  }

  public logout(): void {
    localStorage.removeItem('user');
  }

  public isAuthenticated(): boolean {
    return this.token !== null;
  }

  constructor(private httpClient: HttpClient) {
  }

  newUser(user: User): Observable<Response<User>> {
    const request = JSON.stringify(user);
    let headers = new HttpHeaders();
    headers.append('Content-Type', 'application/json');

    return this.httpClient.post<Response<User>>(`${this._host}api/Users/New`, request, {
      headers
    });
  }


  async login(user: User): Promise<Response<TokenResponse>> {
    const request = JSON.stringify(user);
    let headers = new HttpHeaders().set('Content-Type', 'application/json;charset=utf-8');
    let options = {
      headers
    };
    let response = await this.httpClient.post<Response<TokenResponse>>(`${this._host}api/Users/Login`, request, options).toPromise();
    if (!response.error)
      this.token = response.result;
    return response;
  }
}
