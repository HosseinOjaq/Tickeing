import { Component } from '@angular/core';
import { ReportService } from '../../services/report.service';

@Component({
  selector: 'app-education-report',
  imports: [],
  templateUrl: './education-report.html',
  styleUrl: './education-report.css'
})
export class EducationReport {
  constructor(private reportService: ReportService) {

  }

  downloadPdf() {
    this.reportService.generateEducationPdf().subscribe((blob: any) => {
      const link = document.createElement('a');
      link.href = window.URL.createObjectURL(blob);
      link.download = 'EducationReport.pdf';
      link.click();
    });
  }
}