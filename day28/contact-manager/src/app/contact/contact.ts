import { Component } from '@angular/core';

export interface Contact {
  id: number;
  name: string;
  email: string;
  phone: string;
}
@Component({
  selector: 'app-contact',
  imports: [],
  templateUrl: './contact.html',
  styleUrl: './contact.css'
})
export class Contact {

}
