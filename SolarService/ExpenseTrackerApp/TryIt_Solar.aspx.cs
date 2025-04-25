using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Http;
using System.Text.RegularExpressions;
using SolarUtils;


namespace SolarServiceApp
{
    public partial class TryIt_Solar : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // On first load, prefill panel-size from cookie if available
            if (!IsPostBack && Request.Cookies["PreferredSize"] != null)
            {
                txtSize.Text = Request.Cookies["PreferredSize"].Value;
            }
        }

        protected void btnCalc_Click(object sender, EventArgs e)
        {
            lblError.Text = "";
            lblResult.Text = "";

            // Validate inputs
            if (!decimal.TryParse(txtLat.Text, out decimal lat) ||
                !decimal.TryParse(txtLon.Text, out decimal lon) ||
                !decimal.TryParse(txtSize.Text, out decimal size))
            {
                lblError.Text = "Please enter valid numbers for latitude, longitude, and panel size.";
                return;
            }

            // Persist panel size in a cookie
            var cookie = new HttpCookie("PreferredSize", size.ToString())
            {
                Expires = DateTime.Now.AddDays(30),
                HttpOnly = true
            };
            Response.Cookies.Add(cookie);

            // Call SolarService API
            try
            {
                using (var client = new HttpClient { BaseAddress = new Uri("https://localhost:44324/") })
                {
                    var response = client.GetAsync($"api/solar/intensity?lat={lat}&lon={lon}").Result;

                    if (!response.IsSuccessStatusCode)
                    {
                        lblError.Text = $"Service error: {response.ReasonPhrase}";
                        return;
                    }

                    // Read raw XML, strip tags, parse decimal
                    string raw = response.Content.ReadAsStringAsync().Result;
                    string stripped = Regex.Replace(raw, "<.*?>", "");
                    if (!decimal.TryParse(stripped, out decimal intensity))
                    {
                        lblError.Text = "Unexpected service response format.";
                        return;
                    }

                    // Compute estimated annual output using DLL
                    decimal annualOutput = PanelCalculator.EstimateAnnualOutput(intensity, size);

                    // Show results
                    lblResult.Text =
                      $"<strong>Intensity:</strong> {intensity:N2} kWh/m²/day<br/>" +
                      $"<strong>Est. Annual Output:</strong> {annualOutput:N2} kWh";
                }
            }
            catch (Exception ex)
            {
                lblError.Text = "Error calling service: " + ex.Message;
            }
        }
    }
}

