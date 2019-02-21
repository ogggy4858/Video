using Data.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PagedList; 
namespace Data.DAO
{
    public class CategoryDAO
    {
        private DBContext db = null;
        public CategoryDAO()
        {
            db = new DBContext();
        }
        public IEnumerable<Category> ListAllPading(string search, int page, int pageSize)
        {
            IQueryable<Category> model = db.Categories;
            if(string.IsNullOrEmpty(search))
            {

            }
            else
            {
                model = model.Where(x => x.Name.Contains(search));
            }
            return model.OrderBy(x => x.ID).ToPagedList(page, pageSize);
        }
        public bool Check(string name)
        {
            var cate = db.Categories.Where(x=> x.Name == name).SingleOrDefault();
            if(cate == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public int Create(Category c)
        {
            try
            {
                if (Check(c.Name))
                { return 0; }
                else
                {
                    db.Categories.Add(c);
                    db.SaveChanges();
                    return 1;
                }
                
            }
            catch

            {
                return 0;
            }
        }
        public int Edit(int id, Category c)
        {
            try
            {
                var cate = db.Categories.Find(id);
                cate.DisplayOrder = c.DisplayOrder;
                cate.Name = c.Name;
                cate.ID = c.ID;
                db.SaveChanges();
                return 1;
            }
            catch
            {
                return 0;
            }
        }
        public List<Category> List()
        {
            return db.Database.SqlQuery<Category>("select * from Category").ToList(); 
        }
        public int Delete(int id)
        {
            try
            {
                var cate = db.Categories.Find(id);
                db.Categories.Remove(cate);
                db.SaveChanges();
                return 1;

            }
            catch
            {
                return 0;
            }
        }
        public Category ViewDetails(int id)
        {
            return db.Categories.Find(id);
        }
    }
}
