import { Component, EventEmitter, Input, OnInit, Output } from "@angular/core";
import { FormControl, Validators } from "@angular/forms";
import { da } from "date-fns/locale";
import Utils from "src/library/Utils";
import { ContactService } from "src/Services/ContactService";
import { ContactTypeService } from "src/Services/ContactTypeService";

@Component({
  selector: "app-contact-modal",
  templateUrl: "./contact-modal.component.html",
  styleUrls: ["./contact-modal.component.css"],
})
export class ContactModalComponent implements OnInit {
  public showModal: Boolean;

  @Output() updateShow = new EventEmitter<Boolean>();
  @Output() updateContacs = new EventEmitter();
  @Input()
  get show(): Boolean {
    return this.showModal;
  }
  set show(value: Boolean) {
    this.showModal = value;
  }

  get pages(): number {
    if (this.contacts.hits % this.queryParams.count === 0) {
      return this.contacts.hits / this.queryParams.count;
    }
    return Math.ceil(this.contacts.hits / this.queryParams.count);
  }

  get computedDate(): Date {
    if (this.contact["birthdayDate"] !== null) {
      return new Date(this.contact["birthdayDate"]);
    }
    return new Date();
  }

  public fetched: Boolean = false;

  public contact: Object = {
    name: null,
    birthdayDate: new Date(),
    phoneNumber: null,
    contactType: null,
  };
  public contactTypeName = new FormControl(null);
  public nameForm = new FormControl(null, [Validators.required]);
  public birthdayForm = new FormControl(null, [Validators.required]);
  public contactTypeForm = new FormControl(null, [Validators.required]);
  public phoneNumberForm = new FormControl(null, [
    Validators.minLength(10),
    Validators.maxLength(15),
  ]);

  get ContactTypeList() {
    return this.contactTypes === undefined ? [] : this.contactTypes.results;
  }

  get contactsList() {
    return this.contacts === undefined ? [] : this.contacts.results;
  }

  public contacts: Pagination<Contact>;
  public contactTypes: Pagination<ContactType>;

  contactService: ContactService;
  contactTypeService: ContactTypeService;

  constructor(
    contactService: ContactService,
    contactTypeService: ContactTypeService
  ) {
    this.contactService = contactService;
    this.contactTypeService = contactTypeService;
  }
  private queryParams = { pageIndex: 1, count: 7 };

  get valid() {
    return (
      this.nameForm.valid &&
      this.birthdayForm.valid &&
      this.contactTypeForm.valid &&
      this.phoneNumberForm.valid
    );
  }

  clearContact() {
    this.contact = {
      name: null,
      birthdayDate: null,
      phoneNumber: null,
      contactType: null,
    };
    this.contactTypeForm.markAsUntouched();
    this.nameForm.markAsUntouched();
    this.contactTypeName.markAsUntouched();
    this.phoneNumberForm.markAsUntouched();
    this.birthdayForm.markAsUntouched();
  }

  ngOnInit() {
    this.contactService.list({ params: this.queryParams }).subscribe((c) => {
      this.contacts = c;
      this.contactTypeService.list().subscribe((c) => (this.contactTypes = c));
      this.fetched = true;
    });
  }

  getString(date: string) {
    return Utils.dateToString(new Date(date), Utils.dateStyles.short);
  }

  removeContact($event, id) {
    $event.stopPropagation();
    this.contactService.delete(id).subscribe((_) => {
      this.updateContacs.emit();
      this.queryParams.pageIndex = 1;
      this.fetchContacts();
    });
  }

  toogleModal() {
    if (this.showModal) {
      this.clearContact();
    }

    this.updateShow.emit(!this.showModal);
  }

  updateField(value, variable, form) {
    form.setValue(value);
    this.mark(form);
    this.contact[variable] = value;
  }

  getDate(date: Date) {
    return date.toISOString().split("T")[0];
  }

  createContactType(value) {
    this.contactTypeName.setValue(null);
    this.contactTypeService
      .create({ name: value })
      .subscribe((_) =>
        this.contactTypeService.list().subscribe((r) => (this.contactTypes = r))
      );
  }

  mark(form: FormControl) {
    form.markAsTouched();
  }

  deleteContactType($event, id: Number) {
    $event.stopPropagation();
    this.contactTypeService.delete(id).subscribe((_) => {
      this.contactTypeService.list().subscribe((r) => (this.contactTypes = r));
      this.queryParams.pageIndex = 1;
      this.fetchContacts();
    });
  }

  movePage(mod: number) {
    this.queryParams.pageIndex += mod;
    this.fetchContacts();
  }

  fetchContacts() {
    this.contactService
      .list({ params: this.queryParams })
      .subscribe((r) => (this.contacts = r));
  }

  onSubmit() {
    this.contactService
      .create({
        ...this.contact,
        contactTypeId: this.contact["contactType"].id,
      })
      .subscribe((r) => {
        this.fetchContacts();
        this.updateContacs.emit();
        this.toogleModal();
      });
  }
}
