using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reservations.Models
{
    public class Reservation
    {
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public int EmployeeId { get; set; }
        public String Email { get; set; }
        //public DateTime Start { get; set; }
        //public DateTime Finish { get; set; }
        //public DateTime Comment { get; set; }
        public String Start { get; set; }
        public String Finish { get; set; }
        public String Comment { get; set; }
        public int RoomId { get; set; }
    }
}

