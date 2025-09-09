import { Component, OnInit, OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs';
//import { DataService } from '../services/data.service';
//import { Book } from './book.model';
import { DataService } from '../data-service';
import { Book } from '../Book.model';

@Component({
  selector: 'app-books',
  templateUrl: './books.component.html',
  styleUrls: ['./books.component.css']
})
export class BooksComponent implements OnInit, OnDestroy {
  booksManual: Book[] = [];
  booksAsync$ = this.dataService.getBooks();
  private subscription: Subscription;

  constructor(private dataService: DataService) { }

  ngOnInit() {
    // Manual subscription with cleanup
    this.subscription = this.dataService.getBooks().subscribe({
      next: (books) => this.booksManual = books,
      error: (err) => console.error('Error fetching books:', err)
    });
  }

  ngOnDestroy() {
    // Cleanup subscription
    if (this.subscription) {
      this.subscription.unsubscribe();
    }
  }
}