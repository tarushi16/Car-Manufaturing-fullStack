import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SalesOrderService {
  private apiUrl = 'https://localhost:7192/api/SalesOrder'; // Adjust the URL as necessary

  constructor(private http: HttpClient) {}

  getAllSalesOrders(): Observable<any[]> {
    return this.http.get<any[]>(this.apiUrl);
  }

  createSalesOrder(order: any): Observable<any> {
    return this.http.post<any>(this.apiUrl, order);
  }

  updateSalesOrder(orderId: number, order: any): Observable<any> {
    return this.http.put<any>(`${this.apiUrl}/${orderId}`, order);
  }

  deleteSalesOrder(orderId: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${orderId}`);
  }
}
