using System.Collections.Generic;
using ReservationApp.Models;

namespace ReservationApp.Data {
    public interface IContactRepo {
        IEnumerable<Contact> ListContacts ();
        Contact ReadContact (int id);
        void CreateContact (Contact contactType);
        void UpdateContact (int id, Contact contactType);
        void PartialUpdateContact (int id, Contact contactType);
    }
}