import { BrowserModule } from '@angular/platform-browser';
import { NgModule, LOCALE_ID } from '@angular/core';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { SharedModule } from './modules/shared.module';
import { NavigationComponent } from './navigation/navigation.component';
import { FooterComponent } from './footer/footer.component';
import { RobotsListComponent } from './robots-list/robots-list.component';
import {HttpClientModule, HTTP_INTERCEPTORS} from '@angular/common/http';
import { MetricsComponent } from './metrics/metrics.component';
import { DateRangeComponent } from './date-range/date-range.component';
import { registerLocaleData } from '@angular/common';
import localeRo from '@angular/common/locales/ro';
import localeRoExtra from '@angular/common/locales/extra/ro';
import { FormsModule } from '@angular/forms';
import { MetricDetailsComponent } from './metric-details/metric-details.component';
import { InterceptorService } from './services/interceptor.service';
import {NgApexchartsModule} from 'ng-apexcharts';
import { ServiceWorkerModule } from '@angular/service-worker';
import { environment } from '../environments/environment';
import { RobotEventsService } from './services/robot-events.service';

registerLocaleData(localeRo, 'ro', localeRoExtra);

@NgModule({
  declarations: [
    AppComponent,
    NavigationComponent,
    FooterComponent,
    RobotsListComponent,
    MetricsComponent,
    DateRangeComponent,
    MetricDetailsComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    AppRoutingModule,
    HttpClientModule,
    BrowserAnimationsModule,
    NgApexchartsModule,
    SharedModule,
    ServiceWorkerModule.register('ngsw-worker.js', { enabled: environment.production })
  ],
  providers: [
    { provide: LOCALE_ID, useValue: 'ro-RO' },
    { provide: HTTP_INTERCEPTORS, useClass: InterceptorService, multi: true },
    RobotEventsService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
