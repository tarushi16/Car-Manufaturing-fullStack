import { Component, OnInit } from '@angular/core';
import { CarModelService, CarModel } from './car-model.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router'; 

@Component({
  selector: 'app-car-models',
  templateUrl: './car-models.component.html',
  styleUrls: ['./car-models.component.scss']
})
export class CarModelsComponent implements OnInit {
  carModels: CarModel[] = [];
  selectedCarModel: CarModel | null = null;
  carModelForm: FormGroup;

  constructor(
    private carModelService: CarModelService,
    private fb: FormBuilder,
    private router: Router
  ) {
    // Initializing form group with proper validation
    this.carModelForm = this.fb.group({
      modelId: [null],  // Changed to `null` instead of empty string
      modelName: ['', Validators.required],  // Added validation
      engineType: ['', Validators.required],  // Added validation
      fuelEfficiency: ['', Validators.required],  // Added validation
      designFeatures: ['', Validators.required],  // Added validation
      productionCost: ['', [Validators.required, Validators.min(0)]],  // Added validation for non-negative production cost
      status: ['Active', Validators.required]  // Set default value to 'Active'
    });
  }

  ngOnInit(): void {
    this.loadCarModels();
  }

  // Load car models from the service
  loadCarModels() {
    this.carModelService.getCarModels().subscribe(
      (data) => {
        this.carModels = data;
      },
      (error) => {
        alert('Failed to load car models!');
        console.error(error);
      }
    );
  }

  // Edit car model data when the user selects a model
  onEdit(carModel: CarModel) {
    this.selectedCarModel = carModel;
    this.carModelForm.patchValue(carModel);
  }

  // Submit the form to add or update a car model
  onSubmit() {
    if (this.carModelForm.invalid) {
      alert('Please fill in all required fields!');
      return;
    }

    const carModel = this.carModelForm.value;

    // Check if modelId exists before updating
    if (carModel.modelId !== null && carModel.modelId !== undefined) {
      // Update car model if it already exists
      this.carModelService.updateCarModel(carModel.modelId, carModel).subscribe(
        () => {
          alert('Car model updated successfully!');
          this.resetForm();
          this.loadCarModels();
        },
        (error) => {
          alert('Failed to update car model!');
          console.error(error);
        }
      );
    } else {
      // Add new car model if modelId is null or undefined
      this.carModelService.addCarModel(carModel).subscribe(
        () => {
          alert('Car model added successfully!');
          this.resetForm();
          this.loadCarModels();
        },
        (error) => {
          alert('Failed to add car model!');
          console.error(error);
        }
      );
    }
  }

  // Delete car model
  onDelete(id: number) {
    if (confirm('Are you sure you want to delete this car model?')) {
      this.carModelService.deleteCarModel(id).subscribe(
        () => {
          alert('Car model deleted successfully!');
          this.loadCarModels();
        },
        (error) => {
          alert('Failed to delete car model!');
          console.error(error);
        }
      );
    }
  }

  // Reset the form and clear selected car model
  resetForm() {
    this.carModelForm.reset({
      modelId: null,  // Set modelId to null upon reset
      modelName: '',
      engineType: '',
      fuelEfficiency: '',
      designFeatures: '',
      productionCost: '',
      status: 'Active'  // Reset status to default 'Active'
    });
    this.selectedCarModel = null;
  }

  // Navigate to Dashboard
  goToDashboard() {
    this.router.navigate(['/dashboard']);
  }
}
