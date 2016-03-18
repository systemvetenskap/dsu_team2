<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/headSite.Master" CodeBehind="tournamentmembadd.aspx.cs" Inherits="GolfBokning.tournamentmembadd" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<form id="formMembAdd" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" />
    <script>
        function Join(id_tournament) {
            var id_member = $(".hidden_id").html();
            PageMethods.JoinTournament(id_tournament, id_member, Join_Success, Join_Fail);
        }

        function Join_Success(msg) {
            alert("Registrerat på tävling!");
            window.location = "minasidor.aspx";
        }

        function Join_Fail(msg) {
            console.log(msg);
        }

        function CheckJoin(id_tournament) {
            var id_member = $(".hidden_id").html();
            PageMethods.CheckJoinTournament(id_tournament, id_member, CheckJoin_Success, CheckJoin_Fail);
        }

        function CheckJoin_Success(msg) {
            if (msg.indexOf("error") != -1) {
                var id_tournament = msg.replace("error ", "");
                $(".btn").each(function () {
                    if ($(this).hasClass(id_tournament)) {
                        $(this).attr("disabled", true);
                        $(this).html("Anmäld redan");
                    }
                });
            }
        }

        function CheckJoin_Fail(msg) {
            console.log(msg);
        }
        function CheckJoinFull(id_tournament, nbr) {
            PageMethods.CheckJoinTournamentFull(id_tournament, nbr, CheckJoinFull_Success, CheckJoinFull_Fail);
        }

        function CheckJoinFull_Success(msg) {
            var temp = msg.replace("error ", "");
            temp = temp.split(';');

            var id_tournament = temp[0];
            var nbr = temp[1];

            $(".btn").each(function () {
                if ($(this).hasClass(id_tournament)) {
                    $(this).attr("disabled", true);
                    $(this).html("Fullt");
                }
            });
        }

        function CheckJoinFull_Fail(msg) {
            console.log(msg);
        }

        $(document).ready(function () {
            $(".btn").each(function () {
                var id = $(this).attr("data");
                var nbr = $(this).attr("data-nbr");
                CheckJoin(id);
                CheckJoinFull(id, nbr);
            });
        });
    </script>
    <style>
        .hidden_id {
            display: none;
        }
    </style>
    <asp:Label ID="LabelHiddenId" runat="server" Text=""></asp:Label>
    <asp:Label ID="Label1" runat="server" Text="<h1>Kommande tävlingar</h1>"></asp:Label>  
    <asp:Label ID="Label2" runat="server" Text="Välj tävlingsklass"></asp:Label>
    <asp:RadioButtonList ID="RadioButtonListClass" runat="server" CssClass="radio-inline">

    </asp:RadioButtonList>
    <asp:Panel ID="PanelTournaments" runat="server"></asp:Panel>
    <asp:Label ID="LabelTest" runat="server" Text=""></asp:Label>
    <asp:Panel ID="PanelBox" runat="server"></asp:Panel>
</form>
</asp:Content>