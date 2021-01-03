
using System.Collections.Generic;
using System.Linq;
using ReservationApp.Models;

namespace ReservationApp.Data {
    public interface IContactTypeRepo
    {
        IQueryable<ContactType> ListContactTypes ();
        ContactType ReadContactType (long id);
        void CreateContactType (ContactType contactType);
        void UpdateContactType (ContactType contactType);
        bool DeleteContactType(long id);
    }
}