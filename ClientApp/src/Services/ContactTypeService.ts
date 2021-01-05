import { HttpClient } from "@angular/common/http";
import { Inject, Injectable } from "@angular/core";
import { Observable } from "rxjs";

@Injectable({ providedIn: "root" })
export class ContactTypeService {
  httpClient: HttpClient;
  url: string;

  constructor(http: HttpClient, @Inject("BASE_URL") base_url: string) {
    this.httpClient = http;
    this.url = base_url + "contact-type";
  }

  list(options: Object = {}): Observable<Pagination<ContactType>> {
    return this.httpClient.get<Pagination<ContactType>>(this.url, options)
  }

  partialUpdate(id: Number, patch: Object, options: Object = {}) {
    return this.httpClient.patch(this.url + `/${id}`, patch, options)
  }

  create(contactType: Object): Observable<ContactType> {
    return this.httpClient.post<ContactType>(this.url, contactType)
  }

  delete(id: Number): Observable<Object> {
    return this.httpClient.delete(`${this.url}/+${id}`)
  }
}
