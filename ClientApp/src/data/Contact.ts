interface Contact {
  id: Number
  name: String
  birthDate: Date
  contactType: ContactType
  phoneNumber: String
  reservations: Array<Reservation>
}
