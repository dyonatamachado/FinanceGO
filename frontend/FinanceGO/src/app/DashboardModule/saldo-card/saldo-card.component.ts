import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'fg-saldo-card',
  templateUrl: './saldo-card.component.html',
  styleUrls: ['./saldo-card.component.scss']
})
export class SaldoCardComponent implements OnInit {
  total: number = 1500 - 867.5

  constructor() { }

  ngOnInit(): void {
  }

}
