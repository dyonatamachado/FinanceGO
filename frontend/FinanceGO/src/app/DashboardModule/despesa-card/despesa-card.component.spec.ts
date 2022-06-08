import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DespesaCardComponent } from './despesa-card.component';

describe('DespesaCardComponent', () => {
  let component: DespesaCardComponent;
  let fixture: ComponentFixture<DespesaCardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DespesaCardComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DespesaCardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
