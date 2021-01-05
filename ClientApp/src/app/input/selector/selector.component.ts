import { Component, EventEmitter, Input, Output } from "@angular/core";

@Component({
  selector: "app-selector",
  templateUrl: "./selector.component.html",
  styleUrls: ["./selector.component.css"],
})
export class SelectorInputComponent {
  @Input() options: Array<Object>;
  @Input() placeHolder: string;
  @Output() updateValue = new EventEmitter<Object>();
  @Input()
  get value(): Object | String | Number {
    return this.state;
  }
  set value(value: Object | String | Number) {
    console.log(value);

    this.state = value;
  }

  get computedLabel(): String {
    if (this.options !== undefined) {
      return this.options.find((e) => e["value"] === this.state)["label"];
    }
    return "";
  }

  state: Object | String | Number = null;

  changeState(value) {
    console.log(value);
    this.state = value;
    this.updateValue.emit(value);
  }
}
