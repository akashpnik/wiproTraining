import { Component, Input } from '@angular/core';
import { Book } from '../Book.model';

@Component({
  selector: 'app-book',
  imports: [],
  templateUrl: './book.html',
  styleUrl: './book.css'
})
export class BookComponent {
  @Input()
  book!: Book;

}
