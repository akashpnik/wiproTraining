import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { Parentcomponent } from './parentcomponent/parentcomponent';

@Component({
  selector: 'app-root',
  imports: [Parentcomponent],
  template: `<app-parentcomponent></app-parentcomponent>`,
  styleUrl: './app.css'
})
export class App {
  protected readonly title = signal('comp_communication');
}
