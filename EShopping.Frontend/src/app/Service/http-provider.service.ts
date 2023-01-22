import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { WebApiService } from './web-api.service';

var apiUrl = "https://localhost:5001";

var httpLink = {
  getAllOrder: apiUrl + "/api/Order/GetAll",
  deleteOrderById: apiUrl + "/api/Order",
  getOrderDetailById: apiUrl + "/api/Order/",
  saveOrder: apiUrl + "/api/Order/Add",
  updateOrder: apiUrl + "/api/Order/Update",
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
    return this.webApiService.delete(httpLink.deleteOrderById + '/' + model, "");
  }
  public getOrderDetailById(model: any): Observable<any> {
    return this.webApiService.get(httpLink.getOrderDetailById + model);
  }
  public saveOrder(model: any): Observable<any> {
    return this.webApiService.post(httpLink.saveOrder, model);
  }
  public updateOrder(model: any): Observable<any> {
    return this.webApiService.put(httpLink.updateOrder, model);
  }
}