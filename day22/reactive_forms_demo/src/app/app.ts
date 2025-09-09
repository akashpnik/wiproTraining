import { Component, NgModule, signal } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { RouterOutlet } from '@angular/router';
import { format } from 'node:path';
import { BrowserModule } from '@angular/platform-browser';
import { UserForm } from './user-form/user-form';
/*
@Component({
  selector: 'app-root',
  declarations: [app,user-form],
  imports: [RouterOutlet,ReactiveFormsModule],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected readonly title = signal('reactive_forms_demo');
}
  */

@NgModule ({
  declarations: [UserForm]

  ]
})
