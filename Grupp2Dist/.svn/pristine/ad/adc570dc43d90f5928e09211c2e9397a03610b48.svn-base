<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/headSite.Master" CodeBehind="minasidor.aspx.cs" Inherits="GolfBokning.minasidor" %>


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
            function GetMessage(id, bookedby) {
                if (bookedby != "0") {
                    if (window.confirm("Vill du avboka alla på passet?")) {
                        PageMethods.removeBooking(id, id_member_cookie, bookedby, OnGetMessageSuccess, OnGetMessageFailure);
                    } else {
                        if (window.confirm("OK. Vill du avboka dig själv?")) {
                            PageMethods.removeBooking(id, id_member_cookie, 0, OnGetMessageSuccess, OnGetMessageFailure);
                        }
                    }
                } else {
                    PageMethods.removeBooking(id, id_member_cookie, 0, OnGetMessageSuccess, OnGetMessageFailure);
                }
                
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

            function RemoveTournament(id_tournament, bookedby) {
                var id_member = $(".lbl-hid").html();
                if (bookedby != "0") {
                    if (window.confirm("Vill du avboka alla på tävlingen?")) {
                        PageMethods.RemoveTournamentBooking(id_tournament, id_member, bookedby, Remove_Success, Remove_Fail);
                    } else {
                        if (window.confirm("OK. Vill du avboka dig själv från tävlingen?")) {
                            PageMethods.RemoveTournamentBooking(id_tournament, id_member, "0", Remove_Success, Remove_Fail);
                        }
                    }
                } else {
                    PageMethods.RemoveTournamentBooking(id_tournament, id_member, "0", Remove_Success, Remove_Fail);
                }
            }

          

            function Remove_Fail(msg) {
                console.log(msg);
            }

            $(document).ready(function () {
                PageMethods.fillTable(id_member_cookie, OnGetLoadSuccess, OnGetMessageFailure);
            });
            function OnGetLoadSuccess(result, userContext, methodName) {
                //$("#ContentPlaceHolder1_tbTable").text(result);

                document.getElementById("ContentPlaceHolder1_tbTable").innerHTML = result;
            }

          
            function Remove_Success(msg) {
                alert("Avbokat tävling!");
                window.location = "minasidor.aspx";
            }

            function Remove_Fail(msg) {
                console.log(msg);
            }

            $(function () {
                $("#ContentPlaceHolder1_txtbox_datejournal").datepicker({
                    dateFormat: 'yy-mm-dd'
                });

                $(document).keydown(function (e) {
                    var c = e.which;
                    if (c == 13) {
                        if ($("#ContentPlaceholder1_txtbox_head:focus")) {
                            $("[tabindex=99]").focus();
                        }
                    }
                });

                $("#ContentPlaceHolder1_txtbox_head").click(function () {
                    if ($("#ContentPlaceHolder1_txtbox_head").val() == "Rubrik") {
                        $("#ContentPlaceHolder1_txtbox_head").val("");
                    }

                });

                $("#ContentPlaceHolder1_txtbox_content").click(function () {
                    if ($("#ContentPlaceHolder1_txtbox_content").val() == "Skriv in ditt inlägg här") {
                        $("#ContentPlaceHolder1_txtbox_content").val("");
                    }

                });

            });
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
                    <div class="journalBody">
                        <asp:TextBox ID="txtbox_head" runat="server" Text="Rubrik" CssClass="form-control form-twnt"></asp:TextBox>
                        <asp:TextBox ID="txtbox_datejournal" runat="server" Text= "Datum" CssClass="form-control form-twnt"></asp:TextBox>
                        <asp:Button CssClass="btn btn-default" ID="btn_reset" runat="server" Text="Nytt Inlägg" OnClick="btn_reset_Click" /><br />
                        <asp:TextBox ID="txtbox_content" runat="server" TextMode="MultiLine" Rows="7" Width="100%" Text="Skriv in ditt inlägg här" CssClass="form-control form-textarea"></asp:TextBox><br /> 
                        <asp:Label ID="lblEntries" runat="server" Text="Tidigare inlägg"></asp:Label><br />
                        <asp:ListBox ID="libox_journalentrys" runat="server" AutoPostBack="True" OnSelectedIndexChanged="libox_journalentrys_SelectedIndexChanged"></asp:ListBox>
                        <asp:Button CssClass="btn btn-primary" ID="btn_journalentry" runat="server" Text="Spara inlägg" OnClick="btnjournalentry_Click" />
                        <asp:Button CssClass="btn btn-danger" ID="btn_deletejournalentry" runat="server" Text="Radera Inlägg" OnClick="btn_deletejournalentry_Click" />      
                    </div>
                   
                </div>
            </div>
        </div>
    </form>
</asp:Content>
