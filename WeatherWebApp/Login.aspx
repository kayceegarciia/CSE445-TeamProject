<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="YourNamespace.Login" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <title>Login or Register</title>
</head>
<body style="font-family: Arial; text-align: center; margin-top: 50px;">

    <h1>Login</h1>
    <form runat="server">
        Username: <asp:TextBox ID="txtLoginUsername" runat="server" /><br /><br />
        Password: <asp:TextBox ID="txtLoginPassword" runat="server" TextMode="Password" /><br /><br />
        <asp:Button ID="btnLogin" runat="server" Text="Login" OnClick="btnLogin_Click" /><br />
        <asp:Label ID="lblLoginMessage" runat="server" ForeColor="Red" /><br />
    </form>

    <hr style="margin: 50px;" />

    <h1>Register</h1>
    <form runat="server">
        New Username: <asp:TextBox ID="txtRegisterUsername" runat="server" /><br /><br />
        New Password: <asp:TextBox ID="txtRegisterPassword" runat="server" TextMode="Password" /><br /><br />

        Captcha: <asp:Label ID="lblCaptcha" runat="server" /> <br/>
        Enter Captcha: <asp:TextBox ID="txtCaptcha" runat="server" /><br /><br />

        <asp:Button ID="btnRegister" runat="server" Text="Register" OnClick="btnRegister_Click" /><br />
        <asp:Label ID="lblRegisterMessage" runat="server" ForeColor="Green" />
    </form>

</body>
</html>
