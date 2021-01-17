import { BaseDto } from './base-dto';

export interface RobotEventDto extends BaseDto {
  reporterIp: string;
  reporterHostname?: string;
  eventDateTime: Date;
  eventName: string;
  eventDetails?: string;
}
