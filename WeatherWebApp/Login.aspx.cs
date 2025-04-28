using System;
using System.Web;
using System.Xml;
using System.IO;

namespace YourNamespace
{
    public partial class Login : System.Web.UI.Page
    {
        private string staffUsername = "admin";
        private string staffPassword = "1234";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Generate Captcha number
                Random rand = new Random();
                lblCaptcha.Text = rand.Next(10000, 99999).ToString();
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtLoginUsername.Text.Trim();
            string password = txtLoginPassword.Text.Trim();

            if (username == staffUsername && password == staffPassword)
            {
                Session["username"] = username;
                Session["type"] = "staff";
                Response.Redirect("Staff.aspx");
            }
            else
            {
                if (CheckMemberLogin(username, password))
                {
                    Session["username"] = username;
                    Session["type"] = "member";
                    Response.Redirect("Member.aspx");
                }
                else
                {
                    lblLoginMessage.Text = "Invalid login.";
                }
            }
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            string newUsername = txtRegisterUsername.Text.Trim();
            string newPassword = txtRegisterPassword.Text.Trim();
            string captchaInput = txtCaptcha.Text.Trim();

            if (captchaInput != lblCaptcha.Text)
            {
                lblRegisterMessage.ForeColor = System.Drawing.Color.Red;
                lblRegisterMessage.Text = "Captcha Incorrect!";
                return;
            }

            if (SaveNewMember(newUsername, newPassword))
            {
                lblRegisterMessage.ForeColor = System.Drawing.Color.Green;
                lblRegisterMessage.Text = "Registration successful! You can now login.";
            }
            else
            {
                lblRegisterMessage.ForeColor = System.Drawing.Color.Red;
                lblRegisterMessage.Text = "Registration failed.";
            }
        }

        private bool CheckMemberLogin(string username, string password)
        {
            string path = Server.MapPath("~/App_Data/Member.xml");

            if (!File.Exists(path))
                return false;

            XmlDocument doc = new XmlDocument();
            doc.Load(path);

            foreach (XmlNode userNode in doc.SelectNodes("/Users/User"))
            {
                string storedUsername = userNode["Username"].InnerText;
                string storedPassword = userNode["Password"].InnerText;

                if (storedUsername == username && storedPassword == password)
                    return true;
            }
            return false;
        }

        private bool SaveNewMember(string username, string password)
        {
            string path = Server.MapPath("~/App_Data/Member.xml");

            XmlDocument doc = new XmlDocument();

            if (!File.Exists(path))
            {
                XmlDeclaration declaration = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
                XmlElement root = doc.CreateElement("Users");
                doc.AppendChild(declaration);
                doc.AppendChild(root);
            }
            else
            {
                doc.Load(path);
            }

            XmlElement userElem = doc.CreateElement("User");

            XmlElement userNameElem = doc.CreateElement("Username");
            userNameElem.InnerText = username;

            XmlElement passwordElem = doc.CreateElement("Password");
            passwordElem.InnerText = password;

            userElem.AppendChild(userNameElem);
            userElem.AppendChild(passwordElem);

            doc.DocumentElement.AppendChild(userElem);

            doc.Save(path);
            return true;
        }
    }
}
