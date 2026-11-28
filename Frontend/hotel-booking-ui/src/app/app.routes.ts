
import { Routes } from '@angular/router';

import { Login } from './customer/auth/login/login';
import { Register } from './customer/auth/register/register';
import { HotelSearch } from './customer/hotels/hotel-search/hotel-search';

export const routes: Routes = [
  {
    path: '',
    redirectTo: 'customer/login',
    pathMatch: 'full'
  },
  {
    path: 'customer/login',
    component: Login
  },
  {
    path: 'customer/register',
    component: Register
  },
  {
    path: 'customer/hotels',
    component: HotelSearch
  }
];
