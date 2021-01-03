import { Component, Input } from '@angular/core';
import { utils } from 'protractor';
import Utils from 'src/library/Utils';

@Component({
  selector: 'app-home-mobile',
  templateUrl: './home-mobile.component.html',
  styleUrls: ['./home-mobile.component.css']
})
export class HomeMobileComponent{
  @Input() reservations: Array<Reservation>;

  order : Object = null

  getDateString(reservation: Reservation) {
    var format = {...Utils.dateStyles.long}
    return Utils.dateToString(reservation.targetDate, format)
  }

  updateOrderSelector (value) {
    console.log(value)
    this.order = value
  }
}
