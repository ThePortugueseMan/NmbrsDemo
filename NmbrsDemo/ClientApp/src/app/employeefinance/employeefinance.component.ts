import { Component, Inject, Input } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { EmployeeFinanceService } from './employeefinance.service';
import { first, last, Subject, takeUntil } from 'rxjs';

@Component({
  selector: 'app-employeefinance',
  templateUrl: './employeefinance.component.html'
})
export class EmployeeFinanceComponent {
  @Input() employeeId: string = '';

  public baseUrl: string = '';
  public fetchedInfo: boolean = false;
  public newGrossAnnualSalary: string = '';
  public editMode: boolean = false;
  public financeInfo: EmployeeFinance = NewEmployeeFinance()
  private unsubscribe$ = new Subject<void>();

  constructor(@Inject('BASE_URL') baseUrl: string, private employeeService: EmployeeFinanceService) {
  }

  ngOnInit(): void {
    this.getEmployeeFinanceFromService(); 
  }

  getEmployeeFinanceFromService() {
    this.employeeService.getEmployeeFinanceById(this.employeeId)
      .pipe(takeUntil(this.unsubscribe$))
      .subscribe(
        result => {
          this.financeInfo = result;
          this.fetchedInfo = true;
        },
        error => console.error(error)
      );
  }

  editButtonClick() {
    this.editMode = true;
  }

  backButtonClick() {

  }

  cancelEditButtonClick() {
    this.editMode = false;
  }

  submitNewGrossAnnualSalary(grossAnnualSalary: string) {
    this.employeeService.setAnnualGrossSalaryById(this.employeeId, grossAnnualSalary)
      .pipe(takeUntil(this.unsubscribe$))
      .subscribe(
        result => {
          this.getEmployeeFinanceFromService()
        },
        error => console.error(error)
      );
  }
}

interface EmployeeFinance {
  grossAnnualSalary: string;
  netAnnualSalary: string;
  annualCostToEmployer: string;
}

function NewEmployeeFinance(): EmployeeFinance {
  return {
    grossAnnualSalary: '',
    netAnnualSalary: '',
    annualCostToEmployer: '',
  }
}
