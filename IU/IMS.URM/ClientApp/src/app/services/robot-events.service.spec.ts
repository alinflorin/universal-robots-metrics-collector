import { TestBed } from '@angular/core/testing';

import { RobotEventsService } from './robot-events.service';

describe('RobotEventsService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: RobotEventsService = TestBed.get(RobotEventsService);
    expect(service).toBeTruthy();
  });
});
