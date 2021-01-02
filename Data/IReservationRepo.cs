using System.Collections.Generic;
using ReservationApp.Models;

namespace ReservationApp.Data {
    public interface IReservation {
        IEnumerable<Reservation> ListReservations ();
        Reservation ReadReservation (long id);
        void CreateReservation (Reservation reservation);
        void UpdateReservation (long id, Reservation reservation);
        void PartialUpdateReservation (long id, Reservation reservation);
        bool DeleteReservation (long id);
    }
}