import { Component } from "@angular/core";
import ReservationComparers from "src/library/OrderFunctions";
import Utils from "src/library/Utils";
import { ReservationService } from "src/Services/ReservationService";

@Component({
  selector: "app-home",
  templateUrl: "./home.component.html",
  styleUrls: ['./home.component.css']
})
export class HomeComponent {
  get reservations(): Pagination<Reservation> {
    return this._reservations;
  }
  private _reservations: Pagination<Reservation> = {
    hits: 0,
    count: 0,
    pageIndex: 0,
    nextPage: null,
    previousPage: null,
    results: [],
  };
  private pagination = {
    pageIndex: 1,
    count: 10,
  };
  public pages = 0;
  private order = null;
  orders = ReservationComparers.orders
  private reservartionService: ReservationService;

  constructor(reservationService: ReservationService) {
    this.reservartionService = reservationService;
  }

  ngOnInit() {
    this.reservartionService
      .list({ params: { ...this.pagination } })
      .subscribe((r) => {
        this._reservations = r
        if (r.hits % this.pagination.count === 0){
          this.pages = (r.hits / this.pagination.count)
        }
        else {
          this.pages = Math.trunc(r.hits / this.pagination.count) + 1
        }
      });
  }

  movePage(pageToMove: number) {
    this.pagination.pageIndex += pageToMove;
    this.reservartionService.list({params: {...this.pagination, ...this.order}})
      .subscribe((r) => (this._reservations = r));
  }

  changeOrder(order) {
    this.order = order.value;
    this.reservartionService
      .list({ params: { ...order.value, ...this.pagination } })
      .subscribe((r) => {
        this._reservations = r;
      });
  }

  getDateString(reservation) {
    return Utils.dateToString(new Date(reservation.targetDate), Utils.dateStyles.long)
  }

  rate(id, rate) {
    const reservation = this._reservations.results.find((r) => r.id === id);
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
    this.reservartionService.partialUpdate(id, body).subscribe((r) => {
      reservation.rating = removeRate ? null : rate;
    });
  }

  isFavoriteToogler(id: Number) {
    const reservation = this._reservations.results.find((r) => r.id === id);
    const body = [
      {
        op: "replace",
        path: "/isFavorite",
        value: !reservation.isFavorite,
      },
    ];
    this.reservartionService.partialUpdate(id, body).subscribe((r) => {
      reservation.isFavorite = !reservation.isFavorite;
    });
  }
}
