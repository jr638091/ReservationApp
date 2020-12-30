
using System.Collections.Generic;
using ReservationApp.Models;

namespace ReservationApp.Data {
    public interface IContactTypeRepo
    {
        IEnumerable<ContactType> ListContactTypes ();
        ContactType ReadContactType (int id);
        void CreateContactType (ContactType contactType);
        void UpdateContactType (int id, ContactType contactType);
        void PartialUpdateContactType (int id, ContactType contactType);
    }
}