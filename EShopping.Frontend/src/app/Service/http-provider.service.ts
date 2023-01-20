import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { WebApiService } from './web-api.service';

var apiUrl = "https://localhost:5001";

var httpLink = {
  getAllOrder: apiUrl + "/api/Order/GetAll",
  deleteOrderById: apiUrl + "/api/Order/deleteOrderById",
  getOrderDetailById: apiUrl + "/api/Order/",
  saveOrder: apiUrl + "/api/Order/Add"
}
@Injectable({
  providedIn: 'root'
})
export class HttpProviderService {
  constructor(private webApiService: WebApiService) { }

  public getAllOrder(): Observable<any> {
    return this.webApiService.get(httpLink.getAllOrder);
  }
  public deleteOrderById(model: any): Observable<any> {
    return this.webApiService.post(httpLink.deleteOrderById + '?orderId=' + model, "");
  }
  public getOrderDetailById(model: any): Observable<any> {
    return this.webApiService.get(httpLink.getOrderDetailById + model);
  }
  public saveOrder(model: any): Observable<any> {
    return this.webApiService.post(httpLink.saveOrder, model);
  }  
}     