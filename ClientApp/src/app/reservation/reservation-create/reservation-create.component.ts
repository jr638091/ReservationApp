import { Component, OnInit, ViewChild, ViewChildren } from "@angular/core";
import { FormControl, FormGroup, Validators } from "@angular/forms";
import { Router } from "@angular/router";

import { ContactService } from "src/Services/ContactService";
import { ReservationService } from "src/Services/ReservationService";

@Component({
  selector: "app-reservation-create",
  templateUrl: "./reservation-create.component.html",
  styleUrls: ["./reservation-create.component.css"],
})
export class ReservationCreateComponent implements OnInit {
  @ViewChildren("content") content: any;
  public title = new FormControl(null, [Validators.required]);
  public targetDate = new FormControl(null, [Validators.required]);
  public targetTime = new FormControl(null, [Validators.required]);
  public contactId = new FormControl(null, [Validators.required]);
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
    router: Router
  ) {
    this.reservationService = reservationService;
    this.contactService = contactService;
    this.router = router;
  }

  ngOnInit() {
    this.contactService.list().subscribe((r) => {
      this.contacts = r.results;
    });
  }

  onChangeDate(value: Date) {
    this.targetDate.setValue(value.toISOString().split("T")[0]);
  }
  onChangeTime(value: Date) {
    this.targetTime.setValue(
      value.toISOString().split("T")[1].split("Z")[0].slice(0, 5)
    );
  }

  onChangeDateOnPlain(value) {
    this.targetDate.setValue(value.target.value);
  }
  onChangeTimeOnPlain(value) {
    this.targetTime.setValue(value.target.value);
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
    }
    else {
      this.contactId.setValue(value);
      this.selectedItem = value;
    }
  }

  updateContacts () {
    this.contactService.list().subscribe((r) => {
      this.contacts = r.results;
    });
  }

  onSubmit() {
    if (this.valid) {
      this.reservationService
        .create({
          title: this.title.value,
          contactId: this.contactId.value,
          targetDate: `${this.targetDate.value}T${this.targetTime.value}`,
        })
        .subscribe((r) => {
          this.router.navigateByUrl("/");
        });
    }
    console.log();
  }
}
