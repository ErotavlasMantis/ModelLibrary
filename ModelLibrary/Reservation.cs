using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLibrary
{
    public class Reservation
    {
        private static int increment = 0;
        public int Id { get; set; }
        public int UserID { get; set; }
        public int BookID { get; set; }
        public DateTime StartDate { get; set; } = DateTime.Now;

        public DateTime EndDate
        { 
            get
        {   
            return
            StartDate.AddDays(30);
        }
            set
            { StartDate = value; }  
        }

        public Reservation()
        {
            Id = increment++;

            StartDate = DateTime.Now;

            EndDate = StartDate.AddDays(30);
        }

        public Reservation(Book book, User user)
        {
            Id = increment++;

            UserID = user.UserID;

            BookID = book.BookID;
        }
    }
}