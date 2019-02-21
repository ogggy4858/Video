using Data.DTO;
using Data.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DAO
{
    public class HomeDAO
    {
        private DBContext db = null;

        public HomeDAO()
        {
            db = new DBContext();
        }

        #region List News in day

        public List<Data.DTO.ListNewsOfHomeDTO> ListNews()
        {
            List<Data.DTO.ListNewsOfHomeDTO> list = new List<DTO.ListNewsOfHomeDTO>();
            string timeNow = Convert.ToDateTime(DateTime.Now).ToShortDateString();


            list = (from a in db.News
                    select new ListNewsOfHomeDTO
                    {
                        ID = a.ID,
                        Image = a.Image,
                        Title = a.Title,
                        Email = a.Person.Email

                    }).ToList();

            foreach (var item in list)
            {
                item.CommentCount = CommentCountNews(item.ID);
                item.ShortContent = ShortContent(item.ID);
                item.CreateDate = GetDate(item.ID);
                item.Tags = Tags(item.ID);
            }

            var list2 = (from a in list
                         where a.CreateDate == timeNow
                         select a).ToList();

            return list2;

        }

        public List<Data.Framework.News> listNews()
        {
            List<Data.Framework.News> list = new List<News>();
            list = (from a in db.News
                    orderby a.CreateDate descending
                    select a).Take(3).ToList();
            return list;
        }

        public string GetDate(int NewsID)
        {
            string date2 = "";

            var date = db.News.Where(x => x.ID == NewsID).SingleOrDefault();

            date2 = Convert.ToDateTime(date.CreateDate).ToShortDateString();

            return date2;
        }

        public int CommentCountNews(int NewsID)
        {
            var res = (from a in db.CommentNews
                       where a.NewsID == NewsID
                       select a).Count();
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

        #endregion

        #region New in day

        public List<Post> ListPostNew()
        {
            var list = (from a in db.Posts
                        orderby a.CreateDate descending
                        select a).Take(8).ToList();
            return list;
        }

        public List<ListOfferDTO> Offer(int UserID)
        {
            // lay 10 video view count max
            // 3 video view 1 lan
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
                              Poster = a.Poster

                          }).Take(8).ToList();

            foreach (var item in query2)
            {
                item.ViewCount = ViewCount(item.ID);
                item.Email = GetEmailUserByID(item.PeopleID);
            }

            return query2;
        }

        public int ViewCount(int PostID)
        {
            var cout = (from a in db.Viewws
                        where a.PostID == PostID
                        select a.ViewCount).Sum();

            return Convert.ToInt32(cout);
        }

        public string GetEmailUserByID(int? UserID)
        {
            var str = db.People.Where(x => x.ID == UserID).SingleOrDefault();
            return str.Email;
        }

        public List<ListOfferDTO> Offer()
        {
            List<ListOfferDTO> list = new List<ListOfferDTO>();

            var li1 = (from a in db.Posts
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
                           Email = a.Person.Email,
                           Poster = a.Poster

                       }).Take(8).ToList();

            return li1;

        }

        #endregion

        public BannerDTO Banner()
        {
            var res = (from a in db.News
                       where a.DisplayOrder == 1
                       select new BannerDTO
                       {
                           ID = a.ID,
                           Email = a.Person.Email,
                           Tittle = a.Title,
                           ShortContent = a.Content,
                           Image2 = a.Image2

                       }).SingleOrDefault();
            if (res != null)
            {
                char[] ar = res.ShortContent.ToCharArray();
                string str = "";
                if (ar.Length > 178)
                {
                    for (int i = 0; i < 178; i++)
                    {
                        str = str + ar[i];
                    }
                    res.ShortContent = str;
                }

            }
            else
            {
                return new BannerDTO();
            }
            return res;
        }

        #region Content Bottom

        public string[] Tags(int id)
        {
            string Content = db.News.Where(x => x.ID == id).SingleOrDefault().Title;

            string[] ar = Content.Split(' ');

            return ar;
        }

        public List<CommentNew> Review()
        {
            var res = (from a in db.CommentNews orderby a.CreateDate descending select a).Take(10).ToList();
            return res;
        }

        #endregion

        #region Footer

        public List<Post> PostsNew()
        {
            var res = (from a in db.Posts orderby a.CreateDate descending select a).Take(5).ToList();
            return res;
        }

        public List<News> News()
        {
            var res = (from a in db.News orderby a.CreateDate descending select a).Take(3).ToList();
            return res;
        }

        #endregion
    }
}
