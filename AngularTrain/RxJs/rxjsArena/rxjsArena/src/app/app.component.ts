import { Component } from '@angular/core';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'rxjsArena';

  source = Observable.create((observer) => {
    let count = 0;
    console.log('Observable created');

    const timer = setInterval(() => {
      observer.next(count);
      count++;
    }, 1000);

    return () => {
      console.log('Observable destroyed');
      clearInterval(timer);
    }
  });
}
