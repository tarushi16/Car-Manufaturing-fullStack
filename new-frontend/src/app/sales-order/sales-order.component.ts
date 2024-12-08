import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { SalesOrderService } from './sales-order.service';
import { Route, Router } from '@angular/router';

@Component({
  selector: 'app-sales-order',
  templateUrl: './sales-order.component.html',
  styleUrls: ['./sales-order.component.scss']
})
export class SalesOrderComponent implements OnInit {
  salesOrderForm: FormGroup;
  salesOrders: any[] = [];
  editingOrderId: number | null = null;

  constructor(private fb: FormBuilder, private salesOrderService: SalesOrderService, private router: Router) {
    this.salesOrderForm = this.fb.group({
      customerId: ['', Validators.required],
      carModelId: ['', Validators.required],
      orderDate: ['', Validators.required],
      deliveryDate: ['', Validators.required],
      price: ['', [Validators.required, Validators.min(1)]],
      status: ['', Validators.required]
    });
  }

  ngOnInit(): void {
    this.getSalesOrders();
  }

  getSalesOrders(): void {
    this.salesOrderService.getAllSalesOrders().subscribe(
      (orders: any[]) => {
        this.salesOrders = orders;
      },
      (error) => {
        console.error('Error fetching sales orders:', error);
      }
    );
  }

  onSubmit(): void {
    if (this.salesOrderForm.invalid) {
      return;
    }

    const orderData = this.salesOrderForm.value;

    if (this.editingOrderId) {
      this.salesOrderService.updateSalesOrder(this.editingOrderId, orderData).subscribe(
        () => {
          this.getSalesOrders();
          this.resetForm();
        },
        (error) => {
          console.error('Error updating sales order:', error);
        }
      );
    } else {
      this.salesOrderService.createSalesOrder(orderData).subscribe(
        () => {
          this.getSalesOrders();
          this.resetForm();
        },
        (error) => {
          console.error('Error creating sales order:', error);
        }
      );
    }
  }

  editOrder(order: any): void {
    this.editingOrderId = order.orderId;
    this.salesOrderForm.setValue({
      customerId: order.customerId,
      carModelId: order.carModelId,
      orderDate: order.orderDate,
      deliveryDate: order.deliveryDate,
      price: order.price,
      status: order.status
    });
  }

  deleteOrder(orderId: number): void {
    if (confirm('Are you sure you want to delete this order?')) {
      this.salesOrderService.deleteSalesOrder(orderId).subscribe(
        () => {
          this.getSalesOrders();
        },
        (error) => {
          console.error('Error deleting sales order:', error);
        }
      );
    }
  }

  resetForm(): void {
    this.salesOrderForm.reset();
    this.editingOrderId = null;
  }

  goToDashboard(): void {
    this.router.navigate(['/dashboard']);
  }
}
