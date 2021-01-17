import { BaseDto } from './base-dto';

export interface RobotDto extends BaseDto {
  ip: string;
  hostname?: string;
}
