/*
import { CommonModule } from '@angular/common';
import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { json } from 'node:stream/consumers';

@Component({
  selector: 'app-root',
  imports: [CommonModule],
  standalone: true,
  template:`
  <button (click)="dosomething()">Do Something</button>
  <div *ngif ="errorMessage" class="error-message">
  {{errorMessage}}
  </div>
  `,
  styles: ['.error-message {color:red;}'],
})
export class App {
  protected readonly title = signal('error_handling');

  errorMessage: string | null = null;

  dosomething()
  {
    try{
      //simulating an error
      const data=JSON.parse('invalid json');
      console.log(data);

    }
    catch(error: any)
    {
      //handle the error
      this.errorMessage = `An error occured: ${error.message}`;
      console.error('Syncheonous error is caught :',error);
    }
  }
}
*/

import { Component, signal } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule],
  template: `
    <h1>{{ title() }}</h1>
    <button (click)="dosomething()">Trigger Error</button>
    <p *ngIf="errorMessage" class="error-message">{{ errorMessage }}</p>
  `,
  styles: [`
    .error-message {
      color: red;
      font-weight: bold;
    }
  `]
})
export class App {
  protected readonly title = signal('error_handling_demo');
  errorMessage: string | null = null;

  dosomething() {
    try {
      // Simulating an error
      const data = JSON.parse('invalid json');
      console.log(data);
    } catch (error: any) {
      this.errorMessage = `An error occured :${error.message}`;
      console.error('Synchronous error is caught:', error);
    }
  }
}