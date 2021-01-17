import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable()
export class LoaderService {
  private loaderShownSubject = new BehaviorSubject<boolean>(false);

  constructor() {
  }

  showLoader() {
    this.loaderShownSubject.next(true);
  }

  hideLoader() {
    this.loaderShownSubject.next(false);
  }

  get isVisible(): Observable<boolean> {
    return this.loaderShownSubject;
  }
}
