import { Component, signal, SimpleChanges } from '@angular/core';
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-root',
  imports: [],
  template:
    "<div><h1>{{data}}</h1><h1>{{subtitle}}</h1></div>",
  styles:
    [
      'div { color: green; }'
    ],
})
/*
export class AppComponent implements OnChanges {
    title = 'onchanges';
    data: string = 'Initial Data';
    ngOnChanges(changes: SimpleChanges): void
     {
        if (changes ['data']) {
            console.log('Previous:', changes['data'].previousValue);
            console.log('Current:', changes['data'].currentValue);
        }
    }
    changeData() {
        this.data = 'Updated Data';
    }
}
*/
export class App {
  protected readonly title = signal('lifecylehooks_demo');

  title1: string;
  subtitle: string="";
  data: string="";

  constructor() {
    this.title1 = "Welcome to Wipro";
  }

  ngOnInit() {
    this.data = "Welcome to Wipro";
    this.subtitle = "from ngOnInit";
    
  }

/*
// The interface defining the shape of our data
interface Student {
  id: number;
  name: string;
  course: string;
  joinDate: Date;
  tuitionFee: number;
  status: 'Active' | 'Inactive' | 'Graduated';
}

@Component({
  selector: 'app-root',
  
  imports: [CommonModule,RouterOutlet],

  // All HTML goes inside the 'template' property using backticks (` `)
  template: `
    <div class="dashboard-container">
      <h2>Student Dashboard</h2>

      <table>
        <thead>
          <tr>
            <th>Name</th>
            <th>Course</th>
            <th>Joining Date</th>
            <th>Tuition Fee</th>
            <th>Status</th>
          </tr>
        </thead>
        <tbody>
          @for (student of students; track student.id) {
            <tr>
              <td>{{ student.name | uppercase }}</td>
              <td>{{ student.course }}</td>
              <td>{{ student.joinDate | date:'longDate' }}</td>
              <td>{{ student.tuitionFee | currency:'USD' }}</td>
              <td>
                <span [ngClass]="{
                  'status-active': student.status === 'Active',
                  'status-inactive': student.status === 'Inactive',
                  'status-graduated': student.status === 'Graduated'
                }">
                  {{ student.status }}
                </span>
              </td>
            </tr>
          } @empty {
            <tr>
              <td colspan="5">No students to display.</td>
            </tr>
          }
        </tbody>
      </table>
    </div>
  `,

  // All CSS goes inside the 'styles' property as an array of strings
  styles: [`
    .dashboard-container {
      font-family: sans-serif;
      padding: 20px;
      background-color: #f9f9f9;
    }
    table {
      width: 100%;
      border-collapse: collapse;
      box-shadow: 0 2px 3px rgba(0,0,0,0.1);
    }
    th, td {
      border: 1px solid #ddd;
      padding: 12px;
      text-align: left;
    }
    th {
      background-color: #4CAF50;
      color: white;
    }
    tr:nth-child(even) {
      background-color: #f2f2f2;
    }
    .status-active { color: #28a745; font-weight: bold; }
    .status-inactive { color: #dc3545; font-weight: bold; }
    .status-graduated { color: #007bff; font-weight: bold; }
  `]
})
export class AppComponent {
  // The component's data property
  students: Student[] = [
    {
      id: 1,
      name: 'Alice Johnson',
      course: 'Computer Science',
      joinDate: new Date('2023-09-01'),
      tuitionFee: 15000,
      status: 'Active'
    },
    {
      id: 2,
      name: 'bob smith',
      course: 'Mechanical Engineering',
      joinDate: new Date('2022-08-15'),
      tuitionFee: 14500,
      status: 'Graduated'
    },
    {
      id: 3,
      name: 'Charlie Brown',
      course: 'Business Administration',
      joinDate: new Date('2024-01-20'),
      tuitionFee: 16000,
      status: 'Active'
    },
    {
      id: 4,
      name: 'Diana Prince',
      course: 'Fine Arts',
      joinDate: new Date('2023-02-10'),
      tuitionFee: 12000,
      status: 'Inactive'
    }
  ];
}
*/