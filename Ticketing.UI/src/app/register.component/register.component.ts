import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { AuthService } from '../../services/auth.service';
import Swal from 'sweetalert2';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {
  registerForm: FormGroup;

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router
  ) {
    this.registerForm = this.fb.group({
      fullName: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]],
      role: [2, Validators.required] // مقدار پیش‌فرض Employee
    });
  }

  register() {
    if (this.registerForm.invalid) {
      this.registerForm.markAllAsTouched();
      return;
    }
    const payload = {
      ...this.registerForm.value,
      role: Number(this.registerForm.value.role)
    };

    this.authService.register(payload).subscribe({
      next: (res: any) => {
        if (res.isSuccess) {
          Swal.fire('موفق!', 'ثبت‌نام با موفقیت انجام شد', 'success');
          this.router.navigate(['/login']);
        } else {
          Swal.fire('خطا', res.messages.map((m: any) => m.message).join(', '), 'error');
        }
      },
      error: (err) => {
        console.error(err);
        Swal.fire('خطا', 'اتصال به سرور برقرار نشد', 'error');
      }
    });
  }
}
