import { Component, OnInit } from '@angular/core';
import { RobotEventsService } from '../services/robot-events.service';
import { RobotDto } from '../dto/robot-dto';

@Component({
  selector: 'app-robots-list',
  templateUrl: './robots-list.component.html',
  styleUrls: ['./robots-list.component.scss']
})
export class RobotsListComponent implements OnInit {

  allRobots: RobotDto[];

  constructor(private robotEventsService: RobotEventsService) { }

  ngOnInit() {
    this.robotEventsService.getAllRobots().subscribe(x => {
      this.allRobots = x;
    });
  }
}
