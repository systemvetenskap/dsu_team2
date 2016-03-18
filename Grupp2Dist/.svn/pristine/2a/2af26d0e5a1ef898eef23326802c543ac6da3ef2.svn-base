<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="resultapp.aspx.cs" Inherits="GolfBokning.resultapp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Resultatapp</title>
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
<body>
    <form runat="server">
        <asp:Label ID="LabelHiddenLeader" runat="server" Text=""></asp:Label>
        <div id="heading">
            <div class="menu">
                <span class="glyphicon glyphicon-menu-hamburger"></span>
            </div>
        
            <h1>Resultatapp</h1>
        </div>
        <div class="menu-popup">
            <ul>
                <li>
                    <a href="resultapp.aspx">
                        Välj tävling
                    </a>
                </li>
                <li class="showLeaderBoard">
                    Leaderboard
                </li>
            </ul>
        </div>
    
        <asp:Panel ID="PanelLeaderboard" runat="server" CssClass="leaderboard">
            <div class="closeLeader">
                <span class="glyphicon glyphicon-remove closeBox"></span>
            </div>
            <h2 class="leaderHeader"><span class="glyphicon glyphicon-star"></span>Leaderboard<span class="glyphicon glyphicon-star"></span></h2>
        </asp:Panel>

        <asp:Panel ID="PanelChoose" runat="server" CssClass="content">
            <asp:Label ID="LabelName" runat="server" Text="" CssClass="lblname lbl"></asp:Label><br />
            <asp:Label ID="LabelTournament" runat="server" Text="Välj tävling" CssClass="lbl"></asp:Label><br />
            <asp:DropDownList ID="DropDownListChoose" runat="server" CssClass="form-control"></asp:DropDownList>
            <div class="logout">
                <a href="applogin.aspx">
                    <div class="btn btn-danger">Logga ut</div>
                </a>
            </div>
        </asp:Panel>

        <asp:Panel ID="PanelBoxes" runat="server" CssClass="content">
            <asp:Label ID="LabelH2" runat="server" Text="Label" CssClass="lblname"></asp:Label>
            <div class="boxes">
                <div class="box">
                    <table class="table table-bordered table-responsive">
                        <tbody>
                            <tr>
                                <th>Beskrivning</th>
                                <td>
                                    <asp:Label ID="LabelTDesc" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <th>Antal hål</th>
                                <td>
                                    <asp:Label ID="LabelTNbrHoles" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <th>Antal deltagare</th>
                                <td>
                                    <asp:Label ID="LabelTNbrComp" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <th>Tävlingen startar</th>
                                <td>
                                    <asp:Label ID="LabelTStarttime" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <th>Tävlingen slutar</th>
                                <td>
                                    <asp:Label ID="LabelTEndtime" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <th>Din starttid</th>
                                <td>
                                    <asp:Label ID="LabelYStarttime" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <asp:Button ID="ButtonStart" runat="server" Text="Starta" CssClass="btn btn-success btn-start btn-lg"/>
                </div>
            </div>
            <div class="logout">
                <a href="applogin.aspx">
                    <div class="btn btn-danger">Logga ut</div>
                </a>
            </div>
        </asp:Panel>

        <asp:Label ID="LabelTest" runat="server" Text=""></asp:Label>
    </form>
</body>
</html>
