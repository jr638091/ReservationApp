import { Component, Input, OnInit } from "@angular/core";
import { Form, FormControl, FormsModule, Validators } from "@angular/forms";
import { ActivatedRoute, Router } from "@angular/router";
import { ContactService } from "src/Services/ContactService";
import { ReservationService } from "src/Services/ReservationService";

@Component({
  selector: "app-reservation-edit",
  templateUrl: "./reservation-edit.component.html",
  styleUrls: ["./reservation-edit.component.css"],
})
export class ReservationEditComponent implements OnInit {
  reservation: Reservation;

  public title: FormControl;
  public targetDate: FormControl;
  public targetTime: FormControl;
  public contactId: FormControl;
  public selectedItem = null;
  private reservationService: ReservationService;
  private contactService: ContactService;
  public contacts: Contact[] = [];
  router: Router;
  get contactsOptions(): { value: Number; label: String }[] {
    return [
      ...this.contacts.map((c) => {
        return {
          value: c.id,
          label: c.name,
        };
      }),
      { value: null, label: "Contact List..." },
    ];
  }

  get valid() {
    return (
      this.title.valid &&
      this.targetDate.valid &&
      this.targetTime.valid &&
      this.contactId.valid
    );
  }

  public showModal = false;

  constructor(
    reservationService: ReservationService,
    contactService: ContactService,
    router: Router,
    private route: ActivatedRoute
  ) {
    this.reservationService = reservationService;
    this.contactService = contactService;
    this.router = router;
  }

  fetched: Boolean = false

  ngOnInit() {
    const id = this.route.snapshot.paramMap.get('id');
    this.reservationService.read(Number(id))
    .subscribe(r=>{
      this.reservation = r
      this.reservation.targetDate = new Date(this.reservation.targetDate)
      this.reservation.targetTime = new Date(this.reservation.targetDate)
      this.reservation.contactId = r.contact.id;
      this.title = new FormControl(this.reservation.title, [Validators.required]);
      this.targetDate = new FormControl(
        null,
        [Validators.required]
      );
      this.targetTime = new FormControl(
        null,
        [Validators.required]
      );
      this.contactId = new FormControl(this.reservation.contact.id, [
        Validators.required,
      ]);
      this.contactService.list().subscribe((r) => {
        this.contacts = r.results;
        this.fetched = true;
      });
    })


  }

  private dateTime = {

  }

  saveDate(date: String | Date) {
    if (typeof(date) === typeof("")) {
      this.dateTime['date'] = date
    }
    else {
      this.dateTime['date'] = this.getDate(date as Date)
    }

    return date
  }

  saveTime(date: String | Date) {
    if (typeof(date) === typeof("")) {
      this.dateTime['time'] = date
    }
    else {
      this.dateTime['time'] = this.getTime(date as Date)
    }
    return date
  }

  updateField(value, variable, form: FormControl) {
    form.setValue(value);
    this.mark(form);
    this.reservation[variable] = value;
  }

  getDate(date: Date) {
    return date.toISOString().split("T")[0];
  }

  getTime(date: Date) {
    return date.toISOString().split("T")[1].slice(0, 5);
  }

  toDate(str: string) {
    return new Date(str)
  }

  toDateFromTime(str: string){
    var date = new Date()
    var _parsed = str.split(":")
    date.setHours(Number(_parsed[0]), Number(_parsed[1]))
    return date
  }



  mark(value: FormControl) {
    value.markAsTouched();
  }

  toogleModal() {
    this.showModal = !this.showModal;
  }

  updateContact(value) {
    this.contactId.markAsTouched();
    if (value === null) {
      this.toogleModal();
      this.selectedItem = null;
    } else {
      this.contactId.setValue(value);
      this.selectedItem = value;
    }
  }

  updateContacts() {
    this.contactService.list().subscribe((r) => {
      this.contacts = r.results;
    });
  }

  onSubmit() {
    if (this.valid) {
      this.reservationService
        .update(this.reservation.id, {
          title: this.title.value,
          contactId: this.contactId.value,
          targetDate: `${this.getDate(this.targetDate.value)}T${this.getTime(this.targetTime.value)}`,
        })
        .subscribe((r) => {
          this.router.navigateByUrl("/");
        });
    }
  }
}
