using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data;
using ConestogaHospital.Models;
using ConestogaHospital.Services;


namespace ConestogaHospital.Services
{
    public class RoomService : IRoomService
    {
        dbContextHMS context = new dbContextHMS();


        public List<RoomsViewModel> getAllRooms()
        {
            
            List<RoomsViewModel> AllRooms = (from rooms in context.Rooms
                                             select new RoomsViewModel()
                                             {
                                                 RoomId = rooms.RoomId,
                                                 RoomNo = rooms.RoomNo,
                                                 FloorNo = rooms.FloorNo,
                                                 IsAvailable=true
                                             }).ToList();

            var occupiedRooms = (from room in context.Rooms
                                 join roomDet in context.PatientAdmitDetails on room.RoomId equals roomDet.RoomId
                                 join pat in context.Patients on roomDet.PatientId equals pat.PatientId
                                 join doc in context.Doctors on roomDet.DoctorId equals doc.DoctorId
                                 join spec in context.Specialites on doc.SpecialityId equals spec.SpecialityId
                                 join usr in context.Users on doc.UserId equals usr.UserId
                                 where roomDet.DischargeDate == null
                                 select new RoomsViewModel()
                                 {
                                     PatientId = pat.PatientId,
                                     RoomId = room.RoomId,
                                     RoomNo = room.RoomNo,
                                     DoctorId = doc.DoctorId,
                                     PatientName = pat.FName + " " + pat.LName,
                                     DoctorName = usr.FName + " " + usr.LName,
                                     DoctorSpeciality = spec.SpecialityName,
                                     IsAvailable = false,
                                     FloorNo = room.FloorNo,
                                     AdmitDate = roomDet.AdmitDate
                                 }).ToList();

            foreach (var room in AllRooms)
            {
                foreach (var ocupiedRoom in occupiedRooms)
                {
                    if (ocupiedRoom.RoomId == room.RoomId)
                    {
                        

                        room.PatientId = ocupiedRoom.PatientId;
                        room.RoomId = ocupiedRoom.RoomId;
                        room.RoomNo = ocupiedRoom.RoomNo;
                        room.DoctorId = ocupiedRoom.DoctorId;
                        room.PatientName = ocupiedRoom.PatientName;
                        room.DoctorName = ocupiedRoom.DoctorName;
                        room.DoctorSpeciality = ocupiedRoom.DoctorSpeciality;
                        room.IsAvailable = false;
                        room.FloorNo = ocupiedRoom.FloorNo;
                        room.AdmitDate = ocupiedRoom.AdmitDate;
                                     
                    }
                }
            }
            return AllRooms;
        }

        public List<RoomsViewModel> SecondFloorRooms()
        {
            var AllRooms = from rooms in context.Rooms select rooms;

            var occupiedRooms = (from room in context.Rooms
                                 join roomDet in context.PatientAdmitDetails on room.RoomId equals roomDet.RoomId
                                 join pat in context.Patients on roomDet.PatientId equals pat.PatientId
                                 join doc in context.Doctors on roomDet.DoctorId equals doc.DoctorId
                                 join spec in context.Specialites on doc.SpecialityId equals spec.SpecialityId
                                 join usr in context.Users on doc.UserId equals usr.UserId
                                 where roomDet.DischargeDate == null && room.FloorNo == 2
                                 select new RoomsViewModel()
                                 {
                                     PatientId = pat.PatientId,
                                     RoomId = room.RoomId,
                                     RoomNo = room.RoomNo,
                                     DoctorId = doc.DoctorId,
                                     PatientName = pat.FName + " " + pat.LName,
                                     DoctorName = usr.FName + " " + usr.LName,
                                     DoctorSpeciality = spec.SpecialityName,
                                     IsAvailable = false
                                 }).ToList();

            foreach (var room in AllRooms)
            {
                foreach (var ocupiedRoom in occupiedRooms)
                {
                    if (ocupiedRoom.RoomId != room.RoomId)
                    {
                        RoomsViewModel roomsViewModel = new RoomsViewModel();
                        roomsViewModel.RoomId = room.RoomId;
                        roomsViewModel.IsAvailable = true;
                        occupiedRooms.Add(roomsViewModel);
                    }
                }
            }

            return occupiedRooms;
        }
    }
}