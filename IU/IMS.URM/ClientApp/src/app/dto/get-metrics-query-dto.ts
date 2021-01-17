import { BaseDto } from './base-dto';

export interface GetMetricsQueryDto extends BaseDto {
  ip?: string;
  startDate?: Date;
  endDate?: Date;
}
