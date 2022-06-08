import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'fg-receita-card',
  templateUrl: './receita-card.component.html',
  styleUrls: ['./receita-card.component.scss']
})
export class ReceitaCardComponent implements OnInit {
  total: number = 1500

  constructor() { }

  ngOnInit(): void {
  }

}
