import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment'; // Adjust path if necessary

@Injectable({
  providedIn: 'root'
})
export class RegisterService {

  private apiUrl = `https://localhost:7192/api/Auth/register`; // Replace with your actual API URL

  constructor(private http: HttpClient) { }

  register(username: string, email: string, password: string, role: string): Observable<any> {
    const registerData = { username, email, password, role };
    return this.http.post<any>(this.apiUrl, registerData);
  }
}
