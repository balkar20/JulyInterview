import { Component, ViewChild, TemplateRef, ViewContainerRef, ElementRef, AfterViewInit, Renderer2 } from '@angular/core';
import { WhileDirective } from "./while.directive";

// @Component({
//     selector: 'vcr',
//     template: `
//     <ng-templatte ></ng-templatte>
//     `,
//   })
//   export class VcrComponent implements AfterViewInit{
//     constructor() {
//     }

//     ngAfterViewInit() {
//         const buttonElement = this._renderer.createElement('button');
//     this._vcr.createEmbeddedView(buttonElement);
//     //   this._vcr.createEmbeddedView(this.tpl);
//     }
//   }

@Component({
  selector: 'my-app',
  template: `
    <div>
        <ul>
          <li *ngFor="let item of list; trackBy:identify">{{item.id}} and {{item.name}}</li>
        </ul>
    </div>
        
        <button (click)="updateCollection()">ToggleUpdate</button>
        <button (click)="changeCollection()">ToggleChange</button>
    `,
})
export class AppComponent {
  list: any[];

  constructor() {
    this.list = [{ id: 1, name: 'Gustavo' }, { id: 2, name: 'Costa' }]
  }

  identify(index: number, item: any) {
    //do what ever logic you need to come up with the unique identifier of your item in loop, I will just return the object id.
    return item.id;
  }

  updateCollection() {
    this.list = [{ id: 1, name: 'Gustavo' }, { id: 2, name: 'Costa' }]
  }

  changeCollection() {
    this.list = [{ id: 1, name: 'Gustavo' }, { id: 2, name: 'Costo' }]
  }
}
