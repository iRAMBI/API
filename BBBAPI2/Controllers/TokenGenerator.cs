using BBBAPI2.Models;
using System;
using System.Collections.Generic;
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

        public static bool ValidateToken(string token)
        {
            irambidbEntities db = new irambidbEntities();

            var result = from user in db.Users
                         where user.token == token
                         select user;

            if (result.FirstOrDefault() == null)
            {
                return false;
            }else{
                return true;
            }
        }
    }
}