import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {MatButtonModule} from '@angular/material/button';
import {MatListModule} from '@angular/material/list';
import {MatToolbarModule} from '@angular/material/toolbar';
import {MatIconModule} from '@angular/material/icon';
import {MatCardModule} from '@angular/material/card';
import {MatInputModule} from '@angular/material/input';
import {MatSnackBarModule} from '@angular/material/snack-bar';
import {FlexLayoutModule} from '@angular/flex-layout';
import { OwlDateTimeModule, OwlNativeDateTimeModule, OwlDateTimeIntl } from 'ng-pick-datetime';
import { OWL_DATE_TIME_LOCALE } from 'ng-pick-datetime';
import { SetPickerLabels } from '../customization/set-picker-labels';
import {MatTableModule} from '@angular/material/table';
import {MatPaginatorModule} from '@angular/material/paginator';
import {MatTabsModule} from '@angular/material/tabs';
import {MatProgressSpinnerModule} from '@angular/material/progress-spinner';
import { DateRangeService } from '../services/date-range.service';
import { ToastService } from '../services/toast.service';
import { LoaderService } from '../services/loader.service';

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    MatButtonModule,
    MatToolbarModule,
    FlexLayoutModule,
    MatListModule,
    MatIconModule,
    MatCardModule,
    MatInputModule,
    OwlDateTimeModule,
    OwlNativeDateTimeModule,
    MatTableModule,
    MatPaginatorModule,
    MatSnackBarModule,
    MatTabsModule,
    MatProgressSpinnerModule
  ],
  exports: [
    MatButtonModule,
    MatToolbarModule,
    FlexLayoutModule,
    MatListModule,
    MatIconModule,
    MatCardModule,
    MatInputModule,
    OwlDateTimeModule,
    OwlNativeDateTimeModule,
    MatTableModule,
    MatPaginatorModule,
    MatSnackBarModule,
    MatTabsModule,
    MatProgressSpinnerModule
  ],
  providers: [
    {provide: OWL_DATE_TIME_LOCALE, useValue: 'ro'},
    {provide: OwlDateTimeIntl, useClass: SetPickerLabels},
    DateRangeService,
    ToastService,
    LoaderService
  ]
})
export class SharedModule { }
