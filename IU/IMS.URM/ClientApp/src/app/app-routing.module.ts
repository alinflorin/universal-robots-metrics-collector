import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { RobotsListComponent } from './robots-list/robots-list.component';
import { MetricsComponent } from './metrics/metrics.component';
import { MetricDetailsComponent } from './metric-details/metric-details.component';


const routes: Routes = [
  {path: '', pathMatch: 'full', redirectTo: 'robots-list'},
  {path: 'robots-list', component: RobotsListComponent},
  {path: 'metrics', component: MetricsComponent},
  {path: 'metrics/:ip', component: MetricsComponent},
  {path: 'metric-details/:name', component: MetricDetailsComponent},
  {path: 'metric-details/:name/:ip', component: MetricDetailsComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
