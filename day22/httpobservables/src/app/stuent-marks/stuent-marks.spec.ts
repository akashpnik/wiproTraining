import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StuentMarks } from './stuent-marks';

describe('StuentMarks', () => {
  let component: StuentMarks;
  let fixture: ComponentFixture<StuentMarks>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [StuentMarks]
    })
    .compileComponents();

    fixture = TestBed.createComponent(StuentMarks);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
