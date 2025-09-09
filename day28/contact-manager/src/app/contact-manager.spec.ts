import { TestBed } from '@angular/core/testing';

import { ContactManager } from './contact-manager';

describe('ContactManager', () => {
  let service: ContactManager;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ContactManager);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
