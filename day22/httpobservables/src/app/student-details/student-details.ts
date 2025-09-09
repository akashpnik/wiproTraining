import { Component } from '@angular/core';

@Component({
  selector: 'app-student-details',
  imports: [],
  templateUrl: './student-details.html',
  styleUrl: './student-details.css'
})
export class StudentDetails {
  public StudentMarks =[

  {"id" : 1001, "name" : "Irshad", "marks" : 90},
  {"id" : 1002, "name" : "Imran", "marks" : 80},
  {"id" : 1003, "name" : "Rahul", "marks" : 70},
  {"id" : 1004, "name" : "Ajay", "marks" : 85},
  {"id" : 1005, "name" : "Sunny", "marks" : 60}
];
constructor(){}

ngOnInit(){
    this.studentService.getStudents().subscribe(data => this.student = data);
    
}
}
