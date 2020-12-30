using System.Collections.Generic;
using System.Linq;
using ReservationApp.Models;

namespace ReservationApp.Data {
    public class SQLReservationRepo : IReservationRepo
    {
        private readonly ReservationContext _context;

        public SQLReservationRepo(ReservationContext context){
            _context = context;
        }

        public void CreateContactType(ContactType contactType)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<ContactType> ListContactType()
        {
            return _context.ContactTypes.ToList();
        }

        public void PartialUpdateContactType(int id, ContactType contactType)
        {
            throw new System.NotImplementedException();
        }

        public ContactType ReadContactType(int id)
        {
            return _context.ContactTypes.FirstOrDefault(p => p.Id == id);
        }

        public bool saveChage()
        {
            return (_context.SaveChanges() > 0);
        }

        public void UpdateContactType(int id, ContactType contactType)
        {
            throw new System.NotImplementedException();
        }
    }
}