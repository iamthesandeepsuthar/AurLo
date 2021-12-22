import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddUpdatePurposeComponent } from './add-update-purpose.component';

describe('AddUpdatePurposeComponent', () => {
  let component: AddUpdatePurposeComponent;
  let fixture: ComponentFixture<AddUpdatePurposeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AddUpdatePurposeComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AddUpdatePurposeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
