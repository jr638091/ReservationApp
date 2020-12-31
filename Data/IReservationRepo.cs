using System.Collections.Generic;
using ReservationApp.Models;

namespace ReservationApp.Data {
    public interface IReservation {
        IEnumerable<Reservation> ListReservations ();
        Reservation ReadReservation (int id);
        void CreateReservation (Reservation reservation);
        void UpdateReservation (int id, Reservation reservation);
        void PartialUpdateReservation (int id, Reservation reservation);
    }
}