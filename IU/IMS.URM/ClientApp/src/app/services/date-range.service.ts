import { Injectable } from '@angular/core';
import { Subject, BehaviorSubject } from 'rxjs';
import { DateRangeDto } from '../dto/date-range-dto';

@Injectable()
export class DateRangeService {
  private datesChangedSubject = new BehaviorSubject<DateRangeDto>({});

  constructor() { }

  get datesChanged() {
    return this.datesChangedSubject.asObservable();
  }

  setValue(dto: DateRangeDto) {
    this.datesChangedSubject.next(dto);
  }
}
