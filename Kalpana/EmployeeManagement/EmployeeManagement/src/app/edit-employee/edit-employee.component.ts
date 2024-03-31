import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { EmployeeserviceService } from '../employeeservice.service';


@Component({
  selector: 'app-edit-employee',
  templateUrl: './edit-employee.component.html',
  styleUrls: ['./edit-employee.component.css']
})
export class EditEmployeeComponent implements OnInit {

  department:any;
  designation:any;
  name: string = '';
  employeeCode: string = '';
  gender: string = '';
  Designation: string = '';
  Department: string = '';
  constructor(private route: ActivatedRoute,private http: HttpClient, private router:Router, private employeeService:EmployeeserviceService){}

  employeecurrentdata:any;
  ngOnInit(): void {
    
    this.route.paramMap.subscribe(params => {
      const idParam = params.get('id');
      if (idParam) {
        this.getEmployee(idParam);
    }
    });
    this.getdepartment();
    this.getdesignation();
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
  apiUrl = 'https://localhost:7028/api/Employee';
  getEmployee(id: any): void {
    const url = `${this.apiUrl}/${id}`;
    this.http.get(url).subscribe(Response => {
      this.employeecurrentdata = Response;
        
      });
  }

  updateEmployee(employee :any){

    debugger;
    const url = `${this.apiUrl}/UpdateEmployee/${employee.id}`;
  
    console.log('Updating employee:', employee);
    
    this.http.put(url, employee).subscribe(
      (response) => {
        console.log('Update successful:', response);
        this.router.navigate(['employee']);
      },
      (error) => {
        console.error('Error updating employee:', error);
        // Handle error, display message, etc.
      }
    );
  }
}
