using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ConestogaHospital.Models;
using ConestogaHospital.Services;
using System.Data.Entity;
using System.Data;
using System.Data.Entity.Migrations;


namespace ConestogaHospital.Services
{
    public class TokenService : ITokenService
    {
        dbContextHMS context = new dbContextHMS();
        public string AddToken(Token model)
        {
            context.Tokens.Add(model);
            context.SaveChanges();
            return "Added";
        }

        public List<TokenViewModel> GetAllPendingTokensByDoctorId(int doctorId)
        {
            DateTime today = DateTime.Now.Date;
            var list = (from token in context.Tokens
                        join status in context.TokenSatuses on token.StatusId equals status.TokenStatusId
                        join pat in context.Patients on token.PatientId equals pat.PatientId
                        where token.DoctorId == doctorId && System.Data.Entity.DbFunctions.TruncateTime(token.GeneratedTime) == today && token.StatusId == 1
                        orderby token.GeneratedTime ascending
                        select new TokenViewModel()
                        {
                            TokenId = token.TokenId,
                            FName = pat.FName,
                            LName = pat.LName,
                            GeneratedTime = token.GeneratedTime,
                            PatientNo = pat.PatientNo,
                            TokenStatus = status.Status,
                            TokenNo = token.TokenNo
                        });

            return list.ToList();
        }

        public Token GetTokenById(string id)
        {
            int tokenId = Convert.ToInt32(id);
            return context.Tokens.Where(x => x.TokenId == tokenId).FirstOrDefault();
        }
        public TokenViewModel GetTokenForDisplayById(string id)
        {
            int tokenId = Convert.ToInt32(id);
            var selectedToken = (from token in context.Tokens
                                 join status in context.TokenSatuses on token.StatusId equals status.TokenStatusId
                                 join doc in context.Doctors on token.DoctorId equals doc.DoctorId
                                 join user in context.Users on doc.UserId equals user.UserId
                                 join pat in context.Patients on token.PatientId equals pat.PatientId
                                 where token.TokenId == tokenId
                                 select new TokenViewModel()
                                 {
                                     TokenId = token.TokenId,
                                     FName = pat.FName,
                                     LName = pat.LName,
                                     GeneratedTime = token.GeneratedTime,
                                     PatientNo = pat.PatientNo,
                                     TokenStatus = status.Status,
                                     DoctorName = user.FName + " " + user.LName,
                                     TokenNo = token.TokenNo,
                                     RoomNo = doc.RoomNo,
                                     PatientId = pat.PatientId

                                 }).FirstOrDefault(); ;

            return selectedToken;
        }

        public string UpdateToken(Token model)
        {
            context.Tokens.AddOrUpdate(model);
            context.SaveChanges();
            return "Updated";
        }

        public List<TokenViewModel> getTodaysNewToken()
        {
            DateTime today = DateTime.Now.Date;
            var list = (from token in context.Tokens
                        join doctor in context.Doctors on token.DoctorId equals doctor.DoctorId
                        join user in context.Users on doctor.UserId equals user.UserId
                        join pat in context.Patients on token.PatientId equals pat.PatientId
                        where System.Data.Entity.DbFunctions.TruncateTime(token.GeneratedTime) == today && token.StatusId == 1
                        orderby token.GeneratedTime ascending
                        select new TokenViewModel()
                        {
                            TokenId = token.TokenId,
                            RoomNo = doctor.RoomNo,
                            DoctorName = user.FName + " " + user.LName,
                            TokenNo = token.TokenNo,
                            PatientName = pat.FName + " " + pat.LName,
                            GeneratedTime = token.GeneratedTime
                        });

            return list.ToList();
        }
        public List<TokenViewModel> getTodaysUnderDiagnosisToken()
        {
            DateTime today = DateTime.Now.Date;
            var list = (from token in context.Tokens
                        join doctor in context.Doctors on token.DoctorId equals doctor.DoctorId
                        join user in context.Users on doctor.UserId equals user.UserId
                        where System.Data.Entity.DbFunctions.TruncateTime(token.GeneratedTime) == today && token.StatusId == 2
                        orderby token.GeneratedTime ascending
                        select new TokenViewModel()
                        {
                            TokenId = token.TokenId,
                            RoomNo = doctor.RoomNo,
                            DoctorName = user.FName + " " + user.LName,
                            TokenNo = token.TokenNo
                        });

            return list.ToList();
        }

        public List<TokenViewModel> getTodaysUnderDiagnosisTokenNotAnnounced()
        {
            DateTime today = DateTime.Now.Date;
            var list = (from token in context.Tokens
                        join doctor in context.Doctors on token.DoctorId equals doctor.DoctorId
                        join user in context.Users on doctor.UserId equals user.UserId
                        where System.Data.Entity.DbFunctions.TruncateTime(token.GeneratedTime) == today && token.StatusId == 2 && token.IsAnnounced == false
                        orderby token.GeneratedTime ascending
                        select new TokenViewModel()
                        {
                            TokenId = token.TokenId,
                            RoomNo = doctor.RoomNo,
                            DoctorName = user.FName + " " + user.LName,
                            TokenNo = token.TokenNo
                        });

            return list.ToList();
        }
        public string generateTokenNo(int specialityId)
        {
            string tokenNo = "";
            string specialityName = context.Specialites.Where(x => x.SpecialityId == specialityId).FirstOrDefault().SpecialityName;
            string prefix = specialityName[0].ToString() + specialityName[2].ToString().ToUpper();
            var doctorList = context.Doctors.Where(x => x.SpecialityId == specialityId).ToList();
            List<string> tokenNos = new List<string>();

            foreach (var doc in doctorList)
            {
                var tokenList = context.Tokens.Where(x => x.DoctorId == doc.DoctorId).ToList();
                foreach (var token in tokenList)
                {
                    tokenNos.Add(token.TokenNo);
                }
            }
            if (tokenNos == null || tokenNos.Count == 0)
            {
                tokenNo = prefix + "-1";
            }
            else
            {
                List<int> tokenNoIntPart = new List<int>();
                foreach (var no in tokenNos)
                {
                    tokenNoIntPart.Add(Convert.ToInt32(no.Split('-')[1]));
                }
                int nextToken = tokenNoIntPart.Max() + 1;
                tokenNo = prefix + "-" + nextToken;
            }
            return tokenNo;
        }
        public string DeleteToken(int tokenId)
        {
            context.Tokens.Remove(context.Tokens.Where(x => x.TokenId == tokenId).FirstOrDefault());
            context.SaveChanges();
            return "deleted";
        }
    }
}