import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';

@Component({
  selector: 'app-inventory',
  templateUrl: './inventory.component.html',
  styleUrls: ['./inventory.component.scss'],
  
})
export class InventoryComponent implements OnInit {
  inventoryForm: FormGroup;
  inventoryList: any[] = [];
  apiUrl: string = 'https://your-api-url.com/api/Inventory';
  editingInventoryId: number | null = null;

  constructor(private fb: FormBuilder, private http: HttpClient, private router: Router) {
    this.inventoryForm = this.fb.group({
      componentName: ['', [Validators.required, Validators.maxLength(200)]],
      quantity: [0, [Validators.required, Validators.min(0)]],
      supplierId: [0, [Validators.required]],
      stockLevel: [0, [Validators.required, Validators.min(0)]],
      reorderThreshold: [0, [Validators.required, Validators.min(0)]],
    });
  }

  ngOnInit(): void {
    this.fetchInventory();
  }

  // Fetch inventory list
  fetchInventory() {
    this.http.get<any[]>(this.apiUrl).subscribe(
      (data) => {
        this.inventoryList = data;
      },
      (error) => {
        console.error('Error fetching inventory:', error);
      }
    );
  }

  // Submit form (add or update)
  onSubmit() {
    if (this.editingInventoryId === null) {
      // Add new inventory
      this.http.post(this.apiUrl, this.inventoryForm.value).subscribe(
        () => {
          alert('Inventory added successfully!');
          this.fetchInventory();
          this.inventoryForm.reset();
        },
        (error) => {
          console.error('Error adding inventory:', error);
        }
      );
    } else {
      // Update existing inventory
      this.http
        .put(`${this.apiUrl}/${this.editingInventoryId}`, this.inventoryForm.value)
        .subscribe(
          () => {
            alert('Inventory updated successfully!');
            this.fetchInventory();
            this.inventoryForm.reset();
            this.editingInventoryId = null;
          },
          (error) => {
            console.error('Error updating inventory:', error);
          }
        );
    }
  }

  // Edit inventory
  editInventory(item: any) {
    this.editingInventoryId = item.inventoryId;
    this.inventoryForm.patchValue({
      componentName: item.componentName,
      quantity: item.quantity,
      supplierId: item.supplierId,
      stockLevel: item.stockLevel,
      reorderThreshold: item.reorderThreshold,
    });
  }

  // Delete inventory
  deleteInventory(id: number) {
    if (confirm('Are you sure you want to delete this inventory item?')) {
      this.http.delete(`${this.apiUrl}/${id}`).subscribe(
        () => {
          alert('Inventory deleted successfully!');
          this.fetchInventory();
        },
        (error) => {
          console.error('Error deleting inventory:', error);
        }
      );
    }
  }
  goToDashboard() {
    this.router.navigate(['/dashboard']);
  }
}
