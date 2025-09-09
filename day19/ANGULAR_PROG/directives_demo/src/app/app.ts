 import { CommonModule } from '@angular/common';
import { Component, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterOutlet } from '@angular/router';
@Component({
  selector: 'app-root',
  imports: [CommonModule,FormsModule,RouterOutlet],
  /*
  template: `
    
    <h1>{{ title() }}</h1>
    Hello {{name}}
  
      <div *ngIf ='showmessage'>
        <p>Welcome</p>
      </div>
      
      <table>
      @for (f of data; track f.name) {
         <tr>
          <td>{{ f.name }}</td>
          <td>{{f.age}}</td>
          </tr>
  }
      @empty {
    <tr>
      <td>No data available</td>
    </tr>
  }
</table>
  `,
  styles: [
    "h1 {color : cyan; background-color: black; text-align: center;}"
  ]
})
export class App {
  protected readonly title = signal('directives');
  name: string = "Directives Demo";
  showmessage = signal(true);
  data = [
    {name: 'Akash'},
    {age : 25}
  ]
}
  */ 
 template:
 //
    <div class="status-wrapper">
    @switch (status) {
    @case ('pending') {
    <p>Status: Pending Approval</p>
    }
    @case ('approved') {
    <p>Status: Approved</p>
     }
    @case ('rejected') {
      <p>Status: Rejected</p>
    }
    @default {
      <p>Status: Unknown</p>
    }
  }
<p>Company Name: {{ companyName | uppercase }}</p>  
<p>Purchase Date: {{ purchaseDate | date:'fullDate' }}</p>
<p>Total Amount: {{ totalAmount | currency:'USD':'symbol':'1.2-2' }}</p>
<p>Discount Rate: {{ discountRate | percent:'1.2-2' }}</p>
<p>Note: {{ note | slice:0:30 }}...</p>   
<p>Product ID: {{ productDetails.id }}</p>
<p>Product Name: {{ productDetails.name }}</p>  
<p>Product Specs: {{ productDetails.specs | json }}</p>

})
export class App{
  protected readonly title = signal('directives_demo');
  status = 'approved';

  companyName = 'acme technologies';
  purchaseDate = new Date(2025, 7, 9);
  totalAmount = 12345.678;
  discountRate = 0.15;
  note = 'This is a limited-time offer for premium customers only.';
  productDetails = { id: 101, name: 'Laptop', specs: { ram: '16GB', cpu: 'i7' } };
}