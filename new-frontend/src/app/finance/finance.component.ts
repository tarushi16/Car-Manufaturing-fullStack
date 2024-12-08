import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { FinanceService } from './finance.service'; // Assuming this is the service

@Component({
  selector: 'app-finance',
  templateUrl: './finance.component.html',
  styleUrls: ['./finance.component.scss']
})
export class FinanceComponent implements OnInit {
  finances: any[] = [];
  financeForm: FormGroup;
  isFormVisible = false;
  formTitle = 'Add Finance';

  constructor(
    private fb: FormBuilder,
    private financeService: FinanceService,
    private router: Router
  ) {
    this.financeForm = this.fb.group({
      financeId: [null],
      transactionType: ['', Validators.required],
      amount: [null, [Validators.required, Validators.min(0)]],
      date: ['', Validators.required],
      details: ['']
    });
  }

  ngOnInit(): void {
    this.loadFinances();
  }

  loadFinances() {
    this.financeService.getAllFinances().subscribe(data => {
      this.finances = data;
    });
  }

  openFinanceForm() {
    this.isFormVisible = true;
    this.formTitle = 'Add Finance';
    this.financeForm.reset();
  }

  cancelForm() {
    this.isFormVisible = false;
  }

  onSubmit() {
    if (this.financeForm.invalid) {
      return;
    }

    const finance = this.financeForm.value;

    if (finance.financeId) {
      this.financeService.updateFinance(finance.financeId, finance).subscribe(() => {
        this.loadFinances();
        this.cancelForm();
      });
    } else {
      this.financeService.createFinance(finance).subscribe(() => {
        this.loadFinances();
        this.cancelForm();
      });
    }
  }

  onEdit(finance: any) {
    this.isFormVisible = true;
    this.formTitle = 'Edit Finance';
    this.financeForm.patchValue(finance);
  }

  onDelete(financeId: number) {
    if (confirm('Are you sure you want to delete this record?')) {
      this.financeService.deleteFinance(financeId).subscribe(() => {
        this.loadFinances();
      });
    }
  }

  goToDashboard() {
    this.router.navigate(['/dashboard']);
  }
}
