import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { ShowEmployeeComponent } from './show-employee/show-employee.component';
import { AddEmployeeComponent } from './add-employee/add-employee.component';
import { EditEmployeeComponent } from './edit-employee/edit-employee.component';

const routes: Routes = [
 
  {path: 'Home', component: HomeComponent},
  { path: 'employee', component: ShowEmployeeComponent },
  { path: 'add', component: AddEmployeeComponent },
  { path: 'edit/:id', component: EditEmployeeComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
