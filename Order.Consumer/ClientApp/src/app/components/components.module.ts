import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

import { NavbarComponent } from './navbar/navbar.component';
import { SidebarComponent } from './sidebar/sidebar.component';
import { FormsModule } from '@angular/forms';
import { LoginComponent } from '../pages/login/login.component';
import { SignupComponent } from '../pages/signup/signup.component';
import { DetailsComponent } from '../pages/details/details.component';

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
    SignupComponent,
    DetailsComponent
  ],
  exports: [

    NavbarComponent,
    SidebarComponent,
    LoginComponent,
    SignupComponent,
    DetailsComponent
  ]
})
export class ComponentsModule { }
