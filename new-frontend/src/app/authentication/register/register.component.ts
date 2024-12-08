import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { RegisterService } from './register.service';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment'; // Adjust path as needed

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent {
  registerData = { username: '', password: '', email: '', role: '' };

  constructor(private router: Router, private registerService: RegisterService) {}

  onRegister() {
    this.registerService.register(this.registerData.username, this.registerData.email, this.registerData.password, this.registerData.role)
      .subscribe(
        (response) => {
          debugger;
          alert('Registration successful!');
          localStorage.setItem('token', response.token); // Save JWT token in localStorage
          this.router.navigate(['/']); // Redirect to login after successful registration
        },
        (error) => {
          alert('Registration failed: ' + error.error.message);
        }
      );
  }
}