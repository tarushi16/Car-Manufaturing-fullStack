import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { SupplierService } from './supplier.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-supplier',
  templateUrl: './supplier.component.html',
  styleUrls: ['./supplier.component.scss']
})
export class SupplierComponent implements OnInit {
  supplierForm: FormGroup;
  suppliers: any[] = [];
  editingSupplierId: number | null = null;

  constructor(private fb: FormBuilder, private supplierService: SupplierService, private router: Router) {
    this.supplierForm = this.fb.group({
      name: ['', Validators.required],
      contactDetails: ['', Validators.required],
      materialType: ['', Validators.required],
      deliveryTime: ['', [Validators.required, Validators.min(1)]],
      pricing: ['', [Validators.required, Validators.min(0)]],
      status: ['', Validators.required]
    });
  }

  ngOnInit(): void {
    this.getSuppliers();
  }

  getSuppliers(): void {
    this.supplierService.getAllSuppliers().subscribe(
      (suppliers: any[]) => {
        this.suppliers = suppliers;
      },
      (error) => {
        console.error('Error fetching suppliers:', error);
      }
    );
  }

  onSubmit(): void {
    if (this.supplierForm.invalid) {
      return;  // Prevent submission if the form is invalid
    }

    const supplierData = this.supplierForm.value;

    if (this.editingSupplierId) {
      this.supplierService.updateSupplier(this.editingSupplierId, supplierData).subscribe(
        (updatedSupplier) => {
          console.log('Supplier updated:', updatedSupplier);
          this.getSuppliers();  // Refresh the list
          this.resetForm();
        },
        (error) => {
          console.error('Error updating supplier:', error);
        }
      );
    } else {
      this.supplierService.createSupplier(supplierData).subscribe(
        (newSupplier) => {
          console.log('Supplier created:', newSupplier);
          this.getSuppliers();  // Refresh the list
          this.resetForm();
        },
        (error) => {
          console.error('Error creating supplier:', error);
        }
      );
    }
  }

  editSupplier(supplier: any): void {
    this.editingSupplierId = supplier.supplierId;
    this.supplierForm.setValue({
      name: supplier.name,
      contactDetails: supplier.contactDetails,
      materialType: supplier.materialType,
      deliveryTime: supplier.deliveryTime,
      pricing: supplier.pricing,
      status: supplier.status
    });
  }

  deleteSupplier(supplierId: number): void {
    if (confirm('Are you sure you want to delete this supplier?')) {
      this.supplierService.deleteSupplier(supplierId).subscribe(
        () => {
          this.getSuppliers();  // Refresh the list
        },
        (error) => {
          console.error('Error deleting supplier:', error);
        }
      );
    }
  }

  resetForm(): void {
    this.supplierForm.reset();
    this.editingSupplierId = null;
  }

  goToDashboard(): void {
    this.router.navigate(['/dashboard']);
  }
}
