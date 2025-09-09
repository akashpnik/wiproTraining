import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  private apiUrl = environment.apiUrl;
  
  private httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json'
    })
  };

  constructor(private http: HttpClient) { }

  // Generic GET method
  get<T>(endpoint: string): Observable<T> {
    return this.http.get<T>(`${this.apiUrl}/${endpoint}`, this.httpOptions);
  }

  // Generic POST method
  post<T>(endpoint: string, data: any): Observable<T> {
    return this.http.post<T>(`${this.apiUrl}/${endpoint}`, data, this.httpOptions);
  }

  // Generic PUT method
  put<T>(endpoint: string, data: any): Observable<T> {
    return this.http.put<T>(`${this.apiUrl}/${endpoint}`, data, this.httpOptions);
  }

  // Generic DELETE method
  delete<T>(endpoint: string): Observable<T> {
    return this.http.delete<T>(`${this.apiUrl}/${endpoint}`, this.httpOptions);
  }

  // Test connection method
  testConnection(): Observable<any> {
    return this.http.get(`${this.apiUrl}/test`);
  }
}
