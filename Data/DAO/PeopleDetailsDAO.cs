using Data.DTO;
using Data.Framework;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
namespace Data.DAO
{
    public class PeopleDetailsDAO
    {
        private DBContext db = null;

        public static List<PeopleListVideoDTO> ListVideoStatic = new List<PeopleListVideoDTO>();

        public PeopleDetailsDAO()
        {
            db = new DBContext();
        }

        #region History

        public List<LikeHistoryDTO> ListLikeHistory(int UserID)
        {
            List<LikeHistoryDTO> list = new List<LikeHistoryDTO>();
            var li = (from a in db.Likees
                      where a.PeopleID == UserID
                      select new LikeHistoryDTO
                      {
                          CreateDate = a.CreateDate,
                          TitlePost = a.Post.Title,
                          PostID = a.PostID
                      }).ToList();

            var li2 = (from a in db.LikeNews
                       where a.PeopleID == UserID
                       select new LikeHistoryDTO
                       {
                           CreateDate = a.CreateDate,
                           TitleNews = a.News.Title,
                           NewsID = a.NewsID
                       }).ToList();

            list = li;
            foreach(var item in li2)
            {
                list.Add(item);
            }

            var li3 = (from a in list orderby a.CreateDate descending select a).ToList();

            return li3;
        }

        public List<DislikeHistoryDTO> ListDislikeHistory(int UserID)
        {
            List<DislikeHistoryDTO> list = new List<DislikeHistoryDTO>();
            var li = (from a in db.DisLikes
                      where a.PeopleID == UserID
                      select new DislikeHistoryDTO
                      {
                          CreateDate = a.CreateDate,
                          TitlePost = a.Post.Title,
                          PostID = a.PostID
                      }).ToList();

            var li2 = (from a in db.DislikeNews
                       where a.PeopleID == UserID
                       select new DislikeHistoryDTO
                       {
                           CreateDate = a.CreateDate,
                           TitleNews = a.News.Title,
                           NewsID = a.NewsID
                       }).ToList();

            list = li;
            foreach (var item in li2)
            {
                list.Add(item);
            }

            var li3 = (from a in list orderby a.CreateDate descending select a).ToList();

            return li3;
        }

        public List<CommentHistoryDTO> ListCommentHistory(int UserID)
        {
            List<CommentHistoryDTO> list = new List<CommentHistoryDTO>();

            var res = (
                from a in db.Comments
                where a.PeopleID == UserID
                select new CommentHistoryDTO
                {
                    CreateDate = a.CreateDate,
                    TitlePost = a.Post.Title,
                    ContentComment = a.Content,
                    PostID = a.PostID
                }).ToList();

            var res2 = (
                from a in db.CommentNews
                where a.PeopleID == UserID
                select new CommentHistoryDTO
                {
                    CreateDate = a.CreateDate,
                    ContentComment = a.Content,
                    TitleNews = a.News.Title,
                    NewsID = a.NewsID
                }).ToList();

            list = res;

            foreach (var item in res2)
            {
                list.Add(item);
            }

            var list2 = (from a in list orderby a.CreateDate descending select a).ToList();


            return list2;
        }

        public List<Post> ListPostHistory(int UserID)
        {
            List<Post> list = new List<Post>();
            var res = (from a in db.Posts
                       where a.PeopleID == UserID
                       orderby a.CreateDate descending
                       select a).ToList();
            list = res;
            return list;
        }

        public List<History> ListLoginLogoutHistory(int UserID)
        {
            List<History> list = new List<History>();
            var res = (from a in db.Histories
                       where a.PeopleID == UserID
                       orderby a.CreateDate descending
                       select a).ToList();
            list = res;
            return list;
        }

        public string InsertHistory(History his)
        {
            try
            {
                db.Histories.Add(his);
                db.SaveChanges();
                return "";
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        #endregion

        #region categoryHistory

        public string InsertCategoryHistory(CategoryHistory ch)
        {

            try
            {
                db.CategoryHistories.Add(ch);
                db.SaveChanges();
                return "";
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }

        }

        public string UpdateCategoryHistory(int ID, CategoryHistory ch)
        {

            try
            {
                var res = db.CategoryHistories.Find(ID);
                res.Name = ch.Name;
                db.SaveChanges();
                return "";
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        public string DeleteCategoryHistory(int ID, CategoryHistory ch)
        {
            try
            {
                ch = db.CategoryHistories.Find(ID);
                db.CategoryHistories.Remove(ch);
                db.SaveChanges();
                return "";
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        public CategoryHistory ViewDetailsCategoryHistory(int ID)
        {
            return db.CategoryHistories.Find(ID);
        }

        public List<CategoryHistory> ListCategoryHistory()
        {
            return (from a in db.CategoryHistories select a).ToList();
        }

        #endregion

        public Person ViewDetailsPerson(int id)
        {
            return db.People.Find(id);
        }

        public PeopleInfomationDTO Infomation(string Email)
        {
            var model = from a in db.People
                        where a.Email == Email
                        select new PeopleInfomationDTO()
                        {
                            Id = a.ID,
                            Email = a.Email,
                            Pass = a.Pass,
                            FullName = a.FullName,
                            Immage = a.Immage,
                            CreateDatePeople = a.CreateDate,
                        };

            var ob = model.SingleOrDefault();
            return ob;
        }

        public List<PeopleListVideoDTO> ListVideo(int id, string se)
        {
            if (string.IsNullOrEmpty(se))
            {
                var model = (from post in db.Posts
                             where post.PeopleID == id
                             select new PeopleListVideoDTO()
                             {
                                 ID = post.ID,
                                 Title = post.Title,
                                 Content = post.Content,
                                 Description = post.Description,
                                 Video = post.Video,
                                 CreateDatePost = post.CreateDate,
                                 StatusPost = post.Status
                             }).ToList();

                foreach (var item in model)
                {
                    item.ViewCount = ViewCount(item.ID);
                    item.LikeCount = Like(item.ID);
                    item.DislikeCount = Dislike(item.ID);
                    item.CommentCount = Comment(item.ID);
                }

                var ob = model.OrderBy(x => x.CreateDatePost).ToList();
                return ob;
            }
            else
            {

                var model = (from post in db.Posts
                             where post.PeopleID == id && post.Title.Contains(se)
                             select new PeopleListVideoDTO()
                             {
                                 ID = post.ID,
                                 Title = post.Title,
                                 Content = post.Content,
                                 Description = post.Description,
                                 Video = post.Video,
                                 CreateDatePost = post.CreateDate,
                                 StatusPost = post.Status
                             }).ToList();

                foreach (var item in model)
                {
                    item.ViewCount = ViewCount(item.ID);
                    item.LikeCount = Like(item.ID);
                    item.DislikeCount = Dislike(item.ID);
                    item.CommentCount = Comment(item.ID);
                }

                var ob = model.OrderBy(x => x.CreateDatePost).ToList();
                return ob;

            }

        }

        public List<PeopleListVideoDTO> ListCount()
        {
            foreach (var item in ListVideoStatic)
            {
                item.ViewCount = ViewCount(item.ID);
                item.LikeCount = Like(item.ID);
                item.DislikeCount = Dislike(item.ID);
                item.CommentCount = Comment(item.ID);
            }
            return ListVideoStatic;
        }

        public int? ViewCount(int PostID)
        {
            var query = (from a in db.Viewws
                         where a.PostID == PostID
                         select a.ViewCount).Sum();
            return query;
        }

        public string EditInfo(PeopleInfomationDTO dto)
        {
            try
            {
                var people = db.People.Find(dto.Id);
                people.FullName = dto.FullName;
                people.Immage = dto.Immage;
                db.SaveChanges();
                return "";

            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        public string EditInfo3(PeopleInfomationDTO dto)
        {
            try
            {
                var people = db.People.Find(dto.Id);
                people.Immage = dto.Immage;
                db.SaveChanges();
                return "";

            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        public string EditInfo2(PeopleInfomationDTO dto)
        {
            try
            {
                var people = db.People.Find(dto.Id);
                people.FullName = dto.FullName;
                db.SaveChanges();
                return "";

            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        public string EditPass(int Id, string newpass)
        {
            try
            {
                var people = db.People.Find(Id);
                people.Pass = newpass;
                db.SaveChanges();
                return "";

            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        public Post ViewDetailsPost(int id)
        {
            return db.Posts.Find(id);
        }

        public PeopleInfomationDTO ViewDetails(int Id)
        {
            var model = from a in db.People
                        where a.ID == Id
                        select new PeopleInfomationDTO()
                        {
                            Id = a.ID,
                            Email = a.Email,
                            Pass = a.Pass,
                            FullName = a.FullName,
                            Immage = a.Immage,
                            CreateDatePeople = a.CreateDate,
                        };
            var ob = model.SingleOrDefault();
            return ob;
        }

        public int Like(int PostID)
        {
            return db.Likees.Where(x => x.PostID == PostID).Count();
        }

        public int Dislike(int PostID)
        {
            return db.DisLikes.Where(x => x.PostID == PostID).Count();
        }

        public int Comment(int PostID)
        {
            return db.Comments.Where(x => x.PostID == PostID).Count();
        }

        public bool EditPost(int Id, Post p)
        {
            try
            {
                var res = db.Posts.Find(Id);
                res.Description = p.Description;
                res.Content = p.Content;
                res.Title = p.Title;
                res.PostCategoryID = p.PostCategoryID;
                res.Poster = p.Poster;
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public string DeletePost(long ID)
        {
            try
            {

                var listCM = db.Comments.Where(x => x.PostID == ID).ToList();
                var listLike = db.Likees.Where(x => x.PostID == ID).ToList();
                var listDis = db.DisLikes.Where(x => x.PostID == ID).ToList();

                foreach (var item in listCM)
                {
                    db.Comments.Remove(item);
                }
                foreach (var item in listLike)
                {
                    db.Likees.Remove(item);
                }
                foreach (var item in listDis)
                {
                    db.DisLikes.Remove(item);
                }

                var res = db.Posts.Find(ID);
                db.Posts.Remove(res);
                db.SaveChanges();
                return "";
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }
    }
}
