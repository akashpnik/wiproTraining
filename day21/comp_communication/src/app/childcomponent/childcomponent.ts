import { Component, Input, Output,EventEmitter} from '@angular/core';
//import { EventEmitter } from 'stream';

@Component({
  selector: 'app-childcomponent',
  imports: [],
  templateUrl: './childcomponent.html',
  styleUrl: './childcomponent.css'
})
export class Childcomponent  {
  //this is for parent to child communication
  // we willl use @Input decorator to pass data from parent to child component
  @Input() userData:any;

  ngOnInit(){
    if ( !this.userData ){
      console.error('No userdata provided!')
    }
  }
  //this is for child to parent communication
  //we can use EventEmitter to send data from child to parent component
  @Output() notifyParent : EventEmitter<string> = new EventEmitter<string>();
   sendNotification()
  {
    const message = "Hello parent, this is your child";
    this.notifyParent.emit(message);

  }


}
