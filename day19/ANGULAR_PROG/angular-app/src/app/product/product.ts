import { Component } from '@angular/core';

@Component({
  selector: 'app-product',
  imports: [],
  templateUrl: './product.html',
  styleUrl: './product.css'
})

export class Product{
  productName : string = 'Sample Product';
  productPrice : number =100;

  // constructor() {
  //   this.productName ='new ample product';
  //   this.productPrice =200;
  // }
}

