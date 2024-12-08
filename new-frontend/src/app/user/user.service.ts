// user.service.ts
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
//import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  private apiUrl = `https://localhost:7192/api//user`; // Update with the correct API URL

  constructor(private http: HttpClient) { }

  // Fetch a user by ID
  getUserById(userId: number): Observable<any> {
    return this.http.get(`${this.apiUrl}/${userId}`);
  }

  // Fetch total number of users and active users
  getTotalUsers(): Observable<any> {
    return this.http.get(`${this.apiUrl}/stats`); // Make sure your API endpoint returns stats like total users, active users
  }

  // Create a new user
  createUser(user: any): Observable<any> {
    return this.http.post(this.apiUrl, user);
  }

  // Update an existing user
  updateUser(userId: number, user: any): Observable<any> {
    return this.http.put(`${this.apiUrl}/${userId}`, user);
  }

  // Deactivate a user
  deactivateUser(userId: number): Observable<any> {
    return this.http.patch(`${this.apiUrl}/deactivate/${userId}`, {});
  }
}
