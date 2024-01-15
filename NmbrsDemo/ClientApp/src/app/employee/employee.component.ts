import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { EmployeeService } from './employee.service';
import { EmployeeFinanceComponent } from '../employeefinance/employeefinance.component';
import { first, last, Subject, takeUntil } from 'rxjs';

@Component({
  selector: 'app-employee',
  templateUrl: './employee.component.html'
})
export class EmployeeComponent {
  public employeeList: Employee[] = [];
  public baseUrl: string = '';

  public showAddEmployeeCell = false;
  public deleteIsActive = false;
  public showDetails: boolean = false;

  public newEmployee: Employee = NewEmployee();
  public newEmployeeFirstName: string = '';
  public newEmployeeLastName: string = '';

  public fetchedList = false;
  public detailEmployeeId: string = '';

  private unsubscribe$ = new Subject<void>();
  

  //public employeeService: EmployeeService;

  constructor(@Inject('BASE_URL') baseUrl: string, private employeeService: EmployeeService) {
  }

  ngOnInit(): void {
    //this.employeeList = this.employeeService.getEmployeeList();
    //while (this.employeeList.length == 0) this.fetchedList = true;
    this.getEmployeeListFromService();
    
  }

  getEmployeeListFromService() {
    this.employeeService.getEmployeeList()
      .pipe(takeUntil(this.unsubscribe$))
      .subscribe(
        result => {
          this.employeeList = result;
          this.fetchedList = true;
        },
        error => console.error(error)
      );
  }

  newEmployeeClick() {
    this.showAddEmployeeCell = true;
    var maxId = this.employeeList.reduce((a, b) => a.employeeId > b.employeeId ? a : b).employeeId;
    this.newEmployee.employeeId = (Number(maxId) + 1).toString();
  }

  cancelNewEmployeeClick(){
    this.showAddEmployeeCell = false;
  }

  removeEmployeeClick() {
    this.deleteIsActive = true;
  }

  cancelRemoveEmployeeClick() {
    this.deleteIsActive = false;
  }

  removeSelectedEmployeeClick(employeeId: string) {
    this.employeeService.removeEmployeeByEmployeeId(employeeId)
      .pipe(takeUntil(this.unsubscribe$))
      .subscribe(
        result => {
          this.getEmployeeListFromService();
        },
        error => console.error(error)
      );

    this.newEmployee = NewEmployee();;
    this.deleteIsActive = false;
  }

  submitNewEmployeeClick(firstName: string, lastName: string) {
    this.newEmployee.firstName = firstName;
    this.newEmployee.lastName = lastName;
    this.employeeService.setNewEmployee(this.newEmployee)
      .pipe(takeUntil(this.unsubscribe$))
      .subscribe(
        result => {
          this.getEmployeeListFromService();
        },
        error => console.error(error)
      );

    this.newEmployee = NewEmployee();
    this.newEmployeeFirstName = '';
    this.newEmployeeLastName = '';
  }

  detailsClick(employeeId: string) {
    this.detailEmployeeId = employeeId;
    this.showDetails = true;
  }
}

interface Employee {
  employeeId: string;
  firstName: string;
  lastName: string;
  //status: string;
}

function NewEmployee(): Employee {
  return {
    employeeId: '',
    firstName: '',
    lastName: '',
    //status: ''
  }
}
