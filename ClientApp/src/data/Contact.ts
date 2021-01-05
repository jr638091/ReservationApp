interface Contact {
  id: Number
  name: String
  birthdayDate: Date
  contactType: ContactType
  phoneNumber: String
  reservations: Array<Reservation>
}
