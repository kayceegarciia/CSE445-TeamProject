using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;


namespace WeatherService
{
    public class Service1 : IService1
    {
        //Using OpenWeatherMap API
        //--------------------------------------------------------------------------------------------------------------------------------------------------
        public string[] Weather5Day(string zipcode)
        {
            try
            {
                // OpenWeatherMap API key and default country
                string apiKey = "637d33b693cba4f8de9e9a6819d14f2b"; // You can switch to proj5 key if needed
                string country = "us"; // Required to form proper ZIP format for the API

                // API endpoint with query parameters
                string url = $"https://api.openweathermap.org/data/2.5/forecast?zip={zipcode},{country}&appid={apiKey}&units=imperial";

                // Create and send HTTP GET request to the API
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "GET";

                // Handle the API response
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    // Read response content into a string
                    string result = reader.ReadToEnd();

                    // Parse the JSON result using Newtonsoft.Json
                    JObject json = JObject.Parse(result);

                    // Prepare to collect 5 unique daily forecasts
                    var forecasts = new List<string>();
                    var datesAdded = new HashSet<string>();

                    // Loop through forecast entries (3-hour intervals)
                    foreach (var item in json["list"])
                    {
                        // Extract the date part (YYYY-MM-DD) from the datetime
                        string dt_txt = item["dt_txt"].ToString();
                        string date = dt_txt.Split(' ')[0];

                        // If this date hasn’t been added yet, grab the forecast
                        if (!datesAdded.Contains(date))
                        {
                            string temp = item["main"]["temp"].ToString(); // Get temperature
                            string desc = item["weather"][0]["description"].ToString(); // Get weather description
                            forecasts.Add($"{date}: {desc}, {temp}°F"); // Format the forecast string
                            datesAdded.Add(date); // Track this date
                        }

                        // Stop after collecting 5 days
                        if (forecasts.Count == 5)
                            break;
                    }

                    // Return the final array of forecasts
                    return forecasts.ToArray();
                }
            }
            catch (WebException ex)
            {
                // Handle common network or API errors
                return new string[] { "Error fetching weather data.", ex.Message };
            }
            catch (Exception ex)
            {
                // Handle unexpected issues
                return new string[] { "Unexpected error occurred.", ex.Message };
            }
        }

    }
}
