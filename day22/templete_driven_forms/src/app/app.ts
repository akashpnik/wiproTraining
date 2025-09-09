import { CommonModule } from '@angular/common';
import { Component, signal } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-root',
  imports: [FormsModule,CommonModule],
  templateUrl: './app.html',
  styleUrl: './app.css',
   standalone: true,
})
export class App {
  protected readonly title = signal('templete_driven_forms');

  onSubmit(form: NgForm){
    console.log('FormSubmit', form.value);

  }
}
