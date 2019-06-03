import { Component, ViewChild, TemplateRef, ViewContainerRef, ElementRef, AfterViewInit, Renderer2} from '@angular/core';
import {WhileDirective} from "./while.directive";

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
        <ng-template [while]="condition">
                <h1>Hi from template</h1>
            </ng-template>
    </div>
        
        <button (click)="toggle()">Toggle</button>
    `,
  })
  export class AppComponent{
    condition: boolean=false;
    toggle(){
        this.condition=!this.condition;
    }
  }
