using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Capstone.Web.Models;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace Capstone.Web.DAL
{
    public class SurveyDAL
    {
        private string connectionString = @"Data Source=localhost\sqlexpress;Initial Catalog = ParkWeather; Integrated Security = True";
        private const string SQL_GetParkCode = "SELECT park.parkCode FROM park WHERE parkName = @ParkName";
        private const string SQL_InsertSurvey = "INSERT INTO survey_result (parkCode, emailAddress, state, activityLevel) VALUES (@ParkCode, @EmailAddress, @State, @ActivityLevel)";
        private const string SQL_GetSurveyResults = "SELECT survey_result.parkCode, park.parkName, COUNT(survey_result.parkCode) AS num_of_surveys FROM survey_result " +
            "JOIN park ON park.parkCode = survey_result.parkCode GROUP BY survey_result.parkCode, park.parkName ORDER BY num_of_surveys DESC";
        private const string SQL_GetParkNames = "SELECT parkName FROM park";


        public SurveyDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        //get a list of all the surveys in the database
        public List<SurveyResult> GetSurveysResults()
        {
            List<SurveyResult> results = new List<SurveyResult>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(SQL_GetSurveyResults, conn);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        SurveyResult s = new SurveyResult();
                        s.NumOfSurveys = Convert.ToInt32(reader["num_of_surveys"]);
                        s.ParkCode = Convert.ToString(reader["parkCode"]);
                        s.ParkName = Convert.ToString(reader["parkName"]);

                        results.Add(s);
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }

            return results;

        }

        //get park code for survey insert
        public string GetParkCodeForSurvey(string parkName)
        {
            string code = "";
            if (parkName == null)
            {
                return code;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(SQL_GetParkCode, conn);
                    cmd.Parameters.AddWithValue("@ParkName", parkName);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {

                        code = Convert.ToString(reader["parkCode"]);


                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }

            return code;

        }

        //get list of park names for dropdown list
        public List<SelectListItem> ParkNameDropdown()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "", Value = "" });
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(SQL_GetParkNames, conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        SelectListItem item = new SelectListItem();
                        item.Text = Convert.ToString(reader["parkName"]);
                        items.Add(item);
                    }

                }

            }
            catch (SqlException)
            {
                throw;
            }
            return items;
        }

        //insert a new survey into the table survey_result
        public bool InsertSurvey(Survey survey)
        {

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(SQL_InsertSurvey, conn);
                    cmd.Parameters.AddWithValue("@ParkCode", survey.ParkCode);
                    cmd.Parameters.AddWithValue("@State", survey.State);
                    cmd.Parameters.AddWithValue("@EmailAddress", survey.EmailAddress);
                    cmd.Parameters.AddWithValue("@ActivityLevel", survey.ActivityLevel);
                    int didWork = cmd.ExecuteNonQuery();

                    if (didWork > 0)
                    {
                        return true;
                    }
                }

            }
            catch (SqlException)
            {

                throw;
            }
            return false;
        }

    }
}