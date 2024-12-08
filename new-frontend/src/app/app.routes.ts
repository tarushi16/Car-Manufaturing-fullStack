import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './authentication/login/login.component';
import { RegisterComponent } from './authentication/register/register.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { CarModelsComponent } from './car-models/car-models.component';
import { CustomerComponent } from './customer/customer.component';
import { FinanceComponent } from './finance/finance.component';
import { InventoryComponent } from './inventory/inventory.component';
import { ProductionComponent } from './production/production.component';
import { QualityControlComponent } from './quality-control/quality-control.component';
import { ReportComponent } from './report/report.component';
import { SalesOrderComponent } from './sales-order/sales-order.component';
import { SupplierComponent } from './supplier/supplier.component';
import { UserComponent } from './user/user.component';

// Exporting the routes array so it can be used in app.config.ts
export const routes: Routes = [
  { path: '', pathMatch:'full', redirectTo:'/login' },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'dashboard', component: DashboardComponent },
  { path: 'car-models', component: CarModelsComponent },
  { path: 'customer', component: CustomerComponent },
  { path: 'finance', component: FinanceComponent},
  { path: 'inventory', component: InventoryComponent },
  { path: 'production', component: ProductionComponent },
  { path: 'quality-control', component: QualityControlComponent },
  { path: 'report', component: ReportComponent },
  { path: 'sales-order', component: SalesOrderComponent },
  { path: 'supplier', component: SupplierComponent},
  { path: 'user', component: UserComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
