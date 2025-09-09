import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Istudent } from './student1';

@Injectable({
  providedIn: 'root'
})

export class StudentService
{
  private url: string = 'src/assets/students.json';

  constructor(private http: HttpClient) { }

  getStudents(): Observable<Istudent[]> {
    return this.http.get<Istudent[]>(this.url);
  }
}