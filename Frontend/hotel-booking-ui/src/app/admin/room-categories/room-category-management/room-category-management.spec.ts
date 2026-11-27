import { ComponentFixture, TestBed } from '@angular/core/testing';
import { RoomCategoryManagement } from './room-category-management';

describe('RoomCategoryManagement', () => {
  let component: RoomCategoryManagement;
  let fixture: ComponentFixture<RoomCategoryManagement>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [RoomCategoryManagement],
    }).compileComponents();

    fixture = TestBed.createComponent(RoomCategoryManagement);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
