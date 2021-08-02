import { Component, OnInit, ElementRef } from '@angular/core';
import { Location } from '@angular/common';
import { User } from '../../models/user';
import { UserService } from '../../services/user.service';
import { Router } from '@angular/router';

declare interface RouteInfo {
  path: string;
  title: string;
  icon: string;
  class: string;
}
@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {
  private listTitles: any[];
  private location: Location;
  user: User;

  private ROUTES: RouteInfo[] = [
    { path: '/product', title: 'Produtos Pendentes', icon: 'library_books', class: '' },
    { path: '/details', title: 'Detalhes', icon: 'library_books', class: '' }
  ];


  constructor(location: Location, private userService: UserService, private router: Router) {
    this.location = location;    
  }

  ngOnInit() {
    this.listTitles = this.ROUTES.filter(listTitle => listTitle);
    this.user = this.userService.token.user;
  };

  logout(): void {
    this.userService.logout();
    this.router.navigate(['login']);
  }

  getTitle() {
    var titlee = this.location.prepareExternalUrl(this.location.path());
    if (titlee.charAt(0) === '#') {
      titlee = titlee.slice(1);
    }

    for (var item = 0; item < this.listTitles.length; item++) {
      if (titlee.includes(this.listTitles[item].path)) {
        return this.listTitles[item].title;
      }
    }
    return 'Dashboard';
  }
}
