using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ConestogaHospital.Models;


namespace ConestogaHospital.Services
{
    public interface IRoomService
    {
        List<RoomsViewModel> getAllRooms();
    }
}