import { Directive, ElementRef, HostListener } from '@angular/core';

@Directive({
  selector: '[TitleCase]'
})
export class TitleCaseDirective {

  lastValue!: string;

  constructor(public ref: ElementRef) { }

  @HostListener('input', ['$event']) onInput($event: any) {
    let str = $event.target.value;
    str = str.toLowerCase().split(' ');
    for (var i = 0; i < str.length; i++) {
      str[i] = str[i].charAt(0).toUpperCase() + str[i].slice(1);
    }
    str = str.join(' ');
    $event.preventDefault();
    if (!this.lastValue || (this.lastValue && str.length > 0 && this.lastValue !== str)) {
      this.lastValue = this.ref.nativeElement.value = str;
      // Propagation
      const evt = document.createEvent('HTMLEvents');
      evt.initEvent('input', false, true);
      $event.target.dispatchEvent(evt);
    }
  }
}
