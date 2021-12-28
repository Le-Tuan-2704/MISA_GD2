import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TheSideBarRightComponent } from './the-side-bar-right.component';

describe('TheSideBarRightComponent', () => {
  let component: TheSideBarRightComponent;
  let fixture: ComponentFixture<TheSideBarRightComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TheSideBarRightComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TheSideBarRightComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
