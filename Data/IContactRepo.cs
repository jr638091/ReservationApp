using System.Collections.Generic;
using System.Linq;
using ReservationApp.Models;

namespace ReservationApp.Data {
    public interface IContactRepo {
        IQueryable<Contact> ListContacts ();
        Contact ReadContact (long id);
        void CreateContact (Contact contactType);
        void UpdateContact (Contact contactType);
        bool DeleteContact(long id);
    }
}