import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-selector',
  templateUrl: './selector.component.html',
  styleUrls: ['./selector.component.css']
})
export class SelectorInputComponent {
  @Input() options: Array<Object>;
  @Output() stateChange = new EventEmitter<Object>()

  state: Object | String | Number = null;

  changeSelection (value) {
    this.state = value
    this.stateChange.emit(value)
  }
}
