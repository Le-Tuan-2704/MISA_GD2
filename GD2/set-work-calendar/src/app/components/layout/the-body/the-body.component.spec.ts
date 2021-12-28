import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TheBodyComponent } from './the-body.component';

describe('TheBodyComponent', () => {
  let component: TheBodyComponent;
  let fixture: ComponentFixture<TheBodyComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TheBodyComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TheBodyComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
