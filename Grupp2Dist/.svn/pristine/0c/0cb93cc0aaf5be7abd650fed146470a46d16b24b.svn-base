<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="resultapp.aspx.cs" Inherits="GolfBokning.resultapp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Resultatapp</title>
    <script type="text/javascript" src="http://code.jquery.com/jquery-2.2.1.min.js"></script>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/css/bootstrap.min.css" />
    <link rel="stylesheet" href="//code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css" />
    <script type="text/javascript" src="Scripts/resultapp.js"></script>
    <script type="text/javascript" src="Scripts/jquery.cookie.js"></script>
    <script src="//code.jquery.com/ui/1.11.4/jquery-ui.js"></script>
    <script src="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/js/bootstrap.min.js"></script>
    <script src="Scripts/jquery-ui-timepicker-addon.js"></script>
    <link rel="stylesheet" href="resultapp.css" />
    <% // <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" /> %>
    <link href='https://fonts.googleapis.com/css?family=Montserrat:400,700|Open+Sans+Condensed:300,300italic,700|Abel' rel='stylesheet' type='text/css' />
</head>
<body>
    <div class="frame">
        <div class="iphone">
            <img src="Images/frame_upper.jpg" class="frame_upper" />
            <form runat="server">
                <asp:ScriptManager ID="ScriptManager1" runat="server" />
                <asp:Label ID="LabelHiddenLeader" runat="server" Text=""></asp:Label>
                <asp:Label ID="LabelHiddenTournamentID" runat="server" Text=""></asp:Label>
                <asp:Label ID="LabelHiddenHole" runat="server" Text=""></asp:Label>
                <asp:Label ID="LabelHiddesTee" runat="server" Text=""></asp:Label>
                <asp:Label ID="LabelHiddenTeam" runat="server" Text=""></asp:Label>
                <div id="heading">
                    <div class="menu">
                        <span class="glyphicon glyphicon-menu-hamburger"></span>
                    </div>

                    <h1>Resultatapp</h1>
                </div>
                <div class="menu-popup">
                    <ul>
                        <li>
                            <a href="resultapp.aspx">Välj tävling
                            </a>
                        </li>
                        <li>
                            <a href="applogin.aspx">Logga ut
                            </a>
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
                    <asp:Label ID="LabelName" runat="server" Text="" CssClass="lblname lbl alert alert-warning"></asp:Label><br />
                    <asp:Label ID="LabelTournament" runat="server" Text="Välj tävling" CssClass="lbl"></asp:Label><br />

                    <asp:DropDownList ID="DropDownListChoose" runat="server" CssClass="form-control"></asp:DropDownList>

                    <div class="logout">
                        <a href="applogin.aspx">
                            <div class="btn btn-danger">Logga ut</div>
                        </a>
                    </div>
                </asp:Panel>

                <asp:Panel ID="PanelBoxes" runat="server" CssClass="content">
                    <asp:Label ID="LabelH2" runat="server" Text="Label" CssClass="lblname alert alert-warning"></asp:Label>
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
                            <asp:Button ID="ButtonStart" runat="server" Text="Starta" CssClass="btn btn-success btn-start btn-lg" />
                        </div>
                    </div>
                </asp:Panel>
                <script>
                    $(document).ready(function () {
                        $("#LabelHiddenHole").hide();
                        var id_tournament = $("#LabelHiddenTournamentID").html();
                        var hole = $("#LabelHiddenHole").html();

                        $("#rbtLstTee").change(function () {
                            var value = $("#rbtLstTee input[type='radio']:checked").val();
                            $.cookie("tee", "", -1);
                            $.cookie("tee", id_tournament + ":" + value);
                        });

                        if ($("#rbtLstTee").is(":visible")) {

                            var tempCookie = $.cookie("tee");
                            var temp = tempCookie.split(':');

                            $(temp).each(function () {
                                if ($("#LabelHiddenTournamentID").html() == temp[0]) {
                                    $("#rbtLstTee input[value='" + temp[1] + "']").attr("checked", true);
                                } else {
                                    $("#rbtLstTee input[value='57 Gul']").attr("checked", true);
                                }
                            });

                        }

                        $("#ButtonStart").click(function () {
                            var teeCookie = $.cookie("tee");
                            if (teeCookie == undefined) {
                                $.cookie("tee", "57 Gul");
                            } else {
                                var tempCookie = $.cookie("tee");
                                var temp = tempCookie.split(':');
                                $(temp).each(function () {
                                    if (temp[1] == "undefined") {
                                        $.cookie("tee", "57 Gul");
                                    }
                                });
                            }

                            // Här någonstans...
                            var cook = $.cookie("tee").split(':');
                            $(cook).each(function () { console.log(cook[1]) });
                            var hole = $("#LabelHiddenHole").html();
                            var id_tournament = $("#LabelHiddenTournamentID").html();

                            var tempCookie = $.cookie("t_hole");
                            var temp = tempCookie.split(':');
                            $.cookie("t_hole", id_tournament + ":" + hole);
                        });
                    });
                </script>
                <%--<asp:Panel ID="PanelTees" runat="server" CssClass="content">
                    <asp:Label ID="LabelTeeName" runat="server" Text="Label" CssClass="lblname alert alert-warning"></asp:Label>
                    <asp:Label ID="Label1" runat="server" Text="<h2>Välj tee</h2>"></asp:Label>
                    <asp:Panel ID="PanelTee" runat="server">
                        <asp:RadioButtonList ID="rbtLstTee" runat="server">
                            <asp:ListItem Text="62 Vit" Value="62 Vit"></asp:ListItem>
                            <asp:ListItem Text="57 Gul" Value="57 Gul"></asp:ListItem>
                            <asp:ListItem Text="54 Blå" Value="54 Blå"></asp:ListItem>
                            <asp:ListItem Text="50 Röd" Value="50 Röd"></asp:ListItem>
                        </asp:RadioButtonList><br />
                    </asp:Panel>
                </asp:Panel>--%>
                <asp:Panel ID="PanelHoles" runat="server" CssClass="content">
                    <asp:Label ID="LabelNameHole" runat="server" Text="Label" CssClass="lblname alert alert-warning"></asp:Label>
                    <asp:Label ID="lblHoleNrApp" runat="server" Text="Hål Nr: "></asp:Label><br />
                    <asp:Label ID="lblPlayerPlaceApp" runat="server" Text="Nuvarande placering: "></asp:Label><br />
                    <br />
                    <asp:Label ID="LabelNbr" runat="server" Text="Antal slag:"></asp:Label>
                    <asp:TextBox MaxLength="2" ID="TextBox1" runat="server" CssClass="form-control antal-slag"></asp:TextBox><br />
                    <br />
                    <table id="resultsTable" runat="server" style="width: 100%;" visible="true" class="table table-bordered">
                        <tr>
                            <th>
                                <asp:Label CssClass="hcpIndexLabelApp" ID="Label21" runat="server" Text="HcpIndex"></asp:Label></th>
                            <th>
                                <asp:Label CssClass="hcpIndexLabelApp" ID="Label24" runat="server" Text="Par"></asp:Label></th>
                            <th>
                                <asp:Label CssClass="hcpIndexLabelApp" ID="Label31" runat="server" Text="Erhållna slag"></asp:Label></th>
                            <th>
                                <asp:Label CssClass="hcpIndexLabelApp" ID="Label35" runat="server" Text="Netto"></asp:Label></th>
                            <th>
                                <asp:Label CssClass="hcpIndexLabelApp" ID="Label39" runat="server" Text="+/-"></asp:Label></th>
                        </tr>
                        <tr>
                            <td class="hcpIndexLabelApp" id="lblHcpIndexApp" runat="server"></td>
                            <td class="hcpIndexLabelApp" id="lblParApp" runat="server"></td>
                            <td class="hcpIndexLabelApp" id="lblPlayHcpApp1" runat="server"></td>
                            <td class="hcpIndexLabelApp" id="lblNettoApp1" runat="server"></td>
                            <td class="hcpIndexLabelApp" id="lblPlusMinus1" runat="server"></td>
                        </tr>
                    </table>
                    <asp:Button ID="btnCalc" runat="server" Text="Uppdatera" OnClick="btnCalc_Click" CssClass="btn btn-success" />
                    <br />
                    <br />
                    <asp:Button ID="btnBack" runat="server" Text="&laquo; Föregående Hål" OnClick="btnBack_Click" CssClass="btn btn-primary" />
                    <asp:Button ID="btnNext" runat="server" Text="Nästa Hål &raquo;" OnClick="btnNext_Click" CssClass="btn btn-primary" />
                </asp:Panel>
               <%-- <div id="tableTeamHolder" runat="server" class="content">
                    <table id="tableTeam" runat="server" style="width: 100%;" visible="true" class="table table-bordered">
                        <tr>
                            <th>Hål Nr</th>
                            <td id="lblHoleNrAppTeam" runat="server"></td>
                        </tr>
                        <tr>
                            <th id="player1lbl" runat="server">Spelare 1</th>
                            <td>
                                <asp:TextBox ID="player1h" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <th id="player2lbl" runat="server">Spelare 2</th>
                            <td>
                                <asp:TextBox ID="player2h" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <th id="player3lbl" runat="server">Spelare 3</th>
                            <td>
                                <asp:TextBox ID="player3h" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <th id="player4lbl" runat="server">Spelare 4</th>
                            <td>
                                <asp:TextBox ID="player4h" runat="server"></asp:TextBox></td>
                        </tr>
                    </table>

                    <table id="tableTeamResultsSpec" runat="server" style="width: 100%;" visible="true" class="table table-bordered">
                        <tr>
                                                    <th><asp:Label CssClass="hcpIndexLabelApp" ID="Label2" runat="server" Text="HcpIndex"></asp:Label></th>
                            <th><asp:Label CssClass="hcpIndexLabelApp" ID="Label3" runat="server" Text="Par"></asp:Label></th> 
                            <th>
                                <asp:Label CssClass="hcpIndexLabelApp" ID="Label2" runat="server" Text="Namn"></asp:Label></th>
                            <th>
                                <asp:Label CssClass="hcpIndexLabelApp" ID="Label4" runat="server" Text="Erhållna slag"></asp:Label></th>
                            <th>
                                <asp:Label CssClass="hcpIndexLabelApp" ID="Label5" runat="server" Text="Netto"></asp:Label></th>
                            <th>
                                <asp:Label CssClass="hcpIndexLabelApp" ID="Label6" runat="server" Text="+/-"></asp:Label></th>
                        </tr>
                        <tr>
                          
                            <td class="hcpIndexLabelApp" id="lblHcpIndexAppTeamP1" runat="server"></td>
                            <td class="hcpIndexLabelApp" id="lblParAppTeamP1" runat="server"></td>
                            <td class="hcpIndexLabelApp" id="lblPlayerNameTeamP1" runat="server"></td>
                            <td class="hcpIndexLabelApp" id="lblPlayHcpApp1TeamP1" runat="server"></td>
                            <td class="hcpIndexLabelApp" id="lblNettoApp1TeamP1" runat="server"></td>
                            <td class="hcpIndexLabelApp" id="lblPlusMinus1TeamP1" runat="server"></td>
                        </tr>
                        <tr>
                            <td class="hcpIndexLabelApp" id="lblHcpIndexAppTeamP2" runat="server"></td>
                            <td class="hcpIndexLabelApp" id="lblParAppTeamP2" runat="server"></td>
                            <td class="hcpIndexLabelApp" id="lblPlayerNameTeamP2" runat="server"></td>
                            <td class="hcpIndexLabelApp" id="lblPlayHcpApp1TeamP2" runat="server"></td>
                            <td class="hcpIndexLabelApp" id="lblNettoApp1TeamP2" runat="server"></td>
                            <td class="hcpIndexLabelApp" id="lblPlusMinus1TeamP2" runat="server"></td>
                        </tr>
                        <tr>
                            <td class="hcpIndexLabelApp" id="lblHcpIndexAppTeamP3" runat="server"></td>
                            <td class="hcpIndexLabelApp" id="lblParAppTeamP3" runat="server"></td>
                            <td class="hcpIndexLabelApp" id="lblPlayerNameTeamP3" runat="server"></td>
                            <td class="hcpIndexLabelApp" id="lblPlayHcpApp1TeamP3" runat="server"></td>
                            <td class="hcpIndexLabelApp" id="lblNettoApp1TeamP3" runat="server"></td>
                            <td class="hcpIndexLabelApp" id="lblPlusMinus1TeamP3" runat="server"></td>
                        </tr>
                        <tr id="fourthRowAppResultsTeam" runat="server">
                                                        <td class="hcpIndexLabelApp" id="lblHcpIndexAppTeamP4" runat="server"></td>
                            <td class="hcpIndexLabelApp" id="lblParAppTeamP4" runat="server"></td>
                            <td class="hcpIndexLabelApp" id="lblPlayerNameTeamP4" runat="server"></td>
                            <td class="hcpIndexLabelApp" id="lblPlayHcpApp1TeamP4" runat="server"></td>
                            <td class="hcpIndexLabelApp" id="lblNettoApp1TeamP4" runat="server"></td>
                            <td class="hcpIndexLabelApp" id="lblPlusMinus1TeamP4" runat="server"></td>
                        </tr>
                    </table>
                    <asp:Button ID="btnRegTeamResults" runat="server" Text="Registrera" OnClick="btnRegTeamResults_Click" />
                    <br />
                    <br />
                     <asp:Button ID="buttonBack" runat="server" Text="&laquo; Föregående Hål" OnClick="btnBack_Click" CssClass="btn btn-primary" />
                    <asp:Button ID="buttonNext" runat="server" Text="Nästa Hål &raquo;" OnClick="btnNext_Click" CssClass="btn btn-primary" />
                </div>--%>
                <div class="logout">
                    <a href="applogin.aspx">
                        <div class="btn btn-danger">Logga ut</div>
                    </a>
                </div>
                <asp:Label ID="LabelTest" runat="server" Text=""></asp:Label>
            </form>
        </div>
    </div>
</body>
</html>
