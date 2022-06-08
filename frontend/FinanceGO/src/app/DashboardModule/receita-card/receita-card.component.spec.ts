import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ReceitaCardComponent } from './receita-card.component';

describe('ReceitaCardComponent', () => {
  let component: ReceitaCardComponent;
  let fixture: ComponentFixture<ReceitaCardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ReceitaCardComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ReceitaCardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
