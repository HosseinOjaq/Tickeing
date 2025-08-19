import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import Swal from 'sweetalert2';
import { TicketService } from '../../services/ticket.service';
import { AuthService } from '../../services/auth.service';
import { firstValueFrom } from 'rxjs';

@Component({
  selector: 'app-ticket',
  standalone: true,
  imports: [CommonModule, FormsModule, ReactiveFormsModule],
  templateUrl: './ticket.component.html',
  styleUrls: ['./ticket.component.css']
})
export class TicketComponent implements OnInit {
  tickets: any[] = [];
  stats: any[] = [];
  isAdmin = false;
  ticketForm: FormGroup;

  constructor(private fb: FormBuilder, private ticketService: TicketService,private authService: AuthService) {
    this.ticketForm = this.fb.group({
      title: ['', [Validators.required]],
      description: ['', [Validators.required, Validators.minLength(6)]]
    });
  }

  async ngOnInit() {
    await this.checkAdminRole();
    this.loadTickets();
    if (this.isAdmin) this.loadStats();
  }

  async checkAdminRole(): Promise<void> {
  const res: any = await firstValueFrom(this.authService.getRoles());
  this.isAdmin = res.data?.includes('Admin') ?? false;
  console.log('isAdmin:', this.isAdmin);
}

  loadTickets() {
    const obs = this.isAdmin ? this.ticketService.getTickets() : this.ticketService.getMyTickets();
    obs.subscribe((res: any) => this.tickets = res.data);
  }

  loadStats() {
    this.ticketService.getStats().subscribe((res: any) => this.stats = res.data);
  }

  addTicket() {
    if (this.ticketForm.invalid) {
      this.ticketForm.markAllAsTouched();
      return;
    }

    const { title, description } = this.ticketForm.value;
    this.ticketService.addTicket(title, description).subscribe(() => {
      this.ticketForm.reset();
      this.loadTickets();
      this.loadStats();
      Swal.fire('موفق!', 'تیکت جدید با موفقیت ثبت شد', 'success');
    });
  }

  updateTicket(ticket: any) {
    this.ticketService.updateTicket(ticket).subscribe(() => {
      this.loadTickets();
      Swal.fire('بروز شد!', 'تیکت با موفقیت ویرایش شد', 'success');
    });
  }

  inprogressTicket(ticket: any) {
    this.ticketService.inprogressTicket(ticket).subscribe(() => {
      this.loadTickets();
      Swal.fire('بروز شد!', 'تیکت با موفقیت ویرایش شد', 'success');
    });
  }

  deleteTicket(ticket: any) {
    Swal.fire({
      title: 'حذف تیکت',
      text: `آیا مطمئن هستید که می‌خواهید "${ticket.title}" را حذف کنید؟`,
      icon: 'warning',
      showCancelButton: true,
      confirmButtonText: 'بله، حذف کن',
      cancelButtonText: 'انصراف'
    }).then(result => {
      if (result.isConfirmed) {
        this.ticketService.deleteTicket(ticket.id).subscribe(() => {
          this.loadTickets();
          this.loadStats();
          Swal.fire('حذف شد!', 'تیکت با موفقیت حذف شد', 'success');
        });
      }
    });
  }
}
