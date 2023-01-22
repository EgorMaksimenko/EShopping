import { Component, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { HttpProviderService } from '../Service/http-provider.service';
import { Validator } from 'fluentvalidation-ts';

@Component({
  selector: 'app-add-order',
  templateUrl: './add-order.component.html',
  styleUrls: ['./add-order.component.scss']
})

export class AddOrderComponent implements OnInit {
  addOrderForm: orderForm = new orderForm();
  orderFormValidate: OrderValidator = new OrderValidator();
  validateResult: any = null;

  @ViewChild("orderForm")
  orderForm!: NgForm;
  isSubmitted: boolean = false;
  constructor(private router: Router, private httpProvider: HttpProviderService, private toastr: ToastrService) { }

  ngOnInit(): void {  }

  AddOrder() {
    this.isSubmitted = true;
    this.validateResult = this.orderFormValidate.validate(this.addOrderForm);
    if (Object.keys(this.validateResult).length == 0) {
      this.httpProvider.saveOrder(this.addOrderForm).subscribe(async data => {
        if (data != null && data.status == 200) {
          this.toastr.success("Added successfully.");
          setTimeout(() => {
            this.router.navigate(['/Home']);
          }, 500);
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

class OrderValidator extends Validator<orderForm> {
  constructor() {
    super();

    this.ruleFor('shippingAdress')
      .notEmpty()
      .withMessage('Please enter Shipping Address')
      .maxLength(256)
      .withMessage('Shipping Address length was exceed.');

    this.ruleFor('orderItemsDtoModel')
    .notNull()
    .must(items => items[0].productId >= 1 && items[0].productId < 99999)
    .withMessage('Please enter right product id.')
    .must(items => items[0].price.amount > 0 && items[0].price.amount < 100)
    .withMessage('Please enter right amount.')
    .must(items => items[0].price.unit >= 0 && items[0].price.unit < 4)
    .withMessage('Please enter right unit.');
  }
}