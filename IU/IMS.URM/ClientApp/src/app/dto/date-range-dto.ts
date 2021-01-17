import { BaseDto } from './base-dto';

export interface DateRangeDto extends BaseDto {
  startDate?: Date;
  endDate?: Date;
}
