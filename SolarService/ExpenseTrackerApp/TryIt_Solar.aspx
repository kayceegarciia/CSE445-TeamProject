<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TryIt_Solar.aspx.cs" Inherits="SolarServiceApp.TryIt_Solar" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>TryIt: SolarIntensity</title>
</head>
<body>
    <form id="form1" runat="server">
      <div style="max-width:400px;margin:2em auto;font-family:sans-serif;">
        <h2>TryIt: SolarIntensity</h2>
        <asp:Label ID="lblError" runat="server" ForeColor="Red" /><br/>

        <label>Latitude:</label>
        <asp:TextBox ID="txtLat" runat="server" /><br/>

        <label>Longitude:</label>
        <asp:TextBox ID="txtLon" runat="server" /><br/>

        <label>Panel Size (kW):</label>
        <asp:TextBox ID="txtSize" runat="server" /><br/><br/>

        <asp:Button ID="btnCalc" runat="server" Text="Calculate Output" OnClick="btnCalc_Click" /><br/><br/>

        <asp:Label ID="lblResult" runat="server" Font-Size="Large" />
      </div>
    </form>
</body>
</html>
