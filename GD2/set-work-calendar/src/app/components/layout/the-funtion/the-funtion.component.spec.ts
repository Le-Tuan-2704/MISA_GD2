import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TheFuntionComponent } from './the-funtion.component';

describe('TheFuntionComponent', () => {
  let component: TheFuntionComponent;
  let fixture: ComponentFixture<TheFuntionComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TheFuntionComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TheFuntionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
