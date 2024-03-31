import { Component, OnInit } from '@angular/core';
import { EmployeeserviceService } from '../employeeservice.service';

@Component({
  selector: 'app-show-employee',
  templateUrl: './show-employee.component.html',
  styleUrl: './show-employee.component.css'
})
export class ShowEmployeeComponent implements OnInit {
  employees:any;
  dialogbox:boolean=false;
  employeeToDeleteId: any;

  constructor(private employeeService : EmployeeserviceService){}

  ngOnInit(): void {

    
    this.loadExpenses();
  }

  loadExpenses() {
    
    this.employeeService.getEmployees().subscribe(Response => {
      this.employees = Response;
    });
  }

  showConfirmationDialog(id: number): void {
    this.dialogbox=true;
    this.employeeToDeleteId = id;
  }

  

  deleteEmployee(): void {
    this.employeeService.deleteEmployee(this.employeeToDeleteId).subscribe(Response => {
      this.employees = Response;
      this.loadExpenses();
    });
    console.log('Deleting employee with ID:', this.employeeToDeleteId);

    
    this.employeeToDeleteId = null;
  }
 
  canceldel(){
    this.dialogbox=false;
    this.loadExpenses();
  }
}
