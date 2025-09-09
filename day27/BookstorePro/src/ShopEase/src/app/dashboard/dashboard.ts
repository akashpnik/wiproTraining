import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { ProductDetails } from '../product-details/product-details';

export interface Product {
  id: number;
  name: string;
  price: number;
}

export interface Feedback {
  productId: number;
  rating: number;
  comment: string;
}

@Component({
  selector: 'app-dashboard',
  imports: [CommonModule,ProductDetails],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.css'
})
export class Dashboard {
products: Product[] = [
    { id: 1, name: 'Laptop', price: 20000 },
    { id: 2, name: 'Smartphone', price: 10000 },
    { id: 3, name: 'Headphones', price: 499 },
    { id: 4, name: 'Tablet', price: 15000 }
  ];

  selectedProduct: Product | null = null;
  feedbackList: Feedback[] = [];

  onProductSelect(product: Product): void {
    this.selectedProduct = product;
  }

  onFeedbackReceived(feedback: Feedback): void {
    // Update feedback list
    const existingIndex = this.feedbackList.findIndex(f => f.productId === feedback.productId);
    if (existingIndex > -1) {
      this.feedbackList[existingIndex] = feedback;
    } else {
      this.feedbackList.push(feedback);
    }
    

console.log('Feedback received:', feedback);
  }
}