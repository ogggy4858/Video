using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebYoutube.Models
{
    public class PeopleDetails
    {
        public string Email { get; set; }

        public string Pass { get; set; }

        public string FullName { get; set; }

        public string Immage { get; set; }

        public DateTime? CreateDatePeople { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string Description { get; set; }

        public string Video { get; set; }

        public int? ViewCount { get; set; }

        public DateTime? CreateDatePost { get; set; }

        public bool? StatusPost { get; set; }
    }
}