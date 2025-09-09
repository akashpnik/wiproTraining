import { Component,Input,Output,EventEmitter } from '@angular/core';
import { Product,Feedback } from '../dashboard/dashboard';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
@Component({
  selector: 'app-product-details',
  imports: [CommonModule, FormsModule],
  templateUrl: './product-details.html',
  styleUrl: './product-details.css'
})
export class ProductDetails {
@Input() product: Product | null = null;
  @Output() feedback = new EventEmitter<Feedback>();

  rating: number = 0;
  comment: string = '';

  onRate(rating: number): void {
    this.rating = rating;
  }

  onSubmitFeedback(): void {
    if (!this.product) return;

    const feedback: Feedback = {
      productId: this.product.id,
      rating: this.rating,
      comment: this.comment
    };

    this.feedback.emit(feedback);

 
    // Reset form
    this.rating = 0;
    this.comment = '';
  }
}
