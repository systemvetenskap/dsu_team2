<%@ Page Title="" Language="C#" MasterPageFile="~/headSite.Master" AutoEventWireup="true" CodeBehind="tournamentresult.aspx.cs" Inherits="GolfBokning.tournamentresult" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="form1" runat="server" style="width:800px; min-height:400px">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" />
    <div class="tournamentResult">
        <div class="pad chooseTournament">
            <label >Välj en tävling</label><br/>
            <asp:DropDownList style="min-width:35%;" ID="DropDownTournament" runat="server" Width="20%"></asp:DropDownList><br/>
        </div>
        
        <div class="pad">
             <%--<asp:Label ID="LabelTournamentName" runat="server" Text="Label"></asp:Label>--%>
            <asp:Label ID="LabelResultTabel" runat="server" Text=""></asp:Label>
            <asp:Label class="pad" ID="LabelSecect" runat="server" Text="Resultat för tävlingsdeltagare"></asp:Label> <br />
            <asp:ListBox style="min-width:35%;" ID="ListBox1" runat="server" OnSelectedIndexChanged="ListBox1_SelectedIndexChanged" AutoPostBack="true" class="pad" ></asp:ListBox><br/>
            
            <div class="pad">
            <asp:GridView ID="GridViewScore" CssClass="table table-bordered pad" runat="server" Width="20%" >

            </asp:GridView>
            </div>

            <div class="pad">
            <div id="hiddenResultsDetail" runat="server">
            </div>
        </div>

     <%--       <table id="hiddenResultsDetailTable" class="table table-bordered table-striped" runat="server" style="width: 100%;" visible="true">
                <tr>
                    <td>hål 1</td>
                    <td>tee1</td> 
                    <td>tee2</td> 
                    <td>par</td> 
                    <td>hcp Index</td> 
                    <td>slag</td> 
                    <td>erhållna slag</td> 
                    <td>netto</td> 
                    <td>poäng</td>                 
                </tr>
                <tr>
                    <td>hål 2</td>
                    <td>tee1</td> 
                    <td>tee2</td> 
                    <td>par</td> 
                    <td>hcp Index</td> 
                    <td>slag</td> 
                    <td>erhållna slag</td> 
                    <td>netto</td> 
                    <td>poäng</td> 
                </tr>
                <tr>
                    <td>hål 3</td>
                    <td>tee1</td> 
                    <td>tee2</td> 
                    <td>par</td> 
                    <td>hcp Index</td> 
                    <td>slag</td> 
                    <td>erhållna slag</td> 
                    <td>netto</td> 
                    <td>poäng</td>                    
                </tr>
                <tr>
                    <td>hål 4</td>
                    <td>tee1</td> 
                    <td>tee2</td> 
                    <td>par</td> 
                    <td>hcp Index</td> 
                    <td>slag</td> 
                    <td>erhållna slag</td> 
                    <td>netto</td> 
                    <td>poäng</td>                    
                </tr>
                <tr>
                    <td>hål 5</td>
                    <td>tee1</td> 
                    <td>tee2</td> 
                    <td>par</td> 
                    <td>hcp Index</td> 
                    <td>slag</td> 
                    <td>erhållna slag</td> 
                    <td>netto</td> 
                    <td>poäng</td> 
                </tr>
                <tr>
                    <td>hål 6</td>
                    <td>tee1</td> 
                    <td>tee2</td> 
                    <td>par</td> 
                    <td>hcp Index</td> 
                    <td>slag</td> 
                    <td>erhållna slag</td> 
                    <td>netto</td> 
                    <td>poäng</td> 
                </tr>
                <tr>
                    <td>hål 7</td>
                    <td>tee1</td> 
                    <td>tee2</td> 
                    <td>par</td> 
                    <td>hcp Index</td> 
                    <td>slag</td> 
                    <td>erhållna slag</td> 
                    <td>netto</td> 
                    <td>poäng</td> 
                </tr>
                <tr>
                    <td>hål 8</td>
                    <td>tee1</td> 
                    <td>tee2</td> 
                    <td>par</td> 
                    <td>hcp Index</td> 
                    <td>slag</td> 
                    <td>erhållna slag</td> 
                    <td>netto</td> 
                    <td>poäng</td> 
                </tr>
                <tr>
                    <td>hål 9</td>
                    <td>tee1</td> 
                    <td>tee2</td> 
                    <td>par</td> 
                    <td>hcp Index</td> 
                    <td>slag</td> 
                    <td>erhållna slag</td> 
                    <td>netto</td> 
                    <td>poäng</td> 
                </tr>
                <tr>
                    <td>hål 10</td>
                    <td>tee1</td> 
                    <td>tee2</td> 
                    <td>par</td> 
                    <td>hcp Index</td> 
                    <td>slag</td> 
                    <td>erhållna slag</td> 
                    <td>netto</td> 
                    <td>poäng</td> 
                </tr>
                <tr>
                    <td>hål 11</td>
                    <td>tee1</td> 
                    <td>tee2</td> 
                    <td>par</td> 
                    <td>hcp Index</td> 
                    <td>slag</td> 
                    <td>erhållna slag</td> 
                    <td>netto</td> 
                    <td>poäng</td> 
                </tr>
                <tr>
                    <td>hål 12</td>
                    <td>tee1</td> 
                    <td>tee2</td> 
                    <td>par</td> 
                    <td>hcp Index</td> 
                    <td>slag</td> 
                    <td>erhållna slag</td> 
                    <td>netto</td> 
                    <td>poäng</td> 
                </tr>
                <tr>
                    <td>hål 13</td>
                    <td>tee1</td> 
                    <td>tee2</td> 
                    <td>par</td> 
                    <td>hcp Index</td> 
                    <td>slag</td> 
                    <td>erhållna slag</td> 
                    <td>netto</td> 
                    <td>poäng</td> 
                </tr>
                <tr>
                    <td>hål 14</td>
                    <td>tee1</td> 
                    <td>tee2</td> 
                    <td>par</td> 
                    <td>hcp Index</td> 
                    <td>slag</td> 
                    <td>erhållna slag</td> 
                    <td>netto</td> 
                    <td>poäng</td> 
                </tr>
                <tr>
                    <td>hål 15</td>
                    <td>tee1</td> 
                    <td>tee2</td> 
                    <td>par</td> 
                    <td>hcp Index</td> 
                    <td>slag</td> 
                    <td>erhållna slag</td> 
                    <td>netto</td> 
                    <td>poäng</td> 
                </tr>
                <tr>
                    <td>hål 16</td>
                    <td>tee1</td> 
                    <td>tee2</td> 
                    <td>par</td> 
                    <td>hcp Index</td> 
                    <td>slag</td> 
                    <td>erhållna slag</td> 
                    <td>netto</td> 
                    <td>poäng</td> 
                </tr>
                <tr>
                    <td>hål 17</td>
                    <td>tee1</td> 
                    <td>tee2</td> 
                    <td>par</td> 
                    <td>hcp Index</td> 
                    <td>slag</td> 
                    <td>erhållna slag</td> 
                    <td>netto</td> 
                    <td>poäng</td> 
                </tr>
                <tr>
                    <td>hål 18</td>
                    <td>tee1</td> 
                    <td>tee2</td> 
                    <td>par</td> 
                    <td>hcp Index</td> 
                    <td>slag</td> 
                    <td>erhållna slag</td> 
                    <td>netto</td> 
                    <td>poäng</td> 
                </tr>
            </table>--%>

            
        </div>
<%--        <script>
            $(document).ready(function () {
                $("tr").click(function () {
                    var id = $(this).attr("data");
                });
                $('[data="0"]').click(function () {
                    $('#ContentPlaceHolder1_hiddenResultsDetail').css('visibility', 'visible');
                });
            });

    </script>--%>

    </div>
    </form>
</asp:Content>

<%--<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:DropDownList ID="DropDownTournament" runat="server" Width="20%"></asp:DropDownList><br/>
        <asp:Label ID="LabelTournamentName" runat="server" Text="Label"></asp:Label>
        <asp:Label ID="LabelResultTabel" runat="server" Text=""></asp:Label>
        <script>
         $(document).ready(function () {
            $("#resultrow0").click(function () {
                alert("wasd");
            });
        });
    </script>

    </div>
    </form>
</body>
</html>--%>
