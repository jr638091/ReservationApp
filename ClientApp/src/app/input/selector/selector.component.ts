import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-selector',
  templateUrl: './selector.component.html',
  styleUrls: ['./selector.component.css']
})
export class SelectorInputComponent {
  @Input() options: Array<Object>;
  @Output() updateValue = new EventEmitter<Object>()
  @Input()
  get value() : Object | String | Number { return this.state; }
  set value(value: Object | String | Number) {
    this.state = value;
  }


  state: Object | String | Number = null;

  changeState (value) {
    this.state = value
    this.updateValue.emit(value)
  }
}
