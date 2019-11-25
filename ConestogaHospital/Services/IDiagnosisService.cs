using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Entity;
using ConestogaHospital.Models;

namespace ConestogaHospital.Services
{
    public interface IDiagnosisService
    {
        List<Alergy> GetPatientAlergies(int patientId);
        DiagnosisViewModel GetLastDiagnosisOfPatient(int patientId, int doctorId);
        string SaveDiagnosis(DiagnosisViewModel model);
        List<DiagnosisViewModel> GetDiagnosisByPatientNo(string patientNo);
        DiagnosisViewModel GetPreSymByDiagnosisId(int diagnosisId);
    }
}