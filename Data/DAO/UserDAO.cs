using Data.Framework;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DAO
{
    public class UserDAO
    {
        
        private DBContext db = null;

        public UserDAO()
        {
            db = new DBContext();
        }

        public List<Position> ListPosition()
        {
            return (from a in db.Positions select a).ToList();
        }

        public int TotalRecord()
        {
            return db.People.Count();
        }

        public IEnumerable<Person> ListAllPading(string search, int page, int pageSize)
        {
            IQueryable<Person> model = db.People;
            if (string.IsNullOrEmpty(search))
            {

            }
            else
            {
                model = model.Where(x => x.Email.Contains(search) || x.FullName.Contains(search));
            }
            return model.OrderBy(x => x.ID).ToPagedList(page, pageSize);
        }

        public Person ViewDetails(string email)
        {
            return db.People.SingleOrDefault(x => x.Email == email);
        }

        public Person ViewDetails(int id)
        {
            return db.People.Find(id);
        }

        public bool Check(string Email)
        {
            var p = ViewDetails(Email);
            if (p == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public int CheckMailFacebook(string Email)
        {
            var res = db.People.SingleOrDefault(x => x.Email == Email);
            if(res == null)
            {
                return -1;
            }
            else
            {
                if(res.Pass == null)
                {
                    return 0;
                }
                else
                {
                    return 1;
                }
            }
        }

        public int Edit(int id, Person p)
        {
            try
            {
                var temp = ViewDetails(id);
                if (temp == null)
                { return 0; }
                else
                {

                    temp.Email = p.Email;
                    temp.FullName = p.FullName;
                    temp.Pass = p.Pass;
                    temp.Immage = p.Immage;
                    temp.Status = p.Status;
                    temp.PositionID = p.PositionID;
                    db.SaveChanges();
                    return 1;
                }


            }
            catch
            {
                return 0;
            }
        }

        public int Delete(int id)
        {
            try
            {
                var people = ViewDetails(id);
                if (people == null)
                {
                    return 0;
                }
                else
                {
                    db.People.Remove(people);
                    db.SaveChanges();
                    return 1;
                }
            }
            catch
            {
                return 0;
            }
        }

        public int Create(Person p)
        {
            try
            {
                if (Check(p.Email))
                {
                    return -1;
                }
                else
                {
                    db.People.Add(p);
                    db.SaveChanges();
                    return 1;
                }
            }
            catch
            {
                return 0;
            }
        }

        public List<Person> List()
        {
            return db.Database.SqlQuery<Person>("select * from People").ToList();
        }
    }
}
