import { Component, EventEmitter, OnInit, Output } from "@angular/core";
import ReservationComparers from "src/library/OrderFunctions";

@Component({
  selector: "app-home-top-banner",
  templateUrl: "./home-top-banner.component.html",
  styleUrls: ["./home-top-banner.component.css"],
})
export class HomeTopBannerComponent implements OnInit {
  orders = ReservationComparers.orders;
  @Output() updateOrder = new EventEmitter<Object>();

  constructor() {}

  ngOnInit() {}

  updateOrderSelector(order) {
    this.updateOrder.emit(order);
  }
}
