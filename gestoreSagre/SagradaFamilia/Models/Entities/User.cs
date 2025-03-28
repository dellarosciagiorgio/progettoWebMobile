using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entities
{
    public class User
    {
        public int IdUser { get; set; }
        public string Email  { get; set; }
        public string Password { get; set; }
        public Ruolo Ruolo { get; set; }
    }
}
