/*import { Component, OnInit } from '@angular/core';
import { ApiService } from './services/api.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
  standalone: false  
})
export class AppComponent implements OnInit {
  title = 'ProfileBook Frontend';
   connectionStatus = 'Testing connection...';
  connectionSuccess = false;
  apiData: any = null;

  constructor(private apiService: ApiService) {}

  ngOnInit() {
    this.testBackendConnection();
  }

  testBackendConnection() {
    this.apiService.get('users').subscribe({
      next: (data) => {
        this.connectionStatus = '✅ Backend connected successfully!';
        this.connectionSuccess = true;
        this.apiData = data;
        console.log('✅ Backend response:', data);
      },
      error: (error) => {
        this.connectionStatus = `❌ Backend connection failed: ${error.status} ${error.statusText || 'Connection Error'}`;
        this.connectionSuccess = false;
        console.error('❌ Backend connection error:', error);
        
        // Additional debugging info
        console.log('🔧 Backend URL:', 'http://localhost:5021/api');
        console.log('🔧 Check if ProfileBook.API is running');
        console.log('🔧 Check CORS configuration');
      }
    }); 
  }

  retryConnection() {
    this.connectionStatus = 'Retrying connection...';
    this.testBackendConnection();
  } 
} */


import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
  standalone: false
})
export class AppComponent {
  title = 'ProfileBook Frontend';
  // Remove all connection test logic from here
}
