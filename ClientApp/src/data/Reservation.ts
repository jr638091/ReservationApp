interface Reservation {
  id: Number
  title: String
  creationDate: Date
  targetDate: Date
  targetTime: Date
  rating: Number
  isFavorite: Boolean
  contact: Contact,
  contactId: Number
}
