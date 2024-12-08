import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment'; // Adjust path as needed
import { LoginService } from './login.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent {
  loginData = { username: '', password: '' };
  
  constructor(private http: HttpClient, private router: Router, private loginService:LoginService) {
  
  }

  onLogin() {
    debugger;
  this.loginService.login(this.loginData.username, this.loginData.password).subscribe((response) => {
      debugger;
      localStorage.setItem('token', response.token); // Save JWT token in localStorage
      this.router.navigate(['/dashboard']); // Redirect to dashboard after successful login
    });

    // this.http.post<any>(`${environment.apiUrl}/api/auth/login`, this.loginData)
    //   .subscribe(
    //     (response) => {
    //       localStorage.setItem('token', response.token); // Save JWT token in localStorage
    //       this.router.navigate(['/dashboard']); // Redirect to dashboard after successful login
    //     },
    //     (error) => {
    //       alert('Login failed: ' + error.error.message);
    //     }
    //   );
  }
}
