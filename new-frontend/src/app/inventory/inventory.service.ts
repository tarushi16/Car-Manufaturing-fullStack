import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class InventoryService {
  private apiUrl = 'https://localhost:7192/api/Inventory';

  constructor(private http: HttpClient) {}

  // Fetch all inventory items
  getAllInventory(): Observable<any[]> {
    return this.http.get<any[]>(this.apiUrl);
  }

  // Fetch an inventory item by ID
  getInventoryById(id: number): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}/${id}`);
  }

  // Add a new inventory item
  addInventory(item: any): Observable<any> {
    return this.http.post<any>(this.apiUrl, item);
  }

  // Update an existing inventory item
  updateInventory(id: number, item: any): Observable<any> {
    return this.http.put<any>(`${this.apiUrl}/${id}`, item);
  }

  // Delete an inventory item
  deleteInventory(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
