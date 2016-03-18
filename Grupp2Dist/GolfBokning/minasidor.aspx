﻿<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/headSite.Master" CodeBehind="minasidor.aspx.cs" Inherits="GolfBokning.minasidor" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Mina sidor</title>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/css/bootstrap.min.css" />
    <link rel="stylesheet" href="StyleSheet.css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="form1" runat="server">
        <!-- Container -->
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" EnablePartialRendering="true" />
        <script type='text/javascript'>
            var id_member_cookie = GetCookieID();
            function GetMessage(id) {
                PageMethods.removeBooking(id, id_member_cookie, OnGetMessageSuccess, OnGetMessageFailure);
            }
            function OnGetMessageSuccess(result, userContext, methodName) {
                PageMethods.fillTable(id_member_cookie, OnGetLoadSuccess, OnGetMessageFailure);
                alert(result);

            }
            function OnGetMessageFailure(error, userContext, methodName) {
                alert(error.get_message());
            }

            $(document).ready(function () {
                PageMethods.fillTable(id_member_cookie, OnGetLoadSuccess, OnGetMessageFailure);
            });
            function OnGetLoadSuccess(result, userContext, methodName)
            {
                //$("#ContentPlaceHolder1_tbTable").text(result);

                document.getElementById("ContentPlaceHolder1_tbTable").innerHTML = result;
            }

            function RemoveTournament(id_tournament) {
                var id_member = $(".lbl-hid").html();
                PageMethods.RemoveTournamentBooking(id_tournament, id_member, Remove_Success, Remove_Fail);
            }

            function Remove_Success(msg) {
                alert("Avbokat tävling!");
                window.location = "minasidor.aspx";
            }

            function Remove_Fail(msg) {
                console.log(msg);
            }
        </script>

        <div class="containerMinaSidor">
            <asp:Label ID="LabelHidden" runat="server" Text="" CssClass="lbl-hid"></asp:Label>
            <!-- main content -->
            <div class="mainContent">
                <!--  Mina bokningar -->
                <div class="mina.bokningar">
                    <h2>Mina bokningar</h2>

                    <table class="tableMinaBokningar table table-bordered table-responsive table-striped">
                        <thead>
                            <tr>
                                <th>Tid</th>
                                <th>Datum</th>
                                <th>Ändra</th>
                            </tr>
                        </thead>
                        <tbody id="tbTable" runat="server">
                        </tbody>
                    </table>
                    <asp:Button class="Button ButtonBokaTid btn btn-primary" ID="ButtonBokaTid" runat="server" Text="Boka ny tid" />
                </div>
                <br />
                <div class="mina.bokningar">
                    <h2>Mina tävlingar</h2>
                    <table class="tableMinaBokningar table table-bordered table-responsive table-striped">
                        <thead>
                            <tr>
                                <th>Tid</th>
                                <th>Datum</th>
                                <th>Tävling</th>
                                <th>Avboka</th>
                            </tr>
                        </thead>
                        <tbody id="Tbody1" runat="server">
                        </tbody>
                    </table>
                    <asp:Button class="Button ButtonBokaTid btn btn-primary" ID="ButtonAllTournaments" runat="server" Text="Visa alla tävlingar" />
                </div>
                
                <asp:Table ID="Table1" runat="server"></asp:Table>
                
                <div id="journalContainer" runat="server" class="journal">
                    <h2>Min Dagbok</h2>
                    <asp:TextBox ID="txtbox_head" runat="server" Text="Rubrik"></asp:TextBox>
                    <asp:TextBox ID="txtbox_datejournal" runat="server" Text= "Datum"></asp:TextBox>
                    <asp:Button CssClass="btn btn-default" ID="btn_journalentry" runat="server" Text="Spara inlägg" OnClick="btnjournalentry_Click" /><br /><br />
                    <asp:TextBox ID="txtbox_content" runat="server" TextMode="MultiLine" Rows="7" Width="100%" Text="Skriv in ditt inlägg här"></asp:TextBox><br /><br />
                    <asp:Label ID="lblEntries" runat="server" Text="Tidigare inlägg"></asp:Label><br />
                    <asp:ListBox ID="libox_journalentrys" runat="server" AutoPostBack="True" OnSelectedIndexChanged="libox_journalentrys_SelectedIndexChanged"></asp:ListBox>
                </div>
            </div>
        </div>
    </form>
</asp:Content>
