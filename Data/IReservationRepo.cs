
using System.Collections.Generic;
using ReservationApp.Models;

namespace ReservationApp.Data {
    public interface IReservationRepo
    {
        bool saveChage();
        #region Contact Type Model
        IEnumerable<ContactType> ListContactType ();
        ContactType ReadContactType (int id);
        void CreateContactType (ContactType contactType);
        void UpdateContactType (int id, ContactType contactType);
        void PartialUpdateContactType (int id, ContactType contactType);
        #endregion
        
    }
}