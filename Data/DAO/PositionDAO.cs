using Data.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DAO
{
    public class PositionDAO
    {
        private DBContext db = null;

        public PositionDAO()
        {
            db = new DBContext();
        }

        public void Add(Position p)
        {
            try
            {
                db.Positions.Add(p);
                db.SaveChanges();
            }
            catch
            {

            }
        }

        public List<Position> List()
        {
            return db.Positions.ToList();
        }

        public void Edit(int id, Position p)
        {
            try
            {
                var res = db.Positions.Find(id);
                res.Name = p.Name;
                db.SaveChanges();
            }
            catch
            {

            }
        }

        public Position ViewDetails(int id)
        {
            return db.Positions.Find(id);
        }

        public void Delete(int id, Position p)
        {
            try
            {
                p = db.Positions.Find(id);
                db.Positions.Remove(p);
                db.SaveChanges();
            }
            catch
            {

            }
        }
    }
}
