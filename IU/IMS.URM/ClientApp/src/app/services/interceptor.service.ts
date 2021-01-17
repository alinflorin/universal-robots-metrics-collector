import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent, HttpResponse, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, tap, delay } from 'rxjs/operators';
import { ToastService } from './toast.service';
import { LoaderService } from './loader.service';

@Injectable()
export class InterceptorService implements HttpInterceptor {

  constructor(private toast: ToastService, private loaderService: LoaderService) {
  }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    this.loaderService.showLoader();
    return next.handle(req).pipe(
      tap(() => {
        this.loaderService.hideLoader();
      }),
      catchError((error: HttpErrorResponse) => {
        if (error && error.message) {
        this.toast.error(error.message);
        }
        return throwError(error);
      })
    );
  }
}
