using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Reservations.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Reservations.Controllers
{
    [Route("api/[controller]")]
    public class ReservationsController : Controller
    {
        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        //public Reservation Get(int id)
        public Reservation Get(int id)
        {
            Reservation r = new Reservation();

            try { 
            SqlConnection conn = new SqlConnection("Server=tcp:hpsprout.database.windows.net,1433;Initial Catalog=sprout;Persist Security Info=False;User ID=sprout-admin;Password=Team42db;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            conn.Open();

            SqlDataReader reader = null;

            string SqlQuery = "select * from employee e inner join reservation r on e.id = r.employeeid where e.id = " + id;
             
            SqlCommand cmd = new SqlCommand(SqlQuery, conn);
            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                    r.FirstName = reader.GetString(reader.GetOrdinal("FirstName"));
                    r.LastName = reader.GetString(reader.GetOrdinal("LastName"));
                    r.EmployeeId = reader.GetInt32(reader.GetOrdinal("EmployeeID"));
                    r.Email = reader.GetString(reader.GetOrdinal("Email"));
                    r.Start = (reader.GetDateTime(reader.GetOrdinal("Start"))).ToString();
                    r.Finish = (reader.GetDateTime(reader.GetOrdinal("Finish"))).ToString();
                    r.Comment = reader.GetString(reader.GetOrdinal("Comment"));
                    r.RoomId = reader.GetInt32(reader.GetOrdinal("RoomID"));

                }

            reader.Close();

            conn.Close();
            }
            catch (SqlException ex)
            {
                string s = ex.Message.ToString();
            }



            return r;
        }

       
        // POST api/<controller>
        [HttpPost]
        public ActionResult Post([FromBody]Reservation reservation)
        {
            SqlConnection conn = new SqlConnection("Server=tcp:hpsprout.database.windows.net,1433;Initial Catalog=sprout;Persist Security Info=False;User ID=sprout-admin;Password=Team42db;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            conn.Open();

            SqlCommand cmd = new SqlCommand("spReservation", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@firstname", reservation.FirstName);
            cmd.Parameters.AddWithValue("@lastname", reservation.LastName);
            cmd.Parameters.AddWithValue("@employeeid", reservation.EmployeeId);
            cmd.Parameters.AddWithValue("@email", reservation.Email);
            cmd.Parameters.AddWithValue("@start", Parse(reservation.Start));
            cmd.Parameters.AddWithValue("@finish", Parse(reservation.Finish));
            cmd.Parameters.AddWithValue("@comment", reservation.Comment);
            cmd.Parameters.AddWithValue("@roomid", reservation.RoomId);

            cmd.ExecuteNonQuery();

            conn.Close();

            return Ok();
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        public DateTime Parse(string str)
        {
            
            string[] strs = str.Split(':');

            DateTime dt = new DateTime(2019, 3, 26, Int32.Parse(strs[0]), Int32.Parse(strs[1]), 0);
    


            return dt;
        }
    }
}
