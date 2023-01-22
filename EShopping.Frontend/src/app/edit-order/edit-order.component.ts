import { Component, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { HttpProviderService } from '../Service/http-provider.service';
import { Validator } from 'fluentvalidation-ts';

@Component({
  selector: 'app-edit-order',
  templateUrl: './edit-order.component.html',
  styleUrls: ['./edit-order.component.scss']
})
export class EditOrderComponent implements OnInit {
  editOrderForm: orderForm = new orderForm();
  orderUpdateFormValidate: OrderUpdateValidator = new OrderUpdateValidator();
  validateResult: any = null;

  @ViewChild("orderForm")
  orderForm!: NgForm;

  isSubmitted: boolean = false;
  orderId: any;

  constructor(private toastr: ToastrService, private route: ActivatedRoute, private router: Router,
    private httpProvider: HttpProviderService) { }

  ngOnInit(): void {
    this.orderId = this.route.snapshot.params['orderId'];
    this.getOrderDetailById();
  }
  getOrderDetailById() {
    this.httpProvider.getOrderDetailById(this.orderId).subscribe((data: any) => {
      if (data != null && data.body != null) {
        var resultData = data.body;
        if (resultData) {
          this.editOrderForm.id = resultData.id;
          this.editOrderForm.trackingNumber = resultData.trackingNumber;
          this.editOrderForm.shippingAdress = resultData.shippingAdress;
          this.editOrderForm.orderDate = resultData.orderDate;
          this.editOrderForm.orderItemsDtoModel[0].id = resultData.orderItems[0].id;
          this.editOrderForm.orderItemsDtoModel[0].productId = resultData.orderItems[0].productId;
          this.editOrderForm.orderItemsDtoModel[0].price.amount = resultData.orderItems[0].price.amount;
          this.editOrderForm.orderItemsDtoModel[0].price.unit = resultData.orderItems[0].price.unit;
        }
      }
    },
      (error: any) => { });
  }

  EditOrder() {
    this.isSubmitted = true;
    this.validateResult = this.orderUpdateFormValidate.validate(this.editOrderForm);
    if (Object.keys(this.validateResult).length == 0) {
      this.httpProvider.updateOrder(this.editOrderForm).subscribe(async data => {
        if (data != null && data.status == 200) {
          this.toastr.success("Updated successfully.");
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
type OrderItemsDtoModel = Array<{id: number, productId: number, price: Price}>;
export class orderForm {
  id: number = 0;
  trackingNumber: string = "";
  shippingAdress: string = "";
  orderDate: string = "";
  orderItemsDtoModel: OrderItemsDtoModel = [
    {
      id: 0,
      productId: 0,
      price: {
        amount: 0,
        unit: 0,
      }
    }
  ];
}

class OrderUpdateValidator extends Validator<orderForm> {
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