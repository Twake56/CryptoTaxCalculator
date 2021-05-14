import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
//import { FileUploadComponent } from './'

@Component({
  selector: 'app-tax-calculator',
  templateUrl: './tax-calculator.component.html'
})
export class TaxCalculatorComponent {

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {

  }
}
