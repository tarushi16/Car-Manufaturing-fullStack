// quality-control.service.ts

import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class QualityControlService {
  private apiUrl = 'https://localhost:7192/api/QualityControl';  // Adjust with your API URL

  constructor(private http: HttpClient) {}

  getAllQualityReports(): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}`);
  }

  getQualityReportById(id: number): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}/${id}`);
  }

  createQualityReport(qualityReport: any): Observable<any> {
    return this.http.post<any>(this.apiUrl, qualityReport);
  }

  updateQualityReport(id: number, qualityReport: any): Observable<any> {
    return this.http.put<any>(`${this.apiUrl}/${id}`, qualityReport);
  }

  deleteQualityReport(id: number): Observable<any> {
    return this.http.delete<any>(`${this.apiUrl}/${id}`);
  }
}
