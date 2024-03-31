import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';

@Injectable({
  providedIn: 'root'
})
export class EmployeeserviceService {

  apiUrl: string = 'https://localhost:7028/api/Employee';
  constructor(private http: HttpClient) { }

  getEmployees(): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/GetEmployeesWithBasicInfo`);
  }

  deleteEmployee(id: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }

  addEmployee(data:any){

    let url = 'https://localhost:7028/api/Employee/AddEmployee'
    return this.http.post<any>(url, data, { headers: new HttpHeaders({ 'Content-Type': 'application/json' }) });
  }

  getdepartment(){
    return this.http.get<any[]>(`${this.apiUrl}/GetDepartments`);  
  }

  getdesignation(){
    return this.http.get<any[]>(`${this.apiUrl}/GetDesignation`);  
  }
}
