import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TheMenuComponent } from './the-menu.component';

describe('TheMenuComponent', () => {
  let component: TheMenuComponent;
  let fixture: ComponentFixture<TheMenuComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TheMenuComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TheMenuComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
