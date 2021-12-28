import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TheSideBarLeftComponent } from './the-side-bar-left.component';

describe('TheSideBarLeftComponent', () => {
  let component: TheSideBarLeftComponent;
  let fixture: ComponentFixture<TheSideBarLeftComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TheSideBarLeftComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TheSideBarLeftComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
