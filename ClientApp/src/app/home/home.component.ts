import { Component } from "@angular/core";
import Utils from "src/library/Utils";
import { ReservationService } from "src/Services/ReservationService";

@Component({
  selector: "app-home",
  templateUrl: "./home.component.html",
})
export class HomeComponent {
  get reservations(): Reservation[] {
    return this._reservations.map(r => {return {...r, targetDate: new Date(r.targetDate)}});
  }
  private _reservations: Reservation[] = [];
  private reservartionService: ReservationService;

  constructor(reservationService: ReservationService) {
    this.reservartionService = reservationService;
  }

  ngOnInit() {
    this.reservartionService.list().subscribe(
      (r) =>
        this._reservations = r.results
    );
  }

  changeOrder(order) {
    this.reservartionService.list({params: order.value})
    .subscribe(r =>
      {
        this._reservations = r.results
      }
    )
  }

  rate({ id, rate }) {
    const reservation = this._reservations.find((r) => r.id === id);
    const removeRate = rate === reservation.rating;
    const body = removeRate
      ? [
          {
            op: "remove",
            path: "/rating",
          },
        ]
      : [
          {
            op: "replace",
            path: "/rating",
            value: rate,
          },
        ];
    this.reservartionService.partialUpdate(id, body).subscribe(r => {
      reservation.rating = removeRate ? null : rate
    })
  }

  isFavoriteToogler(id: Number) {
    const reservation = this._reservations.find((r) => r.id === id);
    const body = [
      {
        op: "replace",
        path: "/isFavorite",
        value: !reservation.isFavorite,
      },
    ];
    this.reservartionService.partialUpdate(id, body)
    .subscribe(r => {
      reservation.isFavorite = !reservation.isFavorite
    })
  }
}
