using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class LoginRepository : ILoginRepository
    {
        private MyCmsContext db;
        public LoginRepository(MyCmsContext context)
        {
            db = context;
        }
        public bool IsExistLogin(string username, string password)
        {
            return db.AdminLogins.Any(a => a.UserName == username && a.PassWord == password);
        }
    }
}
