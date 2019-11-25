using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Entity;
using ConestogaHospital.Models;
using ConestogaHospital.Services;

namespace ConestogaHospital.Services
{
    public class DiagnosisService : IDiagnosisService
    {
        dbContextHMS context = new dbContextHMS();
        public List<Alergy> GetPatientAlergies(int patientId)
        {
            return context.Alergies.Where(x => x.PatientId == patientId).ToList();
        }
        public DiagnosisViewModel GetLastDiagnosisOfPatient(int patientId, int doctorId)
        {
            DiagnosisViewModel model = new DiagnosisViewModel();
            Diagnosis diagnosis = context.Diagnoses.Where(x => x.PatientId == patientId && x.DoctorId == doctorId).OrderByDescending(x => x.DiagnosisDate).Take(1).FirstOrDefault();
            if (diagnosis != null)
            {
                model.DiagnosisDate = diagnosis.DiagnosisDate.ToString("dd/MM/yyyy");
                model.Symptoms = context.Symptoms.Where(x => x.DiagnosisId == diagnosis.DiagnosisId).ToList();
                model.Prescriptions = context.Prescriptions.Where(x => x.DiagnosisId == diagnosis.DiagnosisId).ToList();
            }
            return model;
        }

        public string SaveDiagnosis(DiagnosisViewModel model)
        {
            model.Diagnosis.DiagnosisDate = DateTime.Now;
            context.Diagnoses.Add(model.Diagnosis);
            context.SaveChanges();

            foreach (var symptom in model.Symptoms)
            {
                symptom.DiagnosisId = model.Diagnosis.DiagnosisId;
                context.Symptoms.Add(symptom);
                context.SaveChanges();
            }
            foreach (var prescription in model.Prescriptions)
            {
                prescription.DiagnosisId = model.Diagnosis.DiagnosisId;
                context.Prescriptions.Add(prescription);
                context.SaveChanges();
            }
            if (model.IsAdmit == true)
            {
                model.PatientAdmitDetail.AdmitDate = DateTime.Now;
                context.PatientAdmitDetails.Add(model.PatientAdmitDetail);
                context.SaveChanges();
            }
            return "Added";
        }
        public List<DiagnosisViewModel> GetDiagnosisByPatientNo(string patientNo)
        {
            var list = (from diag in context.Diagnoses
                        join pat in context.Patients on diag.PatientId equals pat.PatientId
                        join doc in context.Doctors on diag.DoctorId equals doc.DoctorId
                        join usr in context.Users on doc.UserId equals usr.UserId
                        join spec in context.Specialites on doc.SpecialityId equals spec.SpecialityId
                        where pat.PatientNo == patientNo
                        select new DiagnosisViewModel()
                        {
                            DiagnosisId = diag.DiagnosisId,
                            PatientName = pat.FName + " " + pat.LName,
                            DoctorName = usr.FName + " " + usr.LName,
                            DiagnosisDateDT = diag.DiagnosisDate,
                            Speciality = spec.SpecialityName
                        }).ToList();

            return list;
        }
        public DiagnosisViewModel GetPreSymByDiagnosisId(int diagnosisId)
        {
            DiagnosisViewModel model = new DiagnosisViewModel();
            
            model.Symptoms = context.Symptoms.Where(x => x.DiagnosisId == diagnosisId).ToList();
            model.Prescriptions = context.Prescriptions.Where(x => x.DiagnosisId == diagnosisId).ToList();

            return model;
        }
    }
}