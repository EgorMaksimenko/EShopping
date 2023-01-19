import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AddOrderComponent } from './add-order/add-order.component';
import { EditOrderComponent } from './edit-order/edit-order.component';
import { HomeComponent } from './home/home.component';
import { ViewOrderComponent } from './view-order/view-order.component';
import { DeleteOrderComponent } from './delete-order/delete-order.component';

const routes: Routes = [
  { path: '', redirectTo: 'Home', pathMatch: 'full'},
  { path: 'Home', component: HomeComponent },
  { path: 'ViewOrder/:orderId', component: ViewOrderComponent },
  { path: 'AddOrder', component: AddOrderComponent },
  { path: 'EditOrder/:orderId', component: EditOrderComponent } ,
  { path: 'DeleteOrder/:orderId', component: DeleteOrderComponent } 
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
