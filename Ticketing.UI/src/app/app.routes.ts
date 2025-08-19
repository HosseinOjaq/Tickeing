import { Routes } from '@angular/router';
import { LoginComponent } from './login.component/login.component';
import { TicketComponent } from './ticket.component/ticket.component';
import { authGuard } from '././guards/auth.guard';

export const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: 'tickets', component: TicketComponent, canActivate: [authGuard] },
  { path: '', redirectTo: 'login', pathMatch: 'full' }
];
