import { Component, OnInit, ElementRef } from '@angular/core';
import { ROUTES } from '../sidebar/sidebar.component';
import { Location } from '@angular/common';
import { User } from '../../models/user';
import { UserService } from '../../services/user.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {
  private listTitles: any[];
  private location: Location;
  user: User;

  constructor(location: Location, private userService: UserService, private router: Router) {
    this.location = location;    
  }

  ngOnInit() {
    this.listTitles = ROUTES.filter(listTitle => listTitle);
    this.user = this.userService.token.user;
  };

  logout(): void {
    this.userService.logout();
    this.router.navigate(['login']);
  }

  getTitle() {
    return 'Produtos Pendentes';
  }
}
