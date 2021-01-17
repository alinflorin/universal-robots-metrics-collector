import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { RobotEventsService } from '../services/robot-events.service';
import { DateRangeService } from '../services/date-range.service';
import { switchMap } from 'rxjs/operators';
import { MetricDetailsDto } from '../dto/metric-details-dto';
import { MatPaginator } from '@angular/material/paginator';
import { MetricDetailDto } from '../dto/metric-detail-dto';
import { MatTableDataSource } from '@angular/material/table';
import { ApexChart, ApexFill, ApexYAxis, ApexXAxis } from 'ng-apexcharts';
import * as _ from 'lodash';

@Component({
  selector: 'app-metric-details',
  templateUrl: './metric-details.component.html',
  styleUrls: ['./metric-details.component.scss']
})
export class MetricDetailsComponent implements OnInit {
  metricName: string;
  ip: string;
  details: MetricDetailsDto;
  robotName: string;
  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
  dataSource = new MatTableDataSource<MetricDetailDto>([]);
  displayedColumns = ['date', 'value'];
  series: any;
  chart: ApexChart = {
    type: 'line',
    stacked: false,
    height: 500,
    zoom: {
      type: 'x',
      enabled: true
    },
    toolbar: {
      autoSelected: 'zoom'
    }
  };

  fill: ApexFill = {

  };
  yaxis: ApexYAxis = {
    title: {
      text: 'Nr. evenimente'
    },
    min: 0
  };
  xaxis: ApexXAxis = {
    title: {
      text: 'Data si ora'
    },
    type: 'datetime'
  };

  constructor(private actRoute: ActivatedRoute, private robotEventsService: RobotEventsService, private drService: DateRangeService) { }

  ngOnInit() {
    this.dataSource.paginator = this.paginator;

    this.actRoute.queryParams.subscribe(qp => {
      this.robotName = qp['robotName'];
      this.actRoute.params.subscribe(params => {
        if (!params['name']) {
          throw new Error('Metric name is required');
        }
        this.metricName = params['name'];
        this.ip = params['ip'];

        this.drService.datesChanged.pipe(switchMap(dates => this.robotEventsService.getMetricDetails({
          endDate: dates.endDate,
          ip: this.ip,
          metricName: this.metricName,
          startDate: dates.startDate
        }))).subscribe(details => {
          this.details = details;
          if (this.details != null && this.details.data != null) {
            this.dataSource.data = this.details.data;
            this.reviveDates(this.dataSource.data);
            this.series = _.chain(this.details.data)
              .reverse()
              .groupBy(x => x.value)
              .map(x => {
                let i = 1;
                return {
                  name: x[0].value,
                  data: x.map(y => [y.date.getTime(), i++])
                };
              })
              .value();
          }
        });
      });
    });
  }

  private reviveDates(details: MetricDetailDto[]): void {
    details.forEach(d => {
      d.date = new Date(d.date);
    });
  }
}
