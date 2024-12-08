// quality-control.component.ts

import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { QualityControlService } from './quality-control.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-quality-control',
  templateUrl: './quality-control.component.html',
  styleUrls: ['./quality-control.component.scss']
})
export class QualityControlComponent implements OnInit {
  qualityControlForm: FormGroup;
  qualityReports: any[] = [];
  editingReportId: number | null = null;

  constructor(
    private fb: FormBuilder,
    private qualityControlService: QualityControlService,
    private router: Router
  ) {
    this.qualityControlForm = this.fb.group({
      carModelId: [null, [Validators.required, Validators.min(1)]],
      inspectionDate: ['', Validators.required],
      inspectorId: [null, [Validators.required, Validators.min(1)]],
      testResults: ['', Validators.required],
      defectsFound: ['', Validators.required],
      status: ['', Validators.required],
    });
  }

  ngOnInit(): void {
    this.fetchQualityReports();
  }

  fetchQualityReports(): void {
    this.qualityControlService.getAllQualityReports().subscribe(
      (data) => {
        this.qualityReports = data;
      },
      (error) => {
        console.error('Error fetching quality reports:', error);
      }
    );
  }

  onSubmit() {
    if (this.qualityControlForm.valid) {
      console.log('Form submitted', this.qualityControlForm.value);
    }
  

    const qualityReport = this.qualityControlForm.value;

    if (this.editingReportId) {
      // Update the existing report
      this.qualityControlService.updateQualityReport(this.editingReportId, qualityReport).subscribe(
        () => {
          alert('Quality report updated successfully!');
          this.fetchQualityReports();
          this.qualityControlForm.reset();
          this.editingReportId = null;
        },
        (error) => {
          console.error('Error updating quality report:', error);
        }
      );
    } else {
      // Create a new report
      this.qualityControlService.createQualityReport(qualityReport).subscribe(
        (data) => {
          alert('Quality report created successfully!');
          this.fetchQualityReports();
          this.qualityControlForm.reset();
        },
        (error) => {
          console.error('Error creating quality report:', error);
        }
      );
    }
  }

  editReport(report: any): void {
    this.editingReportId = report.reportId;
    this.qualityControlForm.setValue({
      carModelId: report.carModelId,
      inspectionDate: report.inspectionDate,
      inspectorId: report.inspectorId,
      testResults: report.testResults,
      defectsFound: report.defectsFound,
      status: report.status
    });
  }

  deleteReport(reportId: number): void {
    this.qualityControlService.deleteQualityReport(reportId).subscribe(
      () => {
        alert('Quality report deleted successfully!');
        this.fetchQualityReports();
      },
      (error) => {
        console.error('Error deleting quality report:', error);
      }
    );
  }

  goToDashboard(): void {
    this.router.navigate(['/dashboard']);
  }
}
