import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
// import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ProductionService {

  private apiUrl = `https://localhost:7192/api/Production`;

  constructor(private http: HttpClient) { }

  // Method to get all production orders
  getAllProductionOrders(): Observable<any[]> {
    return this.http.get<any[]>(this.apiUrl);
  }

  // Method to create a new production order
  createProductionOrder(productionOrder: any): Observable<any> {
    return this.http.post<any>(this.apiUrl, productionOrder);
  }

  // Method to update an existing production order
  updateProductionOrder(id: number, productionOrder: any): Observable<any> {
    return this.http.put<any>(`${this.apiUrl}/${id}`, productionOrder);
  }

  // Method to delete a production order
  deleteProductionOrder(id: number): Observable<any> {
    return this.http.delete<any>(`${this.apiUrl}/${id}`);
  }
}
