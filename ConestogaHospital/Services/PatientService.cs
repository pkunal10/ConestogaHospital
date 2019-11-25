using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.Migrations;
using ConestogaHospital.Models;
using ConestogaHospital.Services;

namespace ConestogaHospital.Services
{
    public class PatientService : IPatientService
    {
        dbContextHMS context = new dbContextHMS();
        public string generatePatientNo()
        {
            string patientNo = "";

            List<string> patientNos = context.Patients.Select(x => x.PatientNo).ToList();

            if (patientNos.Count == 0)
            {
                patientNo = "CH-1";
            }
            else
            {
                List<int> patientNoIntPart = new List<int>();
                foreach (var no in patientNos)
                {
                    patientNoIntPart.Add(Convert.ToInt32(no.Split('-')[1]));
                }
                int nextPatientNo = patientNoIntPart.Max() + 1;
                patientNo = "CH-" + nextPatientNo;
            }
            return patientNo;
        }

        public Patient GetPatientByPatientNo(string patientNo)
        {
            return context.Patients.Where(x => x.PatientNo == patientNo).FirstOrDefault();
        }
        public List<string> GetPatientNoForAutoComplete()
        {
            return context.Patients.Select(x => x.PatientNo + "(" + x.FName + " " + x.LName + ")").ToList();
        }
        public string RegisterPatient(PatientViewModel model)
        {
            context.Addresses.Add(model.Address);
            context.SaveChanges();

            model.Patient.AddressId = model.Address.AddressId;
            context.Patients.Add(model.Patient);
            context.SaveChanges();
            foreach (var allergy in model.Alergies)
            {
                allergy.PatientId = model.Patient.PatientId;
                context.Alergies.Add(allergy);
                context.SaveChanges();
            }
            return "added";
        }
        public List<PatientViewModel> GetAdmitPatientsList()
        {
            var list = (from room in context.Rooms
                        join roomDet in context.PatientAdmitDetails on room.RoomId equals roomDet.RoomId
                        join pat in context.Patients on roomDet.PatientId equals pat.PatientId
                        join doc in context.Doctors on roomDet.DoctorId equals doc.DoctorId
                        join spec in context.Specialites on doc.SpecialityId equals spec.SpecialityId
                        join usr in context.Users on doc.UserId equals usr.UserId
                        where roomDet.DischargeDate == null
                        select new PatientViewModel()
                        {
                            AdmitDetailId = roomDet.DetailsId,
                            PatientId = pat.PatientId,
                            RoomNo = room.RoomNo,
                            PatientName = pat.FName + " " + pat.LName,
                            DoctorName = usr.FName + " " + usr.LName + " (" + spec.SpecialityName + ")",
                            AdmitDate = roomDet.AdmitDate
                        }).ToList();
            return list;
        }
        public string UpdateAdmitDetails(PatientAdmitDetail model)
        {
            context.PatientAdmitDetails.AddOrUpdate(model);
            context.SaveChanges();
            return "Updated";
        }
        public PatientAdmitDetail GetAdmitDetailById(int id)
        {
            return context.PatientAdmitDetails.Where(x => x.DetailsId == id).FirstOrDefault();
        }
    }
}