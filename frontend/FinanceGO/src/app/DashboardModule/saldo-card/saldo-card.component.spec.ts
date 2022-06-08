import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SaldoCardComponent } from './saldo-card.component';

describe('SaldoCardComponent', () => {
  let component: SaldoCardComponent;
  let fixture: ComponentFixture<SaldoCardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SaldoCardComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SaldoCardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
