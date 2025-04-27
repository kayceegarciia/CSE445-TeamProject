<%@ Page Title="TryIt - Weather and Security Functions" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WeatherWebApp._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <h1>Member 3 TryIt Page</h1>

    <hr />

    <h2>TryIt: Weather Forecast Web Service</h2>
    <p>Enter a U.S. ZIP Code to get a 5-day forecast from your deployed web service:</p>
    ZIP Code:
    <asp:TextBox ID="txtZip" runat="server" />
    <asp:Button ID="btnWeather" runat="server" Text="Get Forecast" OnClick="btnWeather_Click" />
    <br /><br />
    <asp:Label ID="lblWeather" runat="server" ForeColor="Blue" />

    <hr />

    <h2>TryIt: Password Hashing, Encryption & Decryption (DLL Test)</h2>
    <p>Enter a password to test the custom Security DLL hashing and encryption methods:</p>
    Password:
    <asp:TextBox ID="txtPass" runat="server" TextMode="Password" />
    <asp:Button ID="btnHash" runat="server" Text="Process Password" OnClick="btnHash_Click" />
    <br /><br />
    <b>Hashed Output:</b><br />
    <asp:Label ID="lblHash" runat="server" ForeColor="Green" />
    <br /><br />

    <b>Encrypted Output:</b><br />
    <asp:Label ID="lblEncrypted" runat="server" ForeColor="Orange" />
    <br /><br />

    <b>Decrypted Output:</b><br />
    <asp:Label ID="lblDecrypted" runat="server" ForeColor="Purple" />

    <hr />

    <h2>TryIt: Application Start Time (Global.asax Event)</h2>
    <p>This shows the time your application started (from Global.asax):</p>
    <asp:Label ID="lblStartTime" runat="server" Font-Bold="true" />

</asp:Content>
