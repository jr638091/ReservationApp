import { HttpClient } from "@angular/common/http";
import { Inject, Injectable } from "@angular/core";
import { Observable } from "rxjs";

@Injectable({ providedIn: "root" })
export class ReservationService {
  httpClient: HttpClient;
  url: string;

  constructor(http: HttpClient, @Inject("BASE_URL") base_url: string) {
    this.httpClient = http;
    this.url = base_url + "reservation";
  }

  list(options: Object = {}): Observable<Pagination<Reservation>> {
    return this.httpClient.get<Pagination<Reservation>>(this.url, options)
  }

  partialUpdate(id: Number, patch: Object, options: Object = {}) {
    return this.httpClient.patch(this.url + `/${id}`, patch, options)
  }
}
