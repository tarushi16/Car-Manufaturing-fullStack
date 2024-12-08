import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class FinanceService {
  private apiUrl = 'https://localhost:7192/api/financial'; // Adjust the URL if needed

  constructor(private http: HttpClient) {}

  getAllFinances(): Observable<any[]> {
    return this.http.get<any[]>(this.apiUrl);
  }

  getFinanceById(financeId: number): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}/${financeId}`);
  }

  createFinance(finance: any): Observable<any> {
    return this.http.post<any>(this.apiUrl, finance);
  }

  updateFinance(financeId: number, finance: any): Observable<any> {
    return this.http.put<any>(`${this.apiUrl}/${financeId}`, finance);
  }

  deleteFinance(financeId: number): Observable<any> {
    return this.http.delete<any>(`${this.apiUrl}/${financeId}`);
  }
}
