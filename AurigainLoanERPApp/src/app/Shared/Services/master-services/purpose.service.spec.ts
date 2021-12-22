import { TestBed } from '@angular/core/testing';

import { PurposeService } from './purpose.service';

describe('PurposeService', () => {
  let service: PurposeService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(PurposeService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
