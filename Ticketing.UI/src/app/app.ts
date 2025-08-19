import { CommonModule } from '@angular/common';
import { RouterModule } from "@angular/router";
import { Component } from '@angular/core';
import { HeaderComponent } from './header.component/header.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, RouterModule, HeaderComponent],
  template: `<app-header></app-header><router-outlet></router-outlet>`
})
export class App {}
