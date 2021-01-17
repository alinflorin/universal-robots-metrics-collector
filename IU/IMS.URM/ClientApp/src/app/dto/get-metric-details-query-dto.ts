import { GetMetricsQueryDto } from './get-metrics-query-dto';

export interface GetMetricDetailsQueryDto extends GetMetricsQueryDto {
  metricName: string;
}
