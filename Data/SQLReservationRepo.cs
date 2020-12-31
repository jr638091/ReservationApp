using System.Collections.Generic;
using System.Linq;
using ReservationApp.Models;

namespace ReservationApp.Data {
    public class SQLReservationRepo : IReservationAppRepo
    {
        private readonly ReservationContext _context;

        public SQLReservationRepo(ReservationContext context){
            _context = context;
        }

        public bool saveChage()
        {
            return (_context.SaveChanges() > 0);
        }

        

        #region Contact Type Methods

        public void CreateContactTypes (ContactType contactType)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<ContactType> ListContactTypes ()
        {
            return _context.ContactTypes.ToList();
        }

        public void PartialUpdateContactType (int id, ContactType contactType)
        {
            throw new System.NotImplementedException();
        }

        public ContactType ReadContactType(int id)
        {
            return _context.ContactTypes.FirstOrDefault(p => p.Id == id);
        }

        public void UpdateContactType(int id, ContactType contactType)
        {
            throw new System.NotImplementedException();
        }

        public void CreateContactType(ContactType contactType)
        {
            throw new System.NotImplementedException();
        }

        #endregion

        #region Contac Methods
        public IEnumerable<Contact> ListContacts()
        {
            return _context.Contacts.ToList();
        }

        public Contact ReadContact(int id)
        {
            return _context.Contacts.FirstOrDefault(contact => contact.Id == id);
        }

        public void CreateContact(Contact contactType)
        {
            throw new System.NotImplementedException();
        }

        public void UpdateContact(int id, Contact contactType)
        {
            throw new System.NotImplementedException();
        }

        public void PartialUpdateContact(int id, Contact contactType)
        {
            throw new System.NotImplementedException();
        }
        #endregion
    
        #region Reservation Methods
        public IEnumerable<Reservation> ListReservations()
        {
            throw new System.NotImplementedException();
        }

        public Reservation ReadReservation(int id)
        {
            throw new System.NotImplementedException();
        }

        public void CreateReservation(Reservation reservation)
        {
            throw new System.NotImplementedException();
        }

        public void UpdateReservation(int id, Reservation reservation)
        {
            throw new System.NotImplementedException();
        }

        public void PartialUpdateReservation(int id, Reservation reservation)
        {
            throw new System.NotImplementedException();
        }
        #endregion
    }
}