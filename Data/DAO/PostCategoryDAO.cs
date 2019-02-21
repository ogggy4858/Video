using Data.Framework;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DAO
{
    public class PostCategoryDAO
    {
        private DBContext db = null;

        public PostCategoryDAO()
        {
            db = new DBContext();
        }

        public bool Add(PostCategory postCate)
        {
            try
            {
                db.PostCategories.Add(postCate);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Edit(int ID, PostCategory postCate)
        {
            try
            {
                var res = db.PostCategories.Find(ID);
                res.Name = postCate.Name;
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<PostCategory> List()
        {
            return db.PostCategories.ToList();
        }

        public PostCategory ViewDetails(int ID)
        {
            return db.PostCategories.Find(ID);
        }

        public bool Delete(int ID, PostCategory p)
        {
            try
            {
                p = ViewDetails(ID);
                db.PostCategories.Remove(p);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<int> ListNamePostCategory()
        {
            var res = db.Database.SqlQuery<int>("select ID from PostCategory").ToList();

            return res;
        }

        public List<List<Post>> ListPost()
        {
            List<List<Post>> res = new List<List<Post>>();

            foreach (var item in ListNamePostCategory())
            {
                var listPost = db.Posts.Where(x => x.PostCategoryID == item).OrderByDescending(x => x.CreateDate).Take(4).ToList();
                res.Add(listPost);
            }

            return res;

        }

        public List<Post> ListPostDetails(int id, int page, int pageSize, string search)
        {
            List<Post> list = new List<Post>();
            if (string.IsNullOrEmpty(search))
            {
                list = db.Posts.Where(x => x.PostCategoryID == id)
                         .OrderByDescending(x => x.CreateDate)
                         .Skip((page - 1) * pageSize)
                        .Take(pageSize)
                        .ToList();

            }
            else
            {
                list = db.Posts.Where(x => x.PostCategoryID == id && x.Title.Contains(search))
                            .OrderByDescending(x => x.CreateDate)
                   .Skip((page - 1) * pageSize)
                        .Take(pageSize)

                    .ToList();
            }
            return list;
        }

        public string GetName(int id)
        {
            return (from a in db.PostCategories where a.ID == id select a.Name).SingleOrDefault();
        }

        public int TotalRecord(int id, string search)
        {
            int list = 0;
            if (string.IsNullOrEmpty(search))
            {
                list = db.Posts.Where(x => x.PostCategoryID == id)
                          .Count();
            }
            else
            {
                list = db.Posts.Where(x => x.PostCategoryID == id && x.Title.Contains(search)) 
                     .Count();
            }

            return list;

        }

    }
}
