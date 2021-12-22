import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DetailPurposeComponent } from './detail-purpose.component';

describe('DetailPurposeComponent', () => {
  let component: DetailPurposeComponent;
  let fixture: ComponentFixture<DetailPurposeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DetailPurposeComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DetailPurposeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
