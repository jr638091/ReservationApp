import { Component, OnInit } from "@angular/core";
import { FormControl, FormGroup, Validators } from "@angular/forms";
import { ReservationService } from "src/Services/ReservationService";

@Component({
  selector: "app-reservation-create",
  templateUrl: "./reservation-create.component.html",
  styleUrls: ["./reservation-create.component.css"],
})
export class ReservationCreateComponent implements OnInit {
  public reservation: Reservation = {
    id: null,
    title: null,
    creationDate: null,
    targetDate: null,
    rating: null,
    isFavorite: null,
    contact: null,
    contactId: null,
  };
  public reservationForm: FormGroup;
  private reservationService: ReservationService;

  constructor(reservationService: ReservationService) {
    this.reservationService = reservationService;
  }

  ngOnInit() {
    this.reservationForm = new FormGroup({
      title: new FormControl(this.reservation.title, [Validators.required]),
      targetDate: new FormControl(this.reservation.targetDate, [
        Validators.required,
      ]),
      contactId: new FormControl(this.reservation.contactId, [
        Validators.required,
      ]),
    });
  }

  onSubmit() {
    this.reservationService.create(this.reservationForm.value).subscribe();
    console.log(this.reservationForm.value);
  }
}
