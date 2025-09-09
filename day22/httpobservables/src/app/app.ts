import { Component, Pipe, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { StudentDetails } from './student-details/student-details';
import { StuentMarks } from './stuent-marks/stuent-marks';
import { DefaultValueAccessor } from '@angular/forms';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet,StudentDetails,StuentMarks, HttpClient],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected readonly title = signal('httpobservables');

 @Pipe({
  name: 'namePipe'
 })
 class namePipe impliments PipeTransform
 {
  Transform(value: string, DefaultValue: string) : string{
    if (value)
  }
 }

}
