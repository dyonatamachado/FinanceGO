import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DashboardComponent } from './dashboard/dashboard.component';
import { DespesaCardComponent } from './despesa-card/despesa-card.component';
import { ReceitaCardComponent } from './receita-card/receita-card.component';
import { SaldoCardComponent } from './saldo-card/saldo-card.component';


@NgModule({
  declarations: [
    DashboardComponent,
    DespesaCardComponent,
    ReceitaCardComponent,
    SaldoCardComponent
  ],
  imports: [
    CommonModule
  ],
  exports: [
    DashboardComponent
  ]
})
export class DashboardModule { }
