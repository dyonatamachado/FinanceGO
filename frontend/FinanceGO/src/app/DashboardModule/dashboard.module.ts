import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DashboardComponent } from './dashboard/dashboard.component';
import { DespesaCardComponent } from './despesa-card/despesa-card.component';


@NgModule({
  declarations: [
    DashboardComponent,
    DespesaCardComponent
  ],
  imports: [
    CommonModule
  ],
  exports: [
    DashboardComponent
  ]
})
export class DashboardModule { }
