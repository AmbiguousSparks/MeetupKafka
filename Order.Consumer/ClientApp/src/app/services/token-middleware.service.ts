import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { UserService } from './user.service';

@Injectable({
  providedIn: 'root'
})
export class TokenMiddlewareService implements HttpInterceptor {

  constructor(private userService: UserService, private route: Router) { }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    let clonedRequest = req;
    if (this.userService.isAuthenticated()) {
      clonedRequest = req.clone({ headers: req.headers.append('Authorization', `Bearer ${this.userService.token.token}`) });
    }
    return next.handle(clonedRequest);
  }
}
