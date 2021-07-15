import { Routes } from '@angular/router';

import { DashboardComponent } from '../../dashboard/dashboard.component';
import { ProductComponent } from 'app/product/product.component';
import { AuthGuardService } from '../../services/auth-guard.service';

export const LayoutRoutes: Routes = [

  { path: 'dashboard', component: DashboardComponent, canActivate: [AuthGuardService] },
  { path: 'product', component: ProductComponent, canActivate: [AuthGuardService] },

];
