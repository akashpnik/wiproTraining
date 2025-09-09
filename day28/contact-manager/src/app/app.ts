import { CommonModule } from '@angular/common';
import { Component, OnInit, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterOutlet } from '@angular/router';

import { Contact } from './contact/contact';
import { ContactManagerService } from './contact-manager';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet,CommonModule,FormsModule],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class  App implements OnInit {
  //protected readonly title = signal('contact-manager');
  contacts: Contact[] = [];
  message: string = '';

  // Object to hold data from the "Add" form
  newContact: Omit<Contact, 'id'> = { name: '', email: '', phone: '' };

  // Object to hold data for the "Edit" form
  selectedContact: Contact | null = null;
  
  // Inject the service to make its methods available
  constructor(private contactService: ContactManagerService) {}

  // This runs when the component first loads
   ngOnInit(): void {
    this.loadContacts();
  }

  // Fetches the latest list of contacts from the service
  loadContacts(): void {
    this.contacts = this.contactService.viewContacts();
  }

  // Called when the "Add Contact" form is submitted [cite: 5]
  onAddContact(): void {
    if (!this.newContact.name || !this.newContact.email || !this.newContact.phone) {
      this.message = 'X Please fill all fields to add a contact.';
      return;
    }
    this.message = this.contactService.addContact(this.newContact);
    this.newContact = { name: '', email: '', phone: '' }; // Reset the form
    this.loadContacts();
  }

  // Called when a "Delete" button is clicked [cite: 8]
  onDeleteContact(id: number): void {
    this.message = this.contactService.deleteContact(id);
    this.loadContacts();
    this.selectedContact = null; // Hide edit form if we delete the selected contact
  }
  
  // Sets up the edit form when an "Edit" button is clicked
  selectForEdit(contact: Contact): void {
    this.selectedContact = { ...contact }; // Create a copy to edit
  }

  // Called when the "Update Contact" form is submitted [cite: 7]
  onUpdateContact(): void {
    if (!this.selectedContact) return;

    this.message = this.contactService.modifyContact(this.selectedContact.id, this.selectedContact);
    this.loadContacts();
    this.selectedContact = null; // Hide the edit form
  }

  cancelEdit(): void {
    this.selectedContact = null;
  }
}

