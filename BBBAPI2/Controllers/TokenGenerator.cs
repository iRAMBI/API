using BBBAPI2.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Web;

namespace BBBAPI2.Controllers
{
    public class TokenGenerator
    {
        private const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";

        public static string CreateToken(int length)
        {
            
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            return res.ToString();
        }

        public static bool ValidateToken(string token, string userid)
        {
            irambidbEntities db = new irambidbEntities();

            ObjectResult<validateToken_Result> returnedVal; 

            returnedVal = db.validateToken(userid, token);

            validateToken_Result theUser = returnedVal.FirstOrDefault();

            if (theUser == null)
            {
                return false;
            }else{
                return true;
            }
        }
    }
}