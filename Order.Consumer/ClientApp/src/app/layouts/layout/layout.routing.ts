import { Routes } from '@angular/router';

import { DashboardComponent } from '../../dashboard/dashboard.component';
import { ProductComponent } from 'app/product/product.component';

export const LayoutRoutes: Routes = [
   
    { path: 'dashboard',  component: DashboardComponent },
    { path: 'product',   component: ProductComponent },

];
