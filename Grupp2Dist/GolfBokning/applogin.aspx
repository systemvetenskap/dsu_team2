<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="applogin.aspx.cs" Inherits="GolfBokning.applogin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login</title>
    <script type="text/javascript" src="http://code.jquery.com/jquery-2.2.1.min.js"></script>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/css/bootstrap.min.css" />
    <link rel="stylesheet" href="//code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css" />
    <script type="text/javascript" src="Scripts/resultapp.js"></script>
    <script src="//code.jquery.com/ui/1.11.4/jquery-ui.js"></script>
    <script src="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/js/bootstrap.min.js"></script>
    <script src="Scripts/jquery-ui-timepicker-addon.js"></script>
    <link rel="stylesheet" href="resultapp.css" />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" />
    <link href='https://fonts.googleapis.com/css?family=Montserrat:400,700|Open+Sans+Condensed:300,300italic,700|Abel' rel='stylesheet' type='text/css' />
</head>
<body class="app_bg">

        <div id="heading">
        <!-- <div class="menu">
            <span class="glyphicon glyphicon-menu-hamburger"></span>
        </div> -->
        
        <h1>Resultatapp</h1>
    </div>
    <!-- <div class="menu-popup">
        <ul>
            <li><span class="glyphicon glyphicon-star" style="font-size: 0.9em; margin-right: 5px;"></span>Topplista</li>
            <li><span class="glyphicon glyphicon-off" style="font-size: 0.9em; margin-right: 5px;"></span>Tävlingar</li>
        </ul>
    </div> -->
    <div class="content">
    <img src="Images/logo.png" class="logo" />
    <form id="formLogin" runat="server">
        <div class="loginForm">
            <label>E-mail: </label>
            <asp:TextBox runat="server" id="TextEmailLogin" CssClass="form-control" required="required" /><br />
            <label>Lösenord: </label>
            <asp:TextBox runat="server" id="TextPasswordLogin" CssClass="form-control" TextMode="Password" required="required" /><br />
            <asp:Button id="ButtonLogin" runat="server" Text="Logga in" CssClass="btn btn-success btn-lg" /><br /><br />
            <asp:Panel ID="PanelResponse" runat="server" CssClass="alert alert-warning">
                <asp:Label ID="LabelResponse" runat="server" Text="asd"></asp:Label>
            </asp:Panel>
        </div>
</form>
    </div>
</body>
</html>
