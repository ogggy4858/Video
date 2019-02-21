using Data.DTO;
using Data.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DAO
{
    public class PostDAO
    {
        private DBContext db = null;
        public PostDAO()
        {
            db = new DBContext();
        }

        public List<PostCategory> ListPost()
        {
            return db.PostCategories.ToList();
        }

        public string AddView(Vieww v)
        {
            try
            {
                v.ViewCount = 1;
                db.Viewws.Add(v);
                db.SaveChanges();
                return "";
            }
            catch
            {
                return "false";
            }
        }

        public bool CheckView(int UserID, int PostID)
        {
            var view = db.Viewws.Where(x => x.PeopleID == UserID && x.PostID == PostID).SingleOrDefault();
            if (view == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool CheckView(string IP)
        {
            var view = db.Viewws.Where(x => x.IP == IP).SingleOrDefault();
            if (view == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void UpdateView(int UserID, int PostID)
        {
            try
            {
                var view = db.Viewws.Where(x => x.PeopleID == UserID && x.PostID == PostID).SingleOrDefault();
                if (view == null)
                { }
                else
                {
                    view.ViewCount = view.ViewCount + 1;
                    db.SaveChanges();
                }
            }
            catch
            {

            }
        }

        public void UpdateView(string IP)
        {
            try
            {
                var res = CheckView(IP);
                var view = db.Viewws.Where(x => x.IP == IP).SingleOrDefault();
                if (!res)
                { }
                else
                {
                    view.ViewCount = view.ViewCount + 1;
                    db.SaveChanges();
                }
            }
            catch
            {

            }
        }

        public int ViewCount(int PostID)
        {
            var cout = (from a in db.Viewws
                        where a.PostID == PostID
                        select a.ViewCount).Sum();

            return Convert.ToInt32(cout);
        }

        public List<ListOfferDTO> Offer(int UserID, int PostID)
        {
            // lay 10 video view count max

            List<ListOfferDTO> list = new List<ListOfferDTO>();


            var query2 = (from a in db.Posts
                          join b in db.Viewws on a.ID equals b.PostID
                          where b.PeopleID == UserID
                          orderby b.ViewCount descending
                          select new ListOfferDTO
                          {
                              ID = a.ID,
                              Title = a.Title,
                              Content = a.Content,
                              Description = a.Description,
                              Video = a.Video,
                              CreateDate = a.CreateDate,
                              PeopleID = a.PeopleID,
                              Status = a.Status,
                              Poster = a.Poster,
                              Email = a.Person.Email

                          }).Take(10).ToList();

            foreach (var item in query2)
            {
                if (item.ID == PostID)
                {
                    continue;
                }
                list.Add(item);
            }


            foreach (var item in query2)
            {
                item.ViewCount = ViewCount(item.ID);
            }

            return list;
        }

        public List<ListOfferDTO> Offer()
        {
            List<ListOfferDTO> list = new List<ListOfferDTO>();

            List<string> str = new List<string>();

            str = db.Database.SqlQuery<string>("SELECT DISTINCT Title FROM Post INNER JOIN dbo.Vieww ON Vieww.PostID = Post.ID GROUP BY Title, Vieww.PeopleID").ToList();

            foreach (var item in str)
            {
                var res = (from a in db.Posts
                           where a.Title == item
                           select new ListOfferDTO
                           {
                               ID = a.ID,
                               Title = a.Title,
                               Content = a.Content,
                               Description = a.Description,
                               Video = a.Video,
                               CreateDate = a.CreateDate,
                               PeopleID = a.PeopleID,
                               Status = a.Status,
                               Poster = a.Poster,
                               Email = a.Person.Email
                               
                           }).SingleOrDefault();
                list.Add(res);
            }

            foreach (var item in list)
            {
                item.ViewCount = ViewCount(item.ID);
            }

            return list;

        }

        public string GetEmailUserByID(int? UserID)
        {
            var str = db.People.Where(x => x.ID == UserID).SingleOrDefault();
            return str.Email;
        }

        public Post PostVideo(int id)
        {
            return db.Posts.Find(id);
        }

        public string Add(Post p)
        {
            try
            {
                db.Posts.Add(p);
                db.SaveChanges();
                return "";
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        public Post ViewDetails(int id)
        {
            return db.Posts.Find(id);
        }

        public List<Comment> ListComment(int PostID, int PeopleID)
        {
            List<Comment> cm = new List<Comment>();
            db.Configuration.ProxyCreationEnabled = false;
            cm = db.Comments.Where(x => x.PostID == PostID && x.PeopleID == PeopleID).ToList();
            return cm;
        }

        public List<CommentDTO> LoadComment(int postID, int UserID)
        {
            List<CommentDTO> list = new List<CommentDTO>();
            IQueryable<CommentDTO> query = from cm in db.Comments
                                           join people in db.People
                                           on cm.PeopleID equals people.ID
                                           where cm.PostID == postID
                                           select new CommentDTO()
                                           {
                                               ID = cm.ID,
                                               UserID = people.ID,
                                               Name = people.FullName,
                                               Image = people.Immage,
                                               DateComment = cm.CreateDate.ToString(),
                                               ContentComment = cm.Content,

                                           };
            list = query.ToList();

            foreach (var item in list)
            {
                if (UserID == item.UserID)
                {
                    item.Status = true;
                }
                else
                {
                    item.Status = false;
                }
            }

            return list;
        }

        public List<CommentDTO> LoadComment(int postID)
        {
            List<CommentDTO> list = new List<CommentDTO>();
            IQueryable<CommentDTO> query = from cm in db.Comments
                                           join people in db.People
                                           on cm.PeopleID equals people.ID
                                           where cm.PostID == postID
                                           select new CommentDTO()
                                           {
                                               ID = cm.ID,
                                               UserID = people.ID,
                                               Name = people.FullName,
                                               Image = people.Immage,
                                               DateComment = cm.CreateDate.ToString(),
                                               ContentComment = cm.Content,
                                           };
            list = query.ToList();
            return list;
        }

        public string InsertComment(Comment cm)
        {
            try
            {
                db.Comments.Add(cm);
                db.SaveChanges();
                return "";
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }

        }

        public string GetCommentByID(int id)
        {
            var res = db.Comments.Find(id);
            return res.Content;
        }

        public string EditComment(Comment cm)
        {
            try
            {
                var res = db.Comments.Find(cm.ID);
                res.Content = cm.Content;
                db.SaveChanges();
                return "";
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        public string DeleteComment(int id)
        {
            try
            {
                var res = db.Comments.Find(id);
                db.Comments.Remove(res);
                db.SaveChanges();
                return "";
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        public string InsertLike(Likee like)
        {
            try
            {
                db.Likees.Add(like);
                db.SaveChanges();
                return "";
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        public string InsertDislike(DisLike dlike)
        {
            try
            {
                db.DisLikes.Add(dlike);
                db.SaveChanges();
                return "";
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        public bool CheckLike(int UserID, int PostID)
        {
            var res = ViewDetailsLike(UserID, PostID);
            if (res != null)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public bool CheckDislike(int UserID, int PostID)
        {
            var res = ViewDetailsDislike(UserID, PostID);
            if (res != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Likee ViewDetailsLike(int UserID, int PostID)
        {
            var res = db.Likees.Where(x => x.PeopleID == UserID && x.PostID == PostID).SingleOrDefault();
            return res;
        }

        public string DeleteLike(Likee ee)
        {
            try
            {
                db.Likees.Remove(ee);
                db.SaveChanges();
                return "";
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        public DisLike ViewDetailsDislike(int UserID, int PostID)
        {
            var res = db.DisLikes.Where(x => x.PeopleID == UserID && x.PostID == PostID).SingleOrDefault();
            return res;
        }

        public string DeleteDislike(DisLike ee)
        {
            try
            {
                db.DisLikes.Remove(ee);
                db.SaveChanges();
                return "";
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        public int LikeCount(int PostID)
        {
            return db.Likees.Where(x => x.PostID == PostID).Count();
        }

        public int DislikeCount(int PostID)
        {
            return db.DisLikes.Where(x => x.PostID == PostID).Count();
        }

        public int CommentCount(int PostID)
        {
            return db.Comments.Where(x => x.PostID == PostID).Count();
        }
    }
}
