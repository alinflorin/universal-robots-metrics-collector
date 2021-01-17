import { BaseDto } from './base-dto';
import { MetricDetailDto } from './metric-detail-dto';

export interface MetricDetailsDto extends BaseDto {
  displayName: string;
  data: MetricDetailDto[];
}
