import { Routes } from '@angular/router';

import { ProductComponent } from 'app/pages/product/product.component';
import { DetailsComponent } from '../../pages/details/details.component';
import { AuthGuardService } from '../../services/auth-guard.service';

export const LayoutRoutes: Routes = [
  { path: 'product', component: ProductComponent, canActivate: [AuthGuardService] },
  { path: 'details', component: DetailsComponent, canActivate: [AuthGuardService] }
];
