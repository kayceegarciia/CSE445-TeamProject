using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace SolarService.Controllers
{
    [RoutePrefix("api/solar")]
public class SolarController : ApiController
{
    private static readonly HttpClient _http = new HttpClient
    {
        BaseAddress = new Uri("https://power.larc.nasa.gov/")
    };

    [HttpGet, Route("intensity")]
    public async Task<IHttpActionResult> SolarIntensity(decimal lat, decimal lon)
    {
        try
        {
            // Build NASA POWER URL
            string endpoint = "api/temporal/climatology/point" +
                              "?parameters=ALLSKY_SFC_SW_DWN" +
                              "&community=RE" +
                              $"&latitude={lat}&longitude={lon}" +
                              "&format=JSON";

            // Fetch the JSON payload
            var resp = await _http.GetAsync(endpoint);
            var body = await resp.Content.ReadAsStringAsync();

            if (!resp.IsSuccessStatusCode)
                return Content(resp.StatusCode, new { resp.StatusCode, body });

            var root = JObject.Parse(body);
            // Navigate to the annual average
            JToken annToken = root
              .SelectToken("properties.parameter.ALLSKY_SFC_SW_DWN.ANN");

            if (annToken == null)
                return BadRequest("NASA response missing ANN field.");

            decimal annual = annToken.Value<decimal>();

            return Ok(annual);
        }
        catch (Exception ex)
        {
            return InternalServerError(ex);
        }
    }
}
}
