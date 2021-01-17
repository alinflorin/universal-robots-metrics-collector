import { Component, OnInit } from '@angular/core';
import { RobotEventsService } from '../services/robot-events.service';
import { ActivatedRoute } from '@angular/router';
import { switchMap, tap, concatMap, mergeMap } from 'rxjs/operators';
import { MetricDto } from '../dto/metric-dto';
import { DateRangeService } from '../services/date-range.service';

@Component({
  selector: 'app-metrics',
  templateUrl: './metrics.component.html',
  styleUrls: ['./metrics.component.scss']
})
export class MetricsComponent implements OnInit {
  ip: string;
  robotName: string;
  metrics: MetricDto[];

  constructor(private robotEventsService: RobotEventsService, private actRoute: ActivatedRoute, private drService: DateRangeService) { }

  ngOnInit() {
    this.actRoute.queryParams.subscribe(qp => {
      this.robotName = qp['robotName'];
      this.actRoute.params.subscribe(params => {
        this.ip = params['ip'];
        this.drService.datesChanged.pipe(
          mergeMap(dates => this.robotEventsService.getMetrics({
            ip: this.ip,
            startDate: dates.startDate,
            endDate: dates.endDate
          }))
        ).subscribe(metrics => {
          this.metrics = metrics;
        });
      });
    });
  }

}
