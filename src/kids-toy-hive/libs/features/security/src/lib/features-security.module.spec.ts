import { async, TestBed } from '@angular/core/testing';
import { FeaturesSecurityModule } from './features-security.module';

describe('FeaturesSecurityModule', () => {
  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [FeaturesSecurityModule]
    }).compileComponents();
  }));

  it('should create', () => {
    expect(FeaturesSecurityModule).toBeDefined();
  });
});
