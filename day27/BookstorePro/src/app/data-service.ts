import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Book } from './Book.model';

@Injectable({
  providedIn: 'root'
})
export class DataService {
  private apiUrl = 'https://my-json-server.typicode.com/your-username/your-repo/books';

  constructor(private http: HttpClient) { }

  getBooks(): Observable<Book[]> {
    return this.http.get<Book[]>(this.apiUrl);
  }
}
