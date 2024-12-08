import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ProductionService } from './production.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-production',
  templateUrl: './production.component.html',
  styleUrls: ['./production.component.scss']
})
export class ProductionComponent implements OnInit {
  productionForm: FormGroup;
  productionOrders: any[] = [];
  editingOrderId: number | null = null;

  constructor(
    private fb: FormBuilder,
    private productionService: ProductionService,
    private router: Router
  ) {
    this.productionForm = this.fb.group({
      carModelId: [null, [Validators.required, Validators.min(1)]],
      startDate: ['', Validators.required],
      endDate: ['', Validators.required],
      quantity: [null, [Validators.required, Validators.min(1)]],
      status: ['', [Validators.required, Validators.maxLength(50)]],
    });
  }

  ngOnInit(): void {
    this.fetchProductionOrders();
  }

  // Fetch all production orders
  fetchProductionOrders(): void {
    this.productionService.getAllProductionOrders().subscribe(
      (data) => {
        this.productionOrders = data;
      },
      (error) => {
        console.error('Error fetching production orders:', error);
      }
    );
  }

  // Create or update a production order
  onSubmit(): void {
    if (this.productionForm.invalid) {
      return;
    }

    const productionOrder = this.productionForm.value;

    if (this.editingOrderId) {
      // Update existing order
      this.productionService.updateProductionOrder(this.editingOrderId, productionOrder).subscribe(
        () => {
          alert('Production order updated successfully!');
          this.fetchProductionOrders();
          this.productionForm.reset();
          this.editingOrderId = null;
        },
        (error) => {
          console.error('Error updating production order:', error);
        }
      );
    } else {
      // Create new order
      this.productionService.createProductionOrder(productionOrder).subscribe(
        () => {
          alert('Production order created successfully!');
          this.fetchProductionOrders();
          this.productionForm.reset();
        },
        (error) => {
          console.error('Error creating production order:', error);
        }
      );
    }
  }

  // Edit an existing production order
  editOrder(order: any): void {
    this.editingOrderId = order.orderId; // Set the ID of the order being edited
    this.productionForm.patchValue({
      carModelId: order.carModelId,
      startDate: order.startDate,
      endDate: order.endDate,
      quantity: order.quantity,
      status: order.status
    });
  }

  // Delete a production order
  deleteOrder(orderId: number): void {
    if (confirm('Are you sure you want to delete this production order?')) {
      this.productionService.deleteProductionOrder(orderId).subscribe(
        () => {
          alert('Production order deleted successfully!');
          this.fetchProductionOrders();
        },
        (error) => {
          console.error('Error deleting production order:', error);
        }
      );
    }
  }

  // Navigate to the dashboard
  goToDashboard(): void {
    this.router.navigate(['/dashboard']);
  }
}
