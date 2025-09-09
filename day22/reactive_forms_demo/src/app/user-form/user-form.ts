import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-user-form',
  imports: [],
  templateUrl: './user-form.html',
  styleUrl: './user-form.css'
})
export class UserForm implements OnInit{
   UserForm:FormGroup;

   cities =['Delhi','Mumbai','Bangalore','Kolkata'];

   ngOnInit(): void {
       this.UserForm = new FormGroup({
        name: new FormControl('',[Validators.required,Validators.minLength])
       })
   }

}
