
using System.Collections.Generic;
using ReservationApp.Models;

namespace ReservationApp.Data {
    public interface IContactTypeRepo
    {
        IEnumerable<ContactType> ListContactTypes ();
        ContactType ReadContactType (long id);
        void CreateContactType (ContactType contactType);
        void UpdateContactType (ContactType contactType);
        bool DeleteContactType(long id);
    }
}