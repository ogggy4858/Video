using Data.Framework;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DAO
{
    public class SearchDAO
    {
        private DBContext db = null;

        public SearchDAO()
        {
            db = new DBContext();
        }

        public List<Post> List(string searchString, int page, int pageSize)
        {
            if (string.IsNullOrEmpty(searchString))
            {
                var res = db.Posts.OrderBy(x => x.CreateDate)
                    .Skip((page - 1) * pageSize).Take(pageSize).ToList();
                return res;
            }
            else
            {
                var res = db.Posts.Where(x =>x.Title.Contains(searchString)).OrderBy(x => x.CreateDate)
                    .Skip((page - 1) * pageSize).Take(pageSize).ToList();
                return res;
            }
        }

        public int Count(string searchString)
        {
            if(string.IsNullOrEmpty(searchString))
            {
                var res = db.Posts.Count();
                return res;
            }
            else
            {
                var res = db.Posts.Where(x => x.Title.Contains(searchString)).Count();
                return res;
            }
        }

        public bool AddSearch(Search se)
        {
            try
            {
                db.Searches.Add(se);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public int CheckSearch(int? UserID, string searchString, string ip = null)
        {
            var res = db.Searches.Where(x => x.PeopleID == UserID || x.IP == ip).SingleOrDefault();

            if(res == null)
            {
                return -1;
            }
            else
            {
                if(res.KeyWord == searchString)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
        }

        public bool UpdateCount(int? UserID, Search se , string search, string ip = null)
        {
            se.IP = ip;
            se.PeopleID = UserID;

            try
            {
                int num = CheckSearch(UserID, search, ip);
                if (num == 1)
                {
                    // count ++
                    var res = db.Searches.Where(x => x.PeopleID == UserID || x.IP == ip).SingleOrDefault();
                    res.Count++;
                    db.SaveChanges();
                }
                else if (num == 0)
                {
                    // add search 
                    se.Count = 1;
                    AddSearch(se);

                }
                else if (num == -1)
                {
                    se.Count = 1;
                    AddSearch(se);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }




    }
}
