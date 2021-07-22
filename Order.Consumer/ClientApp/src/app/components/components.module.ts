import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

import { NavbarComponent } from './navbar/navbar.component';
import { SidebarComponent } from './sidebar/sidebar.component';
import { LoginComponent } from '../login/login.component';
import { FormsModule } from '@angular/forms';
import { SignupComponent } from '../signup/signup.component';

@NgModule({
  imports: [
    CommonModule,
    RouterModule,
    FormsModule,
  ],
  declarations: [

    NavbarComponent,
    SidebarComponent,
    LoginComponent,
    SignupComponent
  ],
  exports: [

    NavbarComponent,
    SidebarComponent,
    LoginComponent,
    SignupComponent
  ]
})
export class ComponentsModule { }
