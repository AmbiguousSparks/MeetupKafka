import { TestBed } from '@angular/core/testing';

import { TokenMiddlewareService } from './token-middleware.service';

describe('TokenMiddlewareService', () => {
  let service: TokenMiddlewareService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(TokenMiddlewareService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
