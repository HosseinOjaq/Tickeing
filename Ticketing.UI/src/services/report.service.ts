import { Injectable } from "@angular/core";
import { environment } from '../../environment';
import { HttpClient } from "@angular/common/http";

@Injectable({
    providedIn: 'root'
})
export class ReportService {
    constructor(private http: HttpClient) {

    }

    private apiUrl = environment.apiUrl;

    generateEducationPdf() {
        return this.http.post(`${this.apiUrl}/education/pdf`, {
            responseType: 'blob', // دریافت فایل باینری
        });
    }
}