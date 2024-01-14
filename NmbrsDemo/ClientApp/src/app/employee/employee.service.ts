import { Component, Inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root', // or specify a module if you want to limit the scope
})

export class EmployeeService {
  public employeeList: Employee[] = [];

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
    this.getEmployeeList(); // Call the method in the constructor to fetch employee data
  }

  public getEmployeeList(): Observable<Employee[]> {
    return this.http.get<Employee[]>(this.baseUrl + 'employee');
    //this.http.get<Employee[]>(this.baseUrl + 'employee').subscribe(result => {
    //  this.employeeList = result;
    //}, error => console.error(error));
    //return this.employeeList;
  }

  public setNewEmployee(newEmployee: Employee): void {
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    this.http.post(this.baseUrl + 'employee', newEmployee, { headers }).subscribe(result => {
      var resultAux = result;
    }, error => console.error(error));
  }

  public removeEmployeeByEmployeeId(employeeId: string): void {
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    this.http.delete(this.baseUrl + `employee?employeeid=${employeeId}`, { headers }).subscribe(result => {
      var resultAux = result;
    }, error => console.error(error));
  }
  
}

interface Employee {
  employeeId: string;
  firstName: string;
  lastName: string;
}

function NewEmployee(): Employee {
  return {
    employeeId: '',
    firstName: '',
    lastName: '',
  }
}
