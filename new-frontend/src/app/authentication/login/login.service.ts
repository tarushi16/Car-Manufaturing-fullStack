import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment'; // Adjust path if necessary

@Injectable({
  providedIn: 'root'
})
export class LoginService {

  private apiUrl = `https://localhost:7192/api/Auth/login`; // Replace with your actual API URL

  constructor(private http: HttpClient) { }

  login(username: string, password: string): Observable<any> {
    const loginData = { username, password };
    return this.http.post<any>(this.apiUrl, loginData);
  }
}
