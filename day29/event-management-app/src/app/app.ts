import { CommonModule } from '@angular/common';
import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { EventList } from './components/event-list/event-list';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet,CommonModule,EventList],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected readonly title = signal('event-management-app');
}
