import { Component } from '@angular/core';
import { Childcomponent } from '../childcomponent/childcomponent';

@Component({
  selector: 'app-parentcomponent',
  imports: [Childcomponent],
  templateUrl: './parentcomponent.html',
  styleUrl: './parentcomponent.css'
})
export class Parentcomponent {
      //  myInputMessage: string = "I am Parent component"
    user = {
      name: 'Alice',
      age:30,
    };

//this is for @outpput decorator

recieveNotification(message:string){
  console.log('Received from child:',message);
  alert(`Received from child:',${message}`);
}
}
