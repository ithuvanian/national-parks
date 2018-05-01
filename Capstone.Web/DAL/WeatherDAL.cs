using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using Capstone.Web.Models;

namespace Capstone.Web.DAL
{
    public class WeatherDAL
    {
        private string connectionString;
        private string get_forecast = "SELECT * FROM weather WHERE [parkCode] = @parkCode";

        public WeatherDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }


        //pass values into Weather object from database, according to park code
        //this method is only used when set to FARENHEIT
        public Weather GetForecast(string parkCode)
        {
            Weather parkForecast = new Weather();
            int[] lowTemps = new int[5];
            int[] highTemps = new int[5];
            string[] daySummaries = new string[5];
            string[] messages = { "", "", "", "", "" };

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(get_forecast, conn);
                    cmd.Parameters.AddWithValue("@parkCode", parkCode);
                    SqlDataReader reader = cmd.ExecuteReader();
                    int day = 0;
                    while (reader.Read())
                    {
                        lowTemps[day] = Convert.ToInt32(reader["low"]);
                        highTemps[day] = Convert.ToInt32(reader["high"]);
                        daySummaries[day] = Convert.ToString(reader["forecast"]).Replace(" ", "_");
                        day++;
                    }

                    parkForecast.LowTemps = lowTemps;
                    parkForecast.HighTemps = highTemps;
                    parkForecast.DaySummaries = daySummaries;
                    parkForecast.Messages = GetMessages(lowTemps, highTemps, daySummaries);

                    return parkForecast;
                }
                catch (SqlException)
                {
                    throw;
                }

            }
        }

        //same as above but for CELSIUS
        public Weather GetCelsiusForecast(string parkCode)
        {
            try
            {
                Weather parkForecast = new Weather();
                int[] lowTemps = new int[5];
                int[] highTemps = new int[5];
                int[] fLowTemps = new int[5];
                int[] fHighTemps = new int[5];
                string[] daySummaries = new string[5];
                string[] messages = { "", "", "", "", "" };

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(get_forecast, conn);
                    cmd.Parameters.AddWithValue("@parkCode", parkCode);
                    SqlDataReader reader = cmd.ExecuteReader();
                    int day = 0;
                    while (reader.Read())
                    {
                        //F to C conversion
                        fLowTemps[day] = Convert.ToInt32(reader["low"]);
                        fHighTemps[day] = Convert.ToInt32(reader["high"]);
                        double cLow = Math.Round((fLowTemps[day] - 32) / 1.8);
                        double cHigh = Math.Round((fHighTemps[day] - 32) / 1.8);
                        lowTemps[day] = (int)cLow;
                        highTemps[day] = (int)cHigh;
                        daySummaries[day] = Convert.ToString(reader["forecast"]).Replace(" ", "_");
                        day++;
                    }

                    parkForecast.LowTemps = lowTemps;
                    parkForecast.HighTemps = highTemps;
                    parkForecast.DaySummaries = daySummaries;
                    parkForecast.Messages = GetMessages(fLowTemps, fHighTemps, daySummaries);
                    //messages are still based on Farenheit temps

                }
                return parkForecast;
            }
            catch (SqlException)
            {
                throw;
            }

        }

        //string array of messages is passed into Weather object
        private string[] GetMessages(int[] lowTemps, int[] highTemps, string[] daySummaries)
        {
            string[] messages = { "", "", "", "", "" };
            for (int i = 0; i < 5; i++)
            {
                if (lowTemps[i] < 20)
                {
                    messages[i] += "Bundle up, it's going to be cold! ";
                }
                if (highTemps[i] > 75)
                {
                    messages[i] += "Bring extra water, it's going to be hot! ";
                }
                if (highTemps[i] - lowTemps[i] > 20)
                {
                    messages[i] += "Wear extra layers, the temperature is going to vary widely! ";
                }
                if (daySummaries[i] == "snow")
                {
                    messages[i] += "Be sure to bring snowshoes! ";
                }
                if (daySummaries[i] == "rain")
                {
                    messages[i] += "Be sure to bring rain gear and waterproof shoes. ";
                }
                if (daySummaries[i] == "thunderstorms")
                {
                    messages[i] += "Thunderstorms likely. Seek shelter and avoid hiking on exposed ridges. ";
                }
                if (daySummaries[i] == "sun")
                {
                    messages[i] += "Be sure to wear sunblock. ";
                }
            }
            return messages;
        }

    }
}