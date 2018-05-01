using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using Capstone.Web.Models;

namespace Capstone.Web.DAL
{
    public class ParkDAL
    {

        private string connectionString;
        private string get_parks = "SELECT * from park";
        private string get_park_detail = "SELECT * from park WHERE parkCode = @parkCode";

        public ParkDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        //get list of parks from database, pass each into List<Park> as model for index page
        public List<Park> GetParks()
        {
            List<Park> allParks = new List<Park>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(get_parks, conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Park p = new Park();
                        p.ParkCode = Convert.ToString(reader["parkCode"]);
                        p.ImageName = Convert.ToString(reader["parkCode"] + ".jpg");
                        p.ParkName = Convert.ToString(reader["parkName"]);
                        p.State = Convert.ToString(reader["state"]);
                        p.Description = Convert.ToString(reader["parkDescription"]);

                        allParks.Add(p);
                    }
                }
                return allParks;
            }
            catch (SqlException)
            {
                throw;
            }
        }


        //populate Park object by from database, according to park code in table
        public Park GetParkDetail(string parkCode)
        {
            Park p = new Park();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(get_park_detail, conn);
                    cmd.Parameters.AddWithValue("@parkCode", parkCode);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        p.ParkCode = Convert.ToString(reader["parkCode"]);
                        p.ImageName = Convert.ToString(reader["parkCode"] + ".jpg");
                        p.ParkName = Convert.ToString(reader["parkName"]);
                        p.State = Convert.ToString(reader["state"]);
                        p.Acreage = Convert.ToInt32(reader["acreage"]);
                        p.Elevation = Convert.ToInt32(reader["elevationInFeet"]);
                        p.TrailMileage = Convert.ToInt32(reader["milesOfTrail"]);
                        p.NumberOfCampsites = Convert.ToInt32(reader["numberOfCampsites"]);
                        p.Climate = Convert.ToString(reader["climate"]);
                        p.YearFounded = Convert.ToInt32(reader["yearFounded"]);
                        p.AnnualVisitors = Convert.ToInt32(reader["annualVisitorCount"]);
                        p.Quote = Convert.ToString(reader["inspirationalQuote"]);
                        p.QuoteSrc = Convert.ToString(reader["inspirationalQuoteSource"]);
                        p.EntryFee = Convert.ToInt32(reader["entryFee"]);
                        p.AnimalSpecies = Convert.ToInt32(reader["numberOfAnimalSpecies"]);
                        p.Description = Convert.ToString(reader["parkDescription"]);
                    }
                }
                return p;
            }
            catch (SqlException)
            {
                throw;
            }


        }

    }
}