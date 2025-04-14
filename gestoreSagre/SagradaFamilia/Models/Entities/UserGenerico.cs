using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DetailedEntities
{
    abstract public class UserGenerico
    {
        public int Id { get; set; }
        public User User { get; set; }
        public int IdUser { get; set; }
    }
}
