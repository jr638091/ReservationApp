using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ReservationApp.Models;


namespace ReservationApp.Data
{
    public class SQLReservationRepo : IReservationAppRepo
    {
        private readonly ReservationContext _context;

        public SQLReservationRepo(ReservationContext context)
        {
            _context = context;
            seedDB();
        }

        public void seedDB()
        {
            if (_context.ContactTypes.Count() == 0 && _context.Contacts.Count() == 0)
            {
                for (long i = 1; i <= 3; ++i)
                {
                    ContactType contactType = new ContactType
                    {
                        Name = string.Format("Contact Type {0}", i),
                    };
                    _context.ContactTypes.Add(contactType);
                }
                _context.SaveChanges();

            }
        }

        public bool saveChange()
        {
            return (_context.SaveChanges() > 0);
        }

        #region Contact Type Methods

        public IEnumerable<ContactType> ListContactTypes()
        {
            return _context.ContactTypes.ToList();
        }

        public ContactType ReadContactType(long id)
        {
            return _context.ContactTypes.Include(c => c.Contacts).FirstOrDefault(p => p.Id == id);
        }

        public void CreateContactType(ContactType contactType)
        {
            if(contactType == null) {
                throw new ArgumentNullException(nameof(contactType));
            }
            _context.ContactTypes.Add(contactType);
        }

        public void PartialUpdateContactType(long id, ContactType contactType)
        {
            throw new System.NotImplementedException();
        }

        public void UpdateContactType(long id, ContactType contactType)
        {
            throw new System.NotImplementedException();
        }

        public bool DeleteContactType(long id)
        {
            ContactType itemToDelete = _context.ContactTypes.Find(id);
            if (itemToDelete != null)
            {
                _context.ContactTypes.Remove(itemToDelete);
                return true;
            }
            return false;
        }
        #endregion

        #region Contac Methods
        public IEnumerable<Contact> ListContacts()
        {
            return _context.Contacts.Include(c => c.ContactType).ToList();
        }

        public Contact ReadContact(long id)
        {
            return _context.Contacts.Include(c => c.ContactType).Include(c => c.Reservations).FirstOrDefault(contact => contact.Id == id);
        }

        public void CreateContact(Contact contact)
        {
            if(contact == null) {
                throw new ArgumentNullException(nameof(contact));
            }
            _context.Contacts.Add(contact);
        }

        public void UpdateContact(long id, Contact contact)
        {
            throw new System.NotImplementedException();
        }

        public void PartialUpdateContact(long id, Contact contact)
        {
            throw new System.NotImplementedException();
        }

        public bool DeleteContact(long id)
        {
            Contact itemToDelete = _context.Contacts.Find(id);
            if (itemToDelete != null)
            {
                _context.Contacts.Remove(itemToDelete);
                return true;
            }
            return false;
        }
        #endregion

        #region Reservation Methods
        public IEnumerable<Reservation> ListReservations()
        {
            return _context.Reservations.ToList();
        }

        public Reservation ReadReservation(long id)
        {
            return _context.Reservations.Include(r => r.Contact.ContactType).FirstOrDefault(reservation => reservation.Id == id);
        }

        public void CreateReservation(Reservation reservation)
        {
            if(reservation == null) {
                throw new ArgumentNullException(nameof(reservation));
            }
            reservation.CreationDate = DateTime.Now;
            _context.Reservations.Add(reservation);
        }

        public void UpdateReservation(long id, Reservation reservation)
        {
            throw new System.NotImplementedException();
        }

        public void PartialUpdateReservation(long id, Reservation reservation)
        {
            throw new System.NotImplementedException();
        }

        public bool DeleteReservation(long id)
        {
            Reservation itemToDelete = _context.Reservations.Find(id);
            if (itemToDelete != null)
            {
                _context.Reservations.Remove(itemToDelete);
                return true;
            }
            return false;
        }
        #endregion

    }
}