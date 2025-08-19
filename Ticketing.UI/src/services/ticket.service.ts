import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class TicketService {
  private http = inject(HttpClient);
  private apiUrl = environment.apiUrl;

  getTickets(): Observable<any> {
    return this.http.get(`${this.apiUrl}/api/Tickets`);
  }

  getMyTickets(): Observable<any> {
    return this.http.get(`${this.apiUrl}/api/Tickets/my`);
  }

  addTicket(title: string, description: string): Observable<any> {
    const payload = { title, description };
    return this.http.post(`${this.apiUrl}/api/Tickets`, payload);
  }

  updateTicket(ticket: any): Observable<any> {
    const payload = {
      id: ticket.id,
      title: ticket.title,
      description: ticket.description,
      status: ticket.status,
      priority: ticket.priority,
      createdByUserId: ticket.createdByUserId,
      assignedToUserId: ticket.assignedToUserId
    };
    return this.http.put(`${this.apiUrl}/api/Tickets/update`, payload);
  }

  inprogressTicket(ticket: any): Observable<any> {
    return this.http.put(`${this.apiUrl}/api/Tickets/${ticket.id}`, ticket);
  }

  deleteTicket(id: string): Observable<any> {
    return this.http.delete(`${this.apiUrl}/api/Tickets/${id}`);
  }

  getStats(): Observable<any> {
    return this.http.get(`${this.apiUrl}/api/Tickets/stats`);
  }
}
