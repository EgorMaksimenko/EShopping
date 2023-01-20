import { Component, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { HttpProviderService } from '../Service/http-provider.service';

@Component({
  selector: 'app-add-order',
  templateUrl: './add-order.component.html',
  styleUrls: ['./add-order.component.scss']
})
export class AddOrderComponent implements OnInit {
  addOrderForm: orderForm = new orderForm();

  @ViewChild("orderForm")
  orderForm!: NgForm;
  isSubmitted: boolean = false;
  constructor(private router: Router, private httpProvider: HttpProviderService, private toastr: ToastrService) { }

  ngOnInit(): void {  }

  AddOrder(isValid: any) {
    this.isSubmitted = true;
    if (isValid) {
      this.httpProvider.saveOrder(this.addOrderForm).subscribe(async data => {
        if (data != null && data.body != null) {
          if (data != null && data.body != null) {
            var resultData = data.body;
            if (resultData != null && resultData.isSuccess) {
              this.toastr.success(resultData.message);
              setTimeout(() => {
                this.router.navigate(['/Home']);
              }, 500);
            }
          }
        }
      },
        async error => {
          this.toastr.error(error.message);
          setTimeout(() => {
            this.router.navigate(['/Home']);
          }, 500);
        });
    }
  }
}

type Price = {amount: number, unit: number};
type OrderItemsDtoModel = Array<{productId: number, price: Price}>;
export class orderForm {
  shippingAdress: string = "";
  orderItemsDtoModel: OrderItemsDtoModel = [
    {
      productId: 0,
      price: {
        amount: 0,
        unit: 0,
      }
    }
  ];
}