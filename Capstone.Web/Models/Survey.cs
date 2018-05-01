using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Capstone.Web.Models
{
    public class Survey
    {

        public string ParkCode { get; set; }

        [Required (ErrorMessage = "Please select a park from the list")]
        public string ParkName { get; set; }

        [Required (ErrorMessage = "Please enter a valid email address")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression("^([a-zA-Z0-9_\\-\\.]+)@([a-zA-Z0-9_\\-\\.]+)\\.([a-zA-Z]{2,5})$")]
        public string EmailAddress { get; set; }

        public string State { get; set; }
        public string StateCode { get; set; }
        public string ActivityLevel { get; set; }
        public string GetStateName(string stateCode)
        {
            string found = "";
            Dictionary<string, string> states = StateAndCodes();
            foreach (KeyValuePair<string, string> kvp in states)
            {
                if (kvp.Key == stateCode)
                {
                    found = kvp.Value;
                }
            }
            return found;
        }

        //for setting dropdown and then inserting state name into survey_result
        public Dictionary<string, string> StateAndCodes()
        {
            Dictionary<string, string> states = new Dictionary<string, string>();

            states.Add("--", "N/A");
            states.Add("AL", "Alabama");
            states.Add("AK", "Alaska");
            states.Add("AZ", "Arizona");
            states.Add("AR", "Arkansas");
            states.Add("CA", "California");
            states.Add("CO", "Colorado");
            states.Add("CT", "Connecticut");
            states.Add("DE", "Delaware");
            states.Add("DC", "District of Columbia");
            states.Add("FL", "Florida");
            states.Add("GA", "Georgia");
            states.Add("HI", "Hawaii");
            states.Add("ID", "Idaho");
            states.Add("IL", "Illinois");
            states.Add("IN", "Indiana");
            states.Add("IA", "Iowa");
            states.Add("KS", "Kansas");
            states.Add("KY", "Kentucky");
            states.Add("LA", "Louisiana");
            states.Add("ME", "Maine");
            states.Add("MD", "Maryland");
            states.Add("MA", "Massachusetts");
            states.Add("MI", "Michigan");
            states.Add("MN", "Minnesota");
            states.Add("MS", "Mississippi");
            states.Add("MO", "Missouri");
            states.Add("MT", "Montana");
            states.Add("NE", "Nebraska");
            states.Add("NV", "Nevada");
            states.Add("NH", "New Hampshire");
            states.Add("NJ", "New Jersey");
            states.Add("NM", "New Mexico");
            states.Add("NY", "New York");
            states.Add("NC", "North Carolina");
            states.Add("ND", "North Dakota");
            states.Add("OH", "Ohio");
            states.Add("OK", "Oklahoma");
            states.Add("OR", "Oregon");
            states.Add("PA", "Pennsylvania");
            states.Add("RI", "Rhode Island");
            states.Add("SC", "South Carolina");
            states.Add("SD", "South Dakota");
            states.Add("TN", "Tennessee");
            states.Add("TX", "Texas");
            states.Add("UT", "Utah");
            states.Add("VT", "Vermont");
            states.Add("VA", "Virginia");
            states.Add("WA", "Washington");
            states.Add("WV", "West Virginia");
            states.Add("WI", "Wisconsin");
            states.Add("WY", "Wyoming");

            return states;

        }

        public List<SelectListItem> Codes()
        {
            List<SelectListItem> codes = new List<SelectListItem>();
            Dictionary<string, string> states = StateAndCodes();
            foreach (KeyValuePair<string, string> kvp in states)
            {
                SelectListItem item = new SelectListItem();
                item.Text = kvp.Key;
                codes.Add(item);

            }
            return codes;
        }

    }
}