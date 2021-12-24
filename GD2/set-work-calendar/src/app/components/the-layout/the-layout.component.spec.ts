import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TheLayoutComponent } from './the-layout.component';

describe('TheLayoutComponent', () => {
  let component: TheLayoutComponent;
  let fixture: ComponentFixture<TheLayoutComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TheLayoutComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TheLayoutComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
