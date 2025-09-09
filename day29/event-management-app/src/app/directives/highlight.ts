import { Directive, ElementRef, Input, OnInit } from '@angular/core';

@Directive({
  selector: '[appHighlight]'
})
export class Highlight implements OnInit  {
  // Use @Input() to allow passing the price into our directive from the template
  @Input('appHighlight') price: number = 0;

  // ElementRef gives us direct access to the host DOM element
  constructor(private el: ElementRef) {}

  //constructor() { }
  ngOnInit(): void {
    // This logic runs when the component initializes
    if (this.price > 2000) {
      this.el.nativeElement.style.backgroundColor = 'lightgoldenrodyellow';
    }
  }
}
