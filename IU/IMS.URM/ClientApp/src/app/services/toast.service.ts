import { Injectable } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';

@Injectable()
export class ToastService {
  private durationInSeconds = 5;

  constructor(private snackBar: MatSnackBar) { }

  private show(message: string, className: string) {
    return this.snackBar.open(message, null, {
      duration: this.durationInSeconds * 1000,
      panelClass: className
    });
  }

  info(message: string) {
    return this.show(message, 'info-toast');
  }

  warn(message: string) {
    return this.show(message, 'warn-toast');
  }

  error(message: string) {
    return this.show(message, 'error-toast');
  }
}
