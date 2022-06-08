import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'fg-despesa-card',
  templateUrl: './despesa-card.component.html',
  styleUrls: ['./despesa-card.component.scss']
})
export class DespesaCardComponent implements OnInit {
  mes: string = 'Março'
  ano: number = 2022
  total: number = 867.50

  constructor() { }

  ngOnInit(): void {
  }

}
