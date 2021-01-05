import { Component, EventEmitter, Input, OnInit, Output } from "@angular/core";

@Component({
  selector: "app-list-group-item",
  templateUrl: "./list-group-item.component.html",
  styleUrls: ["./list-group-item.component.css"],
})
export class ListGroupItemComponent implements OnInit {
  @Input() index: number;
  @Output() click = new EventEmitter();

  constructor() {}

  ngOnInit() {}
}
