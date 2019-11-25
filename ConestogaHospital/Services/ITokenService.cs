using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ConestogaHospital.Models;
using ConestogaHospital.Services;

namespace ConestogaHospital.Services
{
    public interface ITokenService
    {
        List<TokenViewModel> GetAllPendingTokensByDoctorId(int doctorId);
        string AddToken(Token model);
        Token GetTokenById(string id);
        string UpdateToken(Token model);
        TokenViewModel GetTokenForDisplayById(string id);
        List<TokenViewModel> getTodaysNewToken();
        List<TokenViewModel> getTodaysUnderDiagnosisToken();
        List<TokenViewModel> getTodaysUnderDiagnosisTokenNotAnnounced();
        string generateTokenNo(int specialityId);
        string DeleteToken(int tokenId);
    }
}