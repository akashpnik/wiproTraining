// import { Component, signal } from '@angular/core';
// import { RouterOutlet } from '@angular/router';

// @Component({
//   selector: 'app-root',
//   //imports: [RouterOutlet],
//   imports: [RouterOutlet],
//   templateUrl: './app.html',
//   styleUrl: './app.css'
// })
// export class App {
//   protected readonly title = signal('templetedemo');
// 1. Import 'signal' and 'RouterOutlet'
import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-root',
  // standalone components are now common
  imports: [RouterOutlet], // 2. Cleaned up redundant line
  templateUrl: './app.html', // 3. Using conventional file name
  styleUrl: './app.css'     // 3. Using conventional file name
})
// 3. Using conventional class name
export class App{
  // 4. Fixed typo in the string
  protected readonly title = signal('templatedemo');

   name: string = "akash";
   age:number =25;


  // for property binding
 title1 = "Property Binding Demo";
 classtype = "text-danger";
// company = "Wipro Technologies";
// image = "image 
// "https://media.geeksforgeeks.org/wp-content/uploads/geeksforgeeks-6.png";
   
  btnheight =100;
  btnwidth=100;

  addProduct() {
    console.log('Add Product button was clicked!');
    // Add your logic here, e.g., add a product to an array
  }
  firstname:string="kumar";
}

// @Component({
//   selector: 'app-root',
//   template:
//    `<h1>({{title()}})</h1>clr
//     <p>Welome the Angular Templete Demo !</p>`,
//   styles:
//     `h1{color:blue; }`,
// })
// export class App {
//   protected readonly title = signal('templetedemo');

//   constructor(){

//   }
//   name:string = 'Angular Templete Demo';
// }
