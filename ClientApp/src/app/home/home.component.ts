import { HttpClient } from '@angular/common/http';
import { Component, Inject } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {
  public reservations: Reservation[];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<Reservation[]>(baseUrl+'reservation')
    .subscribe(result => {
      this.reservations = result.map(e => {
        return {...e,
          targetDate: new Date(e.targetDate)
        }
      }
        );
    }, error => console.log(error));
  }
}
