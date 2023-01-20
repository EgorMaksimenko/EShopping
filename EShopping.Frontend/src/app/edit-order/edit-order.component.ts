import { Component, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { HttpProviderService } from '../Service/http-provider.service';

@Component({
  selector: 'app-edit-order',
  templateUrl: './edit-order.component.html',
  styleUrls: ['./edit-order.component.scss']
})
export class EditOrderComponent implements OnInit {
  editOrderForm: orderForm = new orderForm();

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
        }
      }
    },
      (error: any) => { });
  }

  EditOrder(isValid: any) {
    this.isSubmitted = true;
    if (isValid) {
      this.httpProvider.saveOrder(this.editOrderForm).subscribe(async data => {
        if (data != null && data.body != null) {
          var resultData = data.body;
          if (resultData != null && resultData.isSuccess) {
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

export class orderForm {
  id: number = 0;
  trackingNumber: string = "";
  shippingAdress: string = "";
  orderDate: string = "";
}