import { Component, OnInit } from '@angular/core';
import { EmployeeserviceService } from '../employeeservice.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-add-employee',
  templateUrl: './add-employee.component.html',
  styleUrl: './add-employee.component.css'
})
export class AddEmployeeComponent implements OnInit {

  department:any;
  designation:any;
  name: string = '';
  employeeCode: string = '';
  gender: string = '';
  Designation: string = '';
  Department: string = '';
  constructor(private employeeService:EmployeeserviceService, private router:Router){}

  ngOnInit(): void {
    this.getdepartment();
    this.getdesignation();
    
  }

  addExpense(data:any){
    debugger;
    this.employeeService.addEmployee(data).subscribe(Response => {
      this.department = Response;
      this.router.navigate(['/employee']);
    });
  }

  getdepartment(){
    this.employeeService.getdepartment().subscribe(Response => {
      this.department = Response;
    });
  }

  getdesignation(){
    this.employeeService.getdesignation().subscribe(Response => {
      this.designation = Response;
    });
  }
}
