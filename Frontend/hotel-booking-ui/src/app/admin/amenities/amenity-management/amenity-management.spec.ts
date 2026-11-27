import { ComponentFixture, TestBed } from '@angular/core/testing';
import { AmenityManagement } from './amenity-management';

describe('AmenityManagement', () => {
  let component: AmenityManagement;
  let fixture: ComponentFixture<AmenityManagement>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AmenityManagement],
    }).compileComponents();

    fixture = TestBed.createComponent(AmenityManagement);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
