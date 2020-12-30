
using System.Collections.Generic;
using ReservationApp.Models;

namespace ReservationApp.Data {
    public interface IReservationRepo: IContactTypeRepo, IContactRepo
    {
        bool saveChage();
    }
}