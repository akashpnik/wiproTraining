import { Injectable } from '@angular/core';
import { Contact } from './contact/contact';

@Injectable({
  providedIn: 'root'
})
export class ContactManagerService {
  // A private list to store contacts, with some initial data.
  private contacts: Contact[] = [
    { id: 1, name: 'Akash', email: 'akash@example.com', phone: '9123456789' },
    { id: 2, name: 'Keku', email: 'keku@example.com', phone: '8976543219' }
  ];
  private nextId = 3; // To auto-generate IDs for new contacts

  constructor() { }

  /**
   * Returns the full list of contacts. [cite: 18]
   */
  viewContacts(): Contact[] {
    return this.contacts;
  }

  /**
   * Adds a new contact to the list. [cite: 17]
   */
  addContact(contact: Omit<Contact, 'id'>): string {
    const newContact: Contact = { id: this.nextId++, ...contact };
    this.contacts.push(newContact);
    return ` Contact '${contact.name}' added successfully!`; // Success message [cite: 11]
  }

  /**
   * Modifies an existing contact found by its ID. [cite: 19]
   */
  modifyContact(id: number, updatedContact: Partial<Omit<Contact, 'id'>>): string {
    const contactIndex = this.contacts.findIndex(c => c.id === id);

    if (contactIndex === -1) {
      return ` Error: Contact with ID ${id} not found.`; // Error message [cite: 9, 10]
    }

    // Merge the existing contact with the updated properties
    this.contacts[contactIndex] = { ...this.contacts[contactIndex], ...updatedContact };
    return ` Contact '${this.contacts[contactIndex].name}' updated successfully!`; // Success message [cite: 11]
  }

  /**
   * Deletes a contact by its ID. [cite: 20]
   */
  deleteContact(id: number): string {
    const contactIndex = this.contacts.findIndex(c => c.id === id);

    if (contactIndex === -1) {
      return ` Error: Contact with ID ${id} not found.`; // Error message [cite: 9, 10]
    }

    const contactName = this.contacts[contactIndex].name;
    this.contacts.splice(contactIndex, 1);
    return ` Contact '${contactName}' deleted successfully.`; // Success message [cite: 11]
  }
}