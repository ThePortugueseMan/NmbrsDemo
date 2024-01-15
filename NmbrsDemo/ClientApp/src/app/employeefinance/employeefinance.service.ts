import { Component, Inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root', // or specify a module if you want to limit the scope
})

export class EmployeeFinanceService {
  private resource: string = 'employeefinance'

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
  }

  public getEmployeeFinanceById(employeeId: string): Observable<EmployeeFinance> {
    return this.http.get<EmployeeFinance>(this.baseUrl + this.resource + `?employeeid=${employeeId}`);
  }

  public setAnnualGrossSalaryById(employeeId: string, grossAnnualSalary: string): Observable<boolean> {
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    const requestBody = { grossAnnualSalary: grossAnnualSalary };
    return this.http.post<boolean>(
      this.baseUrl + this.resource + `?employeeid=${employeeId}`,
      requestBody,
      { headers });
  } 
}

interface EmployeeFinance {
  grossAnnualSalary: string;
  netAnnualSalary: string;
  annualCostToEmployer: string;
}
