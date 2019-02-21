using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTO
{
    public class PeopleInfomationDTO
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public string Pass { get; set; }

        public string FullName { get; set; }

        public string Immage { get; set; }

        public DateTime? CreateDatePeople { get; set; }
    }
}
