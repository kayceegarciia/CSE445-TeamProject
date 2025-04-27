using System;
using WeatherWebApp.DeployedWeatherRef; // Your service reference
using SecurityTools; // Your DLL

namespace WeatherWebApp
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblStartTime.Text = "App started at: " + Application["StartTime"];
            }
        }

        protected void btnWeather_Click(object sender, EventArgs e)
        {
            try
            {
                Service1Client client = new Service1Client(); // WCF proxy
                string[] forecast = client.Weather5Day(txtZip.Text);
                lblWeather.Text = string.Join("<br/>", forecast);
            }
            catch (Exception ex)
            {
                lblWeather.Text = "Error fetching forecast: " + ex.Message;
            }
        }

        protected void btnHash_Click(object sender, EventArgs e)
        {
            try
            {
                string password = txtPass.Text;
                CryptoUtils utils = new CryptoUtils(); // From your DLL

                string hashed = utils.HashPassword(password);
                string encrypted = utils.Encrypt(password);
                string decrypted = utils.Decrypt(encrypted);

                lblHash.Text = "Hashed: " + hashed;
                lblEncrypted.Text = "Encrypted: " + encrypted;
                lblDecrypted.Text = "Decrypted: " + decrypted;
            }
            catch (Exception ex)
            {
                lblHash.Text = "Error: " + ex.Message;
            }
        }
    }
}