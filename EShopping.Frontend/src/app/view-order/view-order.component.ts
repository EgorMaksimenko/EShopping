import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { HttpProviderService } from '../Service/http-provider.service';
import { WebApiService } from '../Service/web-api.service';

@Component({
  selector: 'app-view-order',
  templateUrl: './view-order.component.html',
  styleUrls: ['./view-order.component.scss']
})
export class ViewOrderComponent implements OnInit {

  orderId: any;
  orderDetail : any= [];
   
  constructor(public webApiService: WebApiService, private route: ActivatedRoute, private httpProvider : HttpProviderService) { }
  
  ngOnInit(): void {
    this.orderId = this.route.snapshot.params['orderId'];      
    this.getOrderDetailById();
  }

  getOrderDetailById() {       
    this.httpProvider.getOrderDetailById(this.orderId).subscribe((data : any) => {      
      if (data != null && data.body != null) {
        var resultData = data.body;
        if (resultData) {
          this.orderDetail = resultData;
        }
      }
    },
    (error :any)=> { }); 
  }

}