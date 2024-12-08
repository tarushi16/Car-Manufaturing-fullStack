import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ReportService } from './report.service'; // Import the service for API calls
import { Router } from '@angular/router'; // Import the router for navigation
@Component({
  selector: 'app-report',
  templateUrl: './report.component.html',
  styleUrls: ['./report.component.scss']
})
export class ReportComponent implements OnInit {
  reportForm!: FormGroup;  // Non-null assertion to inform TypeScript it will be initialized later
  reports: any[] = [];  // Using any[] to store the report data directly
  editingReportId: number | null = null;

  constructor(
    private fb: FormBuilder,
    private reportService: ReportService,
    private router: Router
  ) { }

  ngOnInit(): void {
    // Initialize the form
    this.reportForm = this.fb.group({
      type: ['', [Validators.required]],
      generatedDate: ['', [Validators.required]],
      data: ['', [Validators.required]],
      createdBy: ['', [Validators.required]]
    });

    // Fetch all reports on initialization
    this.getReports();
  }

  // Get all reports
  getReports(): void {
    this.reportService.getAllReports().subscribe(
      (data: any[]) => {
        this.reports = data;
      },
      (error) => {
        console.error('Error fetching reports', error);
      }
    );
  }

  // Get a single report by ID
  getReport(id: number): void {
    this.reportService.getReport(id).subscribe(
      (data: any) => {
        this.editingReportId = id;
        this.reportForm.setValue({
          type: data.type,
          generatedDate: data.generatedDate,
          data: data.data,
          createdBy: data.createdBy
        });
      },
      (error) => {
        console.error('Error fetching report', error);
      }
    );
  }

  // Create or update report based on the form state
  onSubmit(): void {
    if (this.reportForm.invalid) {
      return;
    }

    const reportData = this.reportForm.value;

    if (this.editingReportId) {
      // Update existing report
      this.reportService.updateReport(this.editingReportId, reportData).subscribe(
        () => {
          this.getReports();
          this.resetForm();
        },
        (error) => {
          console.error('Error updating report', error);
        }
      );
    } else {
      // Create new report
      this.reportService.createReport(reportData).subscribe(
        () => {
          this.getReports();
          this.resetForm();
        },
        (error) => {
          console.error('Error creating report', error);
        }
      );
    }
  }

  // Edit an existing report (fill form with the selected report's data)
  editReport(report: any): void {
    this.editingReportId = report.reportId;
    this.getReport(report.reportId);
  }

  // Delete a report by ID
  deleteReport(reportId: number): void {
    if (confirm('Are you sure you want to delete this report?')) {
      this.reportService.deleteReport(reportId).subscribe(
        () => {
          this.getReports();
        },
        (error) => {
          console.error('Error deleting report', error);
        }
      );
    }
  }

  // Reset the form to create a new report
  resetForm(): void {
    this.reportForm.reset();
    this.editingReportId = null;
  }

  // Go to the dashboard (you can modify the navigation logic based on your routing)
goToDashboard(): void {
    this.router.navigate(['/dashboard']);
  }
}
