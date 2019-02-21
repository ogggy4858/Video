using Data.DTO;
using Data.Framework;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DAO
{
    public class ViewByDateDAO
    {
        private DBContext db = null;

        public ViewByDateDAO()
        {
            db = new DBContext();
        }

        public List<int> ListYear()
        {
            var res = db.Database.SqlQuery<int>("select Distinct Year(CreateDate) from Post order by Year(CreateDate) desc").ToList();

            return res;
        }

        public List<int> ListMonth(int year)
        {
            object[] ob =
            {
                new SqlParameter("@year", year)
            };

            var res = db.Database.SqlQuery<int>("select Distinct Month(CreateDate) from Post where Year(CreateDate) = @year order by Month(CreateDate) desc", ob).ToList();

            return res;
        }

        public List<int> ListDay(int year, int month)
        {
            object[] ob =
            {
                new SqlParameter("@year", year),
                new SqlParameter("@month", month)
            };

            var res = db.Database.SqlQuery<int>("select Distinct Day(CreateDate) from Post where Year(CreateDate) = @year and Month(CreateDate) = @month", ob).ToList();

            return res;
        }

        public List<Post> ListVideo(int year, int month, int day)
        {
            List<Post> list = new List<Post>();

            object[] ob =
            {
                new SqlParameter("@year", year),
                new SqlParameter("@month", month),
                new SqlParameter("@day", day)
            };

            var li = db.Database.SqlQuery<Post>("select * from Post where Year(CreateDate) = @year and Month(CreateDate) = @month and Day(CreateDate) = @day order by CreateDate", ob).ToList();

            list = li;

            return list;
        }

        public List<Post> ListVideoFirst(int year, int month)
        {
            List<Post> list = new List<Post>();

            object[] ob =
            {
                new SqlParameter("@year", year),
                new SqlParameter("@month", month)
            };

            var li = db.Database.SqlQuery<Post>("proc_ListVideo @year, @month", ob).ToList();
        


            list = li;

            return list;
        }
    }
}
