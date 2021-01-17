import { BaseDto } from './base-dto';

export interface MetricDetailDto extends BaseDto {
  date: Date;
  value: any;
}
