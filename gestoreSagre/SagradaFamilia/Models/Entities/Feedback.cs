using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entities
{
    public class Feedback
    {
        private int _rating;

        public int IdFeedback { get; set; }
        public Acquirente? Acquirente { get; set; } = null;
        public string? Titolo { get; set; }
        public string? Descrizione { get; set; }
        public int Rating
        {
            get => _rating;
            set
            {
                if (value > 0 && value < 11)
                {
                    _rating = value;
                }
            }
        }

        public int IdAcquirente { get; set; }
    }
}
