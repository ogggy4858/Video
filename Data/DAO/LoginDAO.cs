using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Framework;
namespace Data.DAO
{
    public class LoginDAO
    {
        private Framework.DBContext db = null;
        public LoginDAO()
        {
            db = new Framework.DBContext();
        }
        public int Login(string username, string pass)
        {
            if (username.Contains("@gmail.com")
                || username.Contains("@facebook.com")
                || username.Contains("@yahoo.com")
                || username.Contains("@gmx.com")
                || username.Contains("@outlook.com")
                || username.Contains("@mail.com")
                || username.Contains("@inbox.com")
                || username.Contains("@yandex.com")
                || username.Contains("@shortmail.com"))
            {
                var p = db.People.SingleOrDefault(x => x.Email == username);
                if (p == null)
                {
                    return -1;
                }
                else
                {
                    if (p.Pass == pass)
                    {
                        return 1;
                    }
                    else
                    {
                        return 0;
                    }
                }
            }
            else
            {
                return -1;
            }
        }
        public bool CheckUser(Framework.Person p)
        {
            var user = db.People.SingleOrDefault(x => x.Email == p.Email);
            if (user == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public int Register(Framework.Person p)
        {
            try
            {
                if (CheckUser(p))
                {
                    return 0;
                }
                else
                {
                    if (p.Email.Contains("@gmail.com")
                        || p.Email.Contains("@facebook.com")
                        || p.Email.Contains("@yahoo.com")
                        || p.Email.Contains("@gmx.com")
                        || p.Email.Contains("@outlook.com")
                        || p.Email.Contains("@mail.com")
                        || p.Email.Contains("@inbox.com")
                        || p.Email.Contains("@yandex.com")
                        || p.Email.Contains("@shortmail.com"))
                    {
                        db.People.Add(p);
                        db.SaveChanges();
                        return 1;
                    }
                    else
                    {
                        return -1;
                    }
              
                }

            }
            catch
            {
                return 0;
            }
        }
        public List<string> ListAdminName()
        {
            List<string> list = new List<string>();
            list = (from a in db.People
                       join b in db.Positions on a.PositionID equals b.ID
                       where b.Name == "Admin" || b.Name == "Người Viết Bài" || b.Name == "Nhà Báo" || b.Name == "Người Kiểm Duyệt"
                       select a.Email).ToList();
            return list;
        }
        public int AddForFacebook(Framework.Person p)
        {
            try
            {
                var res = CheckUser(p);
                if (res == false)
                {
                    db.People.Add(p);
                    db.SaveChanges();
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            catch
            {
                return -1;
            }
        }
        
    }
}
