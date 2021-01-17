import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { GetMetricsQueryDto } from '../dto/get-metrics-query-dto';
import { GetEventsQueryDto } from '../dto/get-events-query-dto';
import { RobotEventDto } from '../dto/robot-event-dto';
import { MetricDto } from '../dto/metric-dto';
import { RobotDto } from '../dto/robot-dto';
import { MetricDetailsDto } from '../dto/metric-details-dto';
import { GetMetricDetailsQueryDto } from '../dto/get-metric-details-query-dto';

@Injectable()
export class RobotEventsService {

  constructor(private http: HttpClient) { }

  getEvents(dto: GetEventsQueryDto) {
    return this.http.post<RobotEventDto[]>('/api/RobotEvents/GetEvents', dto);
  }

  getMetrics(dto: GetMetricsQueryDto) {
    return this.http.post<MetricDto[]>('/api/RobotEvents/GetMetrics', dto);
  }

  getAllRobots() {
    return this.http.get<RobotDto[]>('/api/RobotEvents/GetAllRobots');
  }

  getMetricDetails(dto: GetMetricDetailsQueryDto) {
    return this.http.post<MetricDetailsDto>('/api/RobotEvents/GetMetricDetails', dto);
  }
}
