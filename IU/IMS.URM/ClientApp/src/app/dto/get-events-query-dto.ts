import { GetMetricsQueryDto } from './get-metrics-query-dto';

export interface GetEventsQueryDto extends GetMetricsQueryDto {
  eventName?: string;
}
