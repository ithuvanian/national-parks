using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using Capstone.Web.DAL;
using Capstone.Web.Models;

namespace Capstone.Web.Controllers
{
    public class HomeController : Controller
    {

        string connectionString = ConfigurationManager.ConnectionStrings["NPGeekDB"].ConnectionString;

        // GET: Home
        public ActionResult Index()
        {

            //boolean session variable for C / F  (F when page is first loaded)
            if (Session["Celsius"] == null)
            {
                bool celsius = false;
                Session["Celsius"] = celsius;
            }

            ParkDAL dal = new ParkDAL(connectionString);
            List<Park> allParks = dal.GetParks();

            return View("Index", allParks);
        }


        //access ParkDAL, check temperature setting before getting weather forecast
        //TempScale is used for displaying in the view
        public ActionResult ParkDetail(string parkCode)
        {
            ParkDAL pdal = new ParkDAL(connectionString);
            Park thisPark = pdal.GetParkDetail(parkCode);
            WeatherDAL wdal = new WeatherDAL(connectionString);
            bool celsius = (bool)Session["Celsius"];
            if (celsius)
            {
                Weather parkForecast = wdal.GetCelsiusForecast(parkCode);
                thisPark.WeatherForecast = parkForecast;
                parkForecast.TempScale = "C";
            }
            else
            {
                Weather parkForecast = wdal.GetForecast(parkCode);
                thisPark.WeatherForecast = parkForecast;
                parkForecast.TempScale = "F";
            }
            return View("ParkDetail", thisPark);
        }


        //same as above, but switch Session variable between C and F
        public ActionResult SwitchUnits(string parkCode)
        {
            ParkDAL pdal = new ParkDAL(connectionString);
            Park thisPark = pdal.GetParkDetail(parkCode);
            WeatherDAL wdal = new WeatherDAL(connectionString);

            bool celsius = (bool)Session["Celsius"];
            if (celsius)
            {
                celsius = false;
                Weather parkForecast = wdal.GetForecast(parkCode);
                thisPark.WeatherForecast = parkForecast;
            }
            else
            {
                celsius = true;
                Weather parkForecast = wdal.GetCelsiusForecast(parkCode);
                thisPark.WeatherForecast = parkForecast;
            }
            Session["Celsius"] = celsius;

            return RedirectToAction("ParkDetail", thisPark);
        }


        public ActionResult Survey()
        {
            SurveyDAL dal = new SurveyDAL(connectionString);

            //creates the dropdown list for parknames
            ViewBag.ParkNameDropdown = dal.ParkNameDropdown();
            Survey s = new Survey();

            return View("Survey", s);
        }


        [HttpPost]
        public ActionResult Survey(Survey survey)
        {
            SurveyDAL dal = new SurveyDAL(connectionString);

            //get park code from the park name selected in dropdown list through method in DAO
            survey.ParkCode = dal.GetParkCodeForSurvey(survey.ParkName);
            ViewBag.ParkNameDropdown = dal.ParkNameDropdown();
            ViewBag.StateCodeDropdown = survey.Codes();
            survey.State = survey.GetStateName(survey.StateCode);
            //get state name from statecode method in model

            //if insert works then send to results page
            if (!ModelState.IsValid)
            {
                return View("Survey", survey);
            }
            dal.InsertSurvey(survey);
            return RedirectToAction("SurveyResults");
        }


        public ActionResult SurveyResults()
        {
            SurveyDAL dal = new SurveyDAL(connectionString);
            List<SurveyResult> results = dal.GetSurveysResults();

            return View("SurveyResults", results);
        }

        public ActionResult About()
        {

            return View();
        }


    }
}
