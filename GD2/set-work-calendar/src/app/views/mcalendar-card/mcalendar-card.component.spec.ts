import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MCalendarCardComponent } from './mcalendar-card.component';

describe('MCalendarCardComponent', () => {
  let component: MCalendarCardComponent;
  let fixture: ComponentFixture<MCalendarCardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MCalendarCardComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(MCalendarCardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
