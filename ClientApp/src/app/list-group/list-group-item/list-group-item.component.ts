import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-list-group-item',
  templateUrl: './list-group-item.component.html',
  styleUrls: ['./list-group-item.component.css']
})
export class ListGroupItemComponent implements OnInit {
  @Input() index: Number

  constructor() { }

  ngOnInit() {
  }

}
