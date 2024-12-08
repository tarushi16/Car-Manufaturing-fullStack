import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http'; // Import HttpClientModule
import { AppRoutingModule } from './app.routes'; // Import routing module
import { FormsModule, ReactiveFormsModule } from '@angular/forms'; // Import FormsModule
import { LoginComponent } from './authentication/login/login.component'; // Import Login component
import { RegisterComponent } from './authentication/register/register.component'; // Import Register component
import { AppComponent } from './app.component';
import { CarModelsComponent } from './car-models/car-models.component';
import { CustomerComponent } from './customer/customer.component';
import { FinanceComponent } from './finance/finance.component';
import { AuthInterceptor } from './authentication/auth.interceptor';
import { InventoryComponent } from './inventory/inventory.component';
import { ProductionComponent } from './production/production.component';
import { QualityControlComponent } from './quality-control/quality-control.component';
import { ReportComponent } from './report/report.component';
import { SalesOrderComponent } from './sales-order/sales-order.component';
import { SupplierComponent } from './supplier/supplier.component';
import { UserComponent } from './user/user.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,  // Removed AppComponent from declarations
    RegisterComponent,
    CarModelsComponent,
    CustomerComponent,
    FinanceComponent,
    InventoryComponent,
    ProductionComponent,
    QualityControlComponent,
    ReportComponent,
    SalesOrderComponent,
    SupplierComponent,
    UserComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,      // Add FormsModule here
    ReactiveFormsModule
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,  // Use the interceptor class
      multi: true                 // Ensures multiple interceptors can be applied
    }],
  bootstrap: [AppComponent]
})
export class AppModule {}
