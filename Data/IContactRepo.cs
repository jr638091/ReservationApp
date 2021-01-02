using System.Collections.Generic;
using ReservationApp.Models;

namespace ReservationApp.Data {
    public interface IContactRepo {
        IEnumerable<Contact> ListContacts ();
        Contact ReadContact (long id);
        void CreateContact (Contact contactType);
        void UpdateContact (long id, Contact contactType);
        void PartialUpdateContact (long id, Contact contactType);
        bool DeleteContact(long id);
    }
}