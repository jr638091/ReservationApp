
using System.Collections.Generic;
using ReservationApp.Models;

namespace ReservationApp.Data {
    public interface IContactTypeRepo
    {
        IEnumerable<ContactType> ListContactTypes ();
        ContactType ReadContactType (long id);
        void CreateContactType (ContactType contactType);
        void UpdateContactType (long id, ContactType contactType);
        void PartialUpdateContactType (long id, ContactType contactType);
        bool DeleteContactType(long id);
    }
}