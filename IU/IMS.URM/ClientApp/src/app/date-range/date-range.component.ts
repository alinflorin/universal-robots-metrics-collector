import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { DateRangeService } from '../services/date-range.service';
import { DateRangeDto } from '../dto/date-range-dto';

@Component({
  selector: 'app-date-range',
  templateUrl: './date-range.component.html',
  styleUrls: ['./date-range.component.scss']
})
export class DateRangeComponent implements OnInit {
  @ViewChild('picker', {static: true}) picker: ElementRef<HTMLInputElement>;

  constructor(public service: DateRangeService) { }

  ngOnInit() {
  }

  onDateTimeChanged(event: any) {
    const dto: DateRangeDto = {};
    if (event.value) {
      dto.startDate = event.value[0];
      dto.endDate = event.value[1];
    }
    this.service.setValue(dto);
  }

  removeDates() {
    this.picker.nativeElement.value = null;
    this.service.setValue({
      endDate: null,
      startDate: null
    });
  }
}
