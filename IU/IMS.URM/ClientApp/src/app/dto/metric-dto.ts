import { BaseDto } from './base-dto';

export interface MetricDto extends BaseDto {
  title: string;
  description?: string;
  value: any;
  metricName: string;
}
