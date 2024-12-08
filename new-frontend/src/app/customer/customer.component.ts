import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { CustomerService } from './customer.service';

@Component({
  selector: 'app-customer',
  templateUrl: './customer.component.html',
  styleUrls: ['./customer.component.scss']
})
export class CustomerComponent implements OnInit {
  customers: any[] = [];  // Directly using 'any' instead of a Customer interface
  selectedCustomer: any = null;  // Directly using 'any' instead of a Customer interface
  customerForm: FormGroup;

  constructor(
    private fb: FormBuilder,
    private customerService: CustomerService,
    private router: Router
  ) {
    this.customerForm = this.fb.group({
      customerId: [null],
      name: ['', Validators.required],
      contactDetails: ['', Validators.required],
      purchaseHistory: ['', Validators.maxLength(1000)],
      status: ['Active', Validators.required]
    });
  }

  ngOnInit(): void {
    this.loadCustomers();
  }

  loadCustomers() {
    this.customerService.getCustomers().subscribe((data: any[]) => {
      this.customers = data;
    });
  }

  onEdit(customer: any) {
    this.selectedCustomer = customer;
    this.customerForm.patchValue(customer);
  }

  onSubmit() {
    if (this.customerForm.invalid) {
      alert('Please fill in all required fields!');
      return;
    }

    const customer = this.customerForm.value;
    if (customer.customerId) {
      this.customerService.updateCustomer(customer).subscribe(() => {
        alert('Customer updated successfully!');
        this.resetForm();
        this.loadCustomers();
      });
    } else {
      this.customerService.addCustomer(customer).subscribe(() => {
        alert('Customer added successfully!');
        this.resetForm();
        this.loadCustomers();
      });
    }
  }

  onDelete(id: number) {
    if (confirm('Are you sure you want to delete this customer?')) {
      this.customerService.deleteCustomer(id).subscribe(() => {
        alert('Customer deleted successfully!');
        this.loadCustomers();
      });
    }
  }

  resetForm() {
    this.customerForm.reset({
      customerId: null,
      name: '',
      contactDetails: '',
      purchaseHistory: '',
      status: 'Active'
    });
    this.selectedCustomer = null;
  }

  goToDashboard() {
    this.router.navigate(['/dashboard']);
  }
}
