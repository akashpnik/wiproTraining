import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'discount'
})
export class DiscountPipe implements PipeTransform {

  transform(price: number, percentage: number = 10): number {
    return price - (price * percentage / 100);
  }

}
