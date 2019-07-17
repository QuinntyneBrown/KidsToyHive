import { async, TestBed } from '@angular/core/testing';
import { DomainModule } from './domain.module';

describe('DomainModule', () => {
  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [DomainModule]
    }).compileComponents();
  }));

  it('should create', () => {
    expect(DomainModule).toBeDefined();
  });
});
