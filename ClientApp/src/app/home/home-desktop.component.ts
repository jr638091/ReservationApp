import { Component, EventEmitter, Input, Output } from '@angular/core';
import ReservationComparers from 'src/library/OrderFunctions';
import Utils from 'src/library/Utils';

@Component({
  selector: 'app-home-desktop',
  templateUrl: './home-desktop.component.html',
  styleUrls: ['./home-desktop.component.css']
})
export class HomeDesktopComponent {
  @Input()
  get reservations () : Array<Reservation> { return this.state }
  set reservations (value: Array<Reservation>) {
    this.state = value
  };
  @Output() updateFavorite = new EventEmitter<Number>();
  @Output() changeOrder = new EventEmitter<Object>();
  @Output() updateRating = new EventEmitter<{id: Number, rate: Number}>();

  state: Array<Reservation>;
  order : Object = null
  orders = ReservationComparers.orders

  getDateString(reservation: Reservation) {
    var format = {...Utils.dateStyles.long}
    return Utils.dateToString(reservation.targetDate, format)
  }

  updateOrderSelector (value: Object) {
    this.order = value
    this.changeOrder.emit(value)
  }

  favoriteUpdate (id: Number) {
    this.updateFavorite.emit(id);
  }

  rateReservation(reservation: Number, rate: Number) {
    this.updateRating.emit({
      id: reservation,
      rate: rate
    })
  }
}
