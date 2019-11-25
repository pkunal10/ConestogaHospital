using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ConestogaHospital.Models;

namespace ConestogaHospital.Services
{
    public interface IPatientService
    {
        string generatePatientNo();
        Patient GetPatientByPatientNo(string patientNo);
        List<string> GetPatientNoForAutoComplete();
        string RegisterPatient(PatientViewModel model);
        List<PatientViewModel> GetAdmitPatientsList();
        string UpdateAdmitDetails(PatientAdmitDetail model);
        PatientAdmitDetail GetAdmitDetailById(int id);
    }
}