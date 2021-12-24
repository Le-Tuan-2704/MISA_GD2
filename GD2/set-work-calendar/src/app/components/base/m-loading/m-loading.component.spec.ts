import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MLoadingComponent } from './m-loading.component';

describe('MLoadingComponent', () => {
  let component: MLoadingComponent;
  let fixture: ComponentFixture<MLoadingComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MLoadingComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(MLoadingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
