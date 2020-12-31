
using System.Collections.Generic;
using ReservationApp.Models;

namespace ReservationApp.Data {
    public interface IReservationAppRepo: IContactTypeRepo, IContactRepo, IReservation
    {
        bool saveChage();
    }
}