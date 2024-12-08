import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface CarModel {
  modelId?: number;
  modelName: string;
  engineType: string;
  fuelEfficiency: string;
  designFeatures: string;
  productionCost: number;
  status: string;
}

@Injectable({
  providedIn: 'root'
})
export class CarModelService {
  private apiUrl = 'https://localhost:7192/api/CarModel'; // Replace with your actual backend URL

  constructor(private http: HttpClient) {}

  getCarModels(): Observable<CarModel[]> {
    return this.http.get<CarModel[]>(this.apiUrl);
  }

  getCarModelById(id: number): Observable<CarModel> {
    return this.http.get<CarModel>(`${this.apiUrl}/${id}`);
  }

  addCarModel(carModel: CarModel): Observable<CarModel> {
    return this.http.post<CarModel>(this.apiUrl, carModel);
  }

  updateCarModel(id: number, carModel: CarModel): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${id}`, carModel);
  }

  deleteCarModel(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
