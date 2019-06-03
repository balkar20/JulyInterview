import { Directive, Input, TemplateRef, ViewContainerRef, Renderer2 } from '@angular/core';
 
@Directive({ selector: '[while]' })
export class WhileDirective {
    constructor(private templateRef: TemplateRef<any>, 
                private viewContainer: ViewContainerRef, 
                private renderer: Renderer2) 
    { }
     
    @Input() set while(condition: boolean) {
        let a = this.renderer.createElement("a");
        if (condition) {
          this.viewContainer.createEmbeddedView(this.templateRef);
        //   this.viewContainer.insert(a);
        } else {
          this.viewContainer.clear();
        }
    }
}