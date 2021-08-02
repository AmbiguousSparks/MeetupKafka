import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';


import { AppRoutingModule } from './app.routing';
import { ComponentsModule } from './components/components.module';

import { AppComponent } from './app.component';

import { LayoutComponent } from './layouts/layout/layout.component';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { ProductComponent } from './pages/product/product.component';
import { TokenMiddlewareService } from './services/token-middleware.service';
@NgModule({
  imports: [
    BrowserAnimationsModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    ComponentsModule,
    RouterModule,
    AppRoutingModule,
    MatSnackBarModule
  ],
  declarations: [
    AppComponent,
    LayoutComponent,
    ProductComponent
  ],
  providers: [{ provide: HTTP_INTERCEPTORS, useClass: TokenMiddlewareService, multi: true }],
  bootstrap: [AppComponent]
})
export class AppModule { }
