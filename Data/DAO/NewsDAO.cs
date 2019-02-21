using Data.DTO;
using Data.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DAO
{
    public class NewsDAO
    {
        private DBContext db = null;
        public NewsDAO()
        {
            db = new DBContext();
        }

        #region Insert Update Delete Update ViewCount

        public string InsertNews(News news)
        {
            try
            {
                db.News.Add(news);
                news.ViewCount = 0;
                db.SaveChanges();
                return "";
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        public string UpdateNews(int Id, News news)
        {
            try
            {
                var res = db.News.Find(Id);
                res.Content = news.Content;
                res.DisplayOrder = news.DisplayOrder;
                res.Image = news.Image;
                res.Title = news.Title;
                db.SaveChanges();
                return "";
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        public string DeleteNews(int Id, News news)
        {
            try
            {
                news = db.News.Find(Id);
                db.News.Remove(news);
                db.SaveChanges();
                return "";
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        public News ViewDetails(int ID)
        {
            return db.News.Find(ID);
        }

        public List<ListNewsDTO> ListNews()
        {
            var res = (from a in db.News
                    select new ListNewsDTO
                    {
                        ID = a.ID,
                        Title = a.Title,
                        Image = a.Image,
                        CreateDate = a.CreateDate,
                        DisplayOrder = a.DisplayOrder,
                        PeopleID = a.PeopleID,
                        ViewCount = a.ViewCount,
                        Email = a.Person.Email

                    }).ToList();
            foreach(var item in res)
            {
                item.Content = ShortContent(item.ID);
            }

            return res;
        }

        public List<ListNewsDTO> ListNews2()
        {
            var res = (from a in db.News
                       orderby a.CreateDate
                       select new ListNewsDTO
                       {
                           ID = a.ID,
                           Title = a.Title,
                           Image = a.Image,
                           CreateDate = a.CreateDate,
                           DisplayOrder = a.DisplayOrder,
                           PeopleID = a.PeopleID,
                           ViewCount = a.ViewCount,
                           Email = a.Person.Email

                       }).Take(5).ToList();
            foreach (var item in res)
            {
                item.Content = ShortContent(item.ID);
            }

            return res;
        }

        public string ShortContent(int NewsID)
        {
            var str = db.News.Where(x => x.ID == NewsID).SingleOrDefault();

            var str2 = str.Content;
            var str3 = "";

            char[] ar = str2.ToCharArray();
            if (str2.Length <= 184)
            {
                return str2;
            }
            else
            {
                for (int i = 0; i <= 185; i++)
                {
                    str3 = str3 + ar[i];
                }
                return str3;
            }

        }

        //public List<News> ListNews()
        //{
        //    return (from a in db.News select a).ToList();
        //}

        public List<News> ListNews(string search)
        {
            List<News> list = new List<News>();
            if (string.IsNullOrEmpty(search))
            {
                list = (from a in db.News select a).ToList();
            }
            else
            {
                list = (from a in db.News
                        where a.Title.Contains(search)
                        select a).ToList();
            }
            return list;
        }

        public void UpdateViewCount(int id)
        {
            var res = db.News.Find(id);
            res.ViewCount = res.ViewCount + 1;

            db.SaveChanges();
        }

        public List<News> ListNewsOrder()
        {
            return (db.News.OrderBy(x => x.DisplayOrder).Take(10).ToList());
        }

        public string EditOrder(int ID, News news)
        {
            try
            {
                var res = db.News.Find(ID);
                res.DisplayOrder = news.DisplayOrder;
                res.Image2 = news.Image2;
                db.SaveChanges();
                return "";
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        #endregion

        #region Comment Like Dislike Share

        public bool CheckLike(int UserID, int NewsID)
        {
            var like = db.LikeNews.Where(x => x.PeopleID == UserID && x.NewsID == NewsID).SingleOrDefault();
            if (like == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool CheckDislike(int UserID, int NewsID)
        {
            var like = db.DislikeNews.Where(x => x.PeopleID == UserID && x.NewsID == NewsID).SingleOrDefault();
            if (like == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public string InsertLikeNews(int UserID, int NewsID)
        {
            try
            {
                LikeNew ln = new LikeNew();
                ln.CreateDate = DateTime.Now;
                ln.NewsID = NewsID;
                ln.PeopleID = UserID;
                db.LikeNews.Add(ln);
                db.SaveChanges();
                return "";
            }
            catch (Exception ex)
            { return ex.ToString(); }
        }

        public string InsertDislikeNews(int UserID, int NewsID)
        {
            try
            {
                DislikeNew ln = new DislikeNew();
                ln.CreateDate = DateTime.Now;
                ln.NewsID = NewsID;
                ln.PeopleID = UserID;
                db.DislikeNews.Add(ln);
                db.SaveChanges(); return "";
            }
            catch (Exception ex)
            { return ex.ToString(); }
        }

        public string DeleteLike(int UserID, int NewsID)
        {
            try
            {
                var res = (from a in db.LikeNews
                           where a.PeopleID == UserID && a.NewsID == NewsID
                           select a).SingleOrDefault();

                db.LikeNews.Remove(res);
                db.SaveChanges();
                return "";
            }
            catch (Exception ex)
            { return ex.ToString(); }
        }

        public string DeleteDislike(int UserID, int NewsID)
        {
            try
            {
                var like = db.DislikeNews.Where(x => x.PeopleID == UserID && x.NewsID == NewsID).SingleOrDefault();
                db.DislikeNews.Remove(like);
                db.SaveChanges();
                return "";
            }
            catch (Exception ex)
            { return ex.ToString(); }
        }

        public int CountLikeNews(int NewsID)
        {
            return (from a in db.LikeNews where a.NewsID == NewsID select a).Count();
        }

        public int CountDislikeNews(int NewsID)
        {
            return (from a in db.DislikeNews where a.NewsID == NewsID select a).Count();
        }

        public int CommentNewCount(int newsID)
        {
            return (from a in db.CommentNews where a.NewsID == newsID select a).Count();
        }

        public List<ListCommentNewsDTO> ListComment(int NewsID, int UserID)
        {
            var list = (from a in db.CommentNews
                        where a.NewsID == NewsID
                        select new ListCommentNewsDTO
                        {
                            ID = a.ID,
                            PeopleID = a.PeopleID,
                            NewsID = a.NewsID,
                            Content = a.Content,
                            CreateDate = a.CreateDate.ToString(),
                            Status = a.Status,
                            Email = a.Person.Email,
                            FullName = a.Person.FullName

                        }).ToList();

            foreach (var item in list)
            {
                if (item.PeopleID == UserID)
                {
                    item.Status2 = true;
                }
                else
                {
                    item.Status2 = false;
                }
            }

            return list;
        }

        public string InsertComment(CommentNew cm)
        {
            try
            {
                cm.CreateDate = DateTime.Now;
                db.CommentNews.Add(cm);
                db.SaveChanges();
                return "";
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        public CommentNew ViewDetailsCommentNew(int id)
        {
            return db.CommentNews.Find(id);
        }

        public string EditComment(int id, CommentNew news)
        {
            try
            {
                var res = db.CommentNews.Find(id);
                res.Content = news.Content;
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
                var res = db.CommentNews.Find(id);
                db.CommentNews.Remove(res);
                db.SaveChanges();
                return "";
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        #endregion
    }
}
