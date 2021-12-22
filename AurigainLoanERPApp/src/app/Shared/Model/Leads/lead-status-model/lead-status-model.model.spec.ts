import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LeadStatusModelModel } from './lead-status-model.model';

describe('LeadStatusModelModel', () => {
  let component: LeadStatusModelModel;
  let fixture: ComponentFixture<LeadStatusModelModel>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ LeadStatusModelModel ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(LeadStatusModelModel);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
