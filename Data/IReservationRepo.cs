using System.Collections.Generic;
using System.Linq;
using ReservationApp.Models;

namespace ReservationApp.Data {
    public interface IReservation {
        IQueryable<Reservation> ListReservations ();
        Reservation ReadReservation (long id);
        void CreateReservation (Reservation reservation);
        void UpdateReservation (Reservation reservation);
        bool DeleteReservation (long id);
    }
}