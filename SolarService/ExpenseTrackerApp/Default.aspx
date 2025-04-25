<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="SolarServiceApp.Default" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>Expense Tracker – Public Home</title>
</head>
<body>
  <form id="form1" runat="server" style="font-family:sans-serif;max-width:800px;margin:2em auto;">
    <h1>Welcome to Solar Service!</h1>
    <p>
      Log and categorize your daily expenses, view summaries, or explore our demo services below.  
      Click <strong>Member</strong> to register/login, or <strong>Staff</strong> if you’re a TA/grader.
    </p>

    <!-- Navigation buttons -->
    <asp:Button ID="btnMember" runat="server" Text="Member" PostBackUrl="~/Member.aspx" />
    <span> </span>
    <asp:Button ID="btnStaff"  runat="server" Text="Staff"  PostBackUrl="~/Staff.aspx"  />

    <h2>Service Directory</h2>
    <table border="1" cellpadding="6" cellspacing="0" style="width:100%;border-collapse:collapse;">
      <tr style="background:#eee;">
        <th>Provider</th>
        <th>Type</th>
        <th>Operation</th>
        <th>Parameters</th>
        <th>TryIt</th>
      </tr>
      <!-- RESTful service -->
      <tr>
        <td>Jesus Ayala</td>
        <td>RESTful Service</td>
        <td>SolarIntensity(decimal lat, decimal lon)</td>
        <td>lat, lon</td>
        <td><a href="TryIt_Solar.aspx">TryIt</a></td>
      </tr>
      <!-- DLL function -->
      <tr>
        <td>Jesus Ayala</td>
        <td>DLL Function</td>
        <td>EstimateAnnualOutput(decimal dailyIntensity, decimal panelSize)</td>
        <td>dailyIntensity, panelSize</td>
        <td>(called in TryIt page)</td>
      </tr>
      <!-- Cookie component -->
      <tr>
        <td>Jesus Ayala</td>
        <td>Cookie</td>
        <td>PreferredSize</td>
        <td>panelSize stored in cookie</td>
        <td>(persisted by TryIt page)</td>
      </tr>
    </table>
  </form>
</body>
</html>
