import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from "@angular/router";
import { EducationReport } from './education-report/education-report';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, RouterModule, EducationReport],
  template: `<app-header></app-header><router-outlet></router-outlet>`
})
export class App { }