import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SupplierService {
  private apiUrl = 'https://localhost:7192/api/Supplier';  // Adjust to your API URL

  constructor(private http: HttpClient) {}

  getAllSuppliers(): Observable<any[]> {
    return this.http.get<any[]>(this.apiUrl);
  }

  createSupplier(supplier: any): Observable<any> {
    return this.http.post<any>(this.apiUrl, supplier);
  }

  updateSupplier(supplierId: number, supplier: any): Observable<any> {
    return this.http.put<any>(`${this.apiUrl}/${supplierId}`, supplier);
  }

  deleteSupplier(supplierId: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${supplierId}`);
  }
}
