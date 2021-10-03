<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="prjASP_ICE_Task.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login Page</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
             <center>
    <div class="jumbotron">
        <h1>Welcome to depression</h1>
    </div>

    <div class="login">

        <p>
        <asp:TextBox ID="txtUsername" placeholder="Username" runat="server" ToolTip="Please enter your username"></asp:TextBox>
            
        </p>

        <p>
        <asp:TextBox ID="txtPassword" placeholder="Password" runat="server" ToolTip="Please enter your password"></asp:TextBox>
        </p>

        <p>
            <asp:Button ID="btnLogin" runat="server" Text="Login" Width="66px" OnClick="btnLogin_Click" />
            <asp:Button ID="btnRegister" runat="server" Text="Register" Width="66px" OnClick="btnRegister_Click" />
        </p>
        <p>
            <asp:Label ID="lblError" runat="server" Text="Error message!" ForeColor="Red"></asp:Label>
        </p>

    </div>
    </center>
        </div>
    </form>
</body>
</html>
