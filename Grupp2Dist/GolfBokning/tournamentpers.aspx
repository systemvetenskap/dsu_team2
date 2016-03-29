<%@ Page Language="C#" MasterPageFile="~/headSite.Master" AutoEventWireup="true" EnableEventValidation="false" ValidateRequest="false" CodeBehind="tournamentpers.aspx.cs" Inherits="GolfBokning.tournamentpers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form runat="server" id="formTournamentPers">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" />
        <script type="text/javascript">
            function MinMetod() {
                var value = $("#ContentPlaceHolder1_DropDownListTournament option:selected").val();
                var sendString = "_" + value + "_";
                $("#ContentPlaceHolder1_ListBoxChosen option").each(function () {
                    sendString += $(this).val() + ";";
                });

                var teamID = $("#<%=DropLiTeam.ClientID %> option:selected").val();
                console.log(teamID);
                if (teamID == undefined || teamID.lengt < 1) {
                    console.log("alla");
                    teamID = "0";
                }
                else {

                }
                console.log(teamID);
                PageMethods.MyFunction(sendString, teamID, MyMethod_Success, MyMethod_Fail);
            }

            function MyMethod_Success(msg) {
                $("#ContentPlaceHolder1_PanelResponse").show();
            }

            function MyMethod_Fail(msg) {
                console.log(msg);
            }

            function CheckIfRemove() {
                var id_tournament = $("#ContentPlaceHolder1_DropDownListTournament option:selected").val();
                var id_member = $("#ContentPlaceHolder1_ListBoxChosen option:selected").val();
                PageMethods.canBeDeleted(id_tournament, id_member, CheckIfRemove_Success, CheckIfRemove_Fail);
            }

            function CheckIfRemove_Success(msg) {
                if (msg == "true") {
                    var value = $("#ContentPlaceHolder1_ListBoxChosen").find(":selected").val();
                    var name = $("#ContentPlaceHolder1_ListBoxChosen").find(":selected").text();
                    var remove = $("#ContentPlaceHolder1_ListBoxChosen").find(":selected");
                    if (name != "") {
                        remove.remove();
                        $("#ContentPlaceHolder1_ListBoxAll").append("<option value='" + value + "'>" + name + "</option>");
                        SortText("ContentPlaceHolder1_ListBoxAll");
                    }
                } else {
                    alert("Du kan inte ta bort en medlem som redan har ett resultat på denna tävling!");
                }
            }

            function CheckIfRemove_Fail(msg) {
                console.log(msg);
            }


        function OnGetMessageSuccess(result, userContext, methodName) {
            alert(result);
        }
        function OnGetMessageFailure(error, userContext, methodName) {
            alert(error.get_message());
        }
        $(function () {
            $("#ContentPlaceHolder1_PanelResponse").hide();
            $("#ContentPlaceHolder1_ButtonA").bind("click", function () {
                var value = $("#ContentPlaceHolder1_ListBoxAll").find(":selected").val();
                var name = $("#ContentPlaceHolder1_ListBoxAll").find(":selected").text();
                var remove = $("#ContentPlaceHolder1_ListBoxAll").find(":selected");
                if (name != "") {
                    remove.remove();
                    $("#ContentPlaceHolder1_ListBoxChosen").append("<option value='" + value + "'>" + name + "</option>");
                    SortText("ContentPlaceHolder1_ListBoxChosen");
                }
            });

            $("#ContentPlaceHolder1_ButtonR").bind("click", function () {
                CheckIfRemove();
            }); 
        });

        function SortText(id) {
            var list = $("#" + id);
            var listitems = list.children("option").get();
            listitems.sort(function (a, b) {
                var compA = $(a).text().toUpperCase();
                var compB = $(b).text().toUpperCase();
                return (compA < compB) ? -1 : (compA > compB) ? 1 : 0;
            });

            $.each(listitems, function (idx, itm) {
                list.append(itm);
            });

        }

        $(function () {
            var availableTagsAll = [ <%= SuggestionListAll %>];
            var availableTagsCho = [ <%= SuggestionListCho %>];
            var availableTagsChoSort = [ <%= SuggestionListChoSort %>];
            var availableTagsDefault = [ <%= SuggestionListDefault %>];

            $.each(availableTagsDefault, function (a, b) {
                $("#ContentPlaceHolder1_ListBoxAll").append(b);
            });

            $.each(availableTagsCho, function (a, b) {
                $("#ContentPlaceHolder1_ListBoxChosen").append(b);
            });

            function ucwords(str) {
                return (str + '').replace(/^([a-z])|\s+([a-z])/g, function ($1) {
                    return $1.toUpperCase();
                });
            }

            $("#ContentPlaceHolder1_TextBoxSortAll").bind("keyup", function (e) {
                var input = $("#ContentPlaceHolder1_TextBoxSortAll").val();
                if (input != "") {
                    if (input.match(/[a-zA-Z]/i)) {
                        $("#ContentPlaceHolder1_ListBoxAll").empty();
                        var output = "";
                        var list = "";
                        var splitList = [];
                        $.each(availableTagsAll, function (k, v) {
                            input = input.toLowerCase();
                            val = v.split(":");
                            id = val[1].replace(/(.*?)_/, "");
                            val = val[0];
                            val = val.toLowerCase();

                            if (val.indexOf(input) != -1) {
                                list += val + ":" + id + ";";

                            }
                        });
                        splitList = list.split(";");
                        $.each(splitList, function (key, full) {
                            var split = full.split(":");
                            if (split[0] != "" && split[1] != undefined) {
                                output += "<option value='" + split[1] + "'>" + ucwords(split[0]) + "</option>;";
                            }
                        });
                        var splt = output.split(";");
                        $.each(splt, function (k, v) {
                            $("#ContentPlaceHolder1_ListBoxAll").append(v);
                        });
                    } else {
                        $("#ContentPlaceHolder1_ListBoxAll").empty();
                        var output = "";
                        var list = "";
                        var splitList = [];
                        $.each(availableTagsAll, function (k, v) {
                            input = input.toLowerCase();
                            val = v.split(":");
                            golf_id = val[1];
                            id = val[1].replace(/(.*?)_/, "");
                            val = val[0];
                            val = val.toLowerCase();
                            if (golf_id.indexOf(input) != -1) {
                                list += val + ":" + id + ";";
                            }
                        });
                        splitList = list.split(";");
                        $.each(splitList, function (key, full) {
                            var split = full.split(":");
                            if (split[0] != "" && split[1] != undefined) {
                                output += "<option value='" + split[1] + "'>" + ucwords(split[0]) + "</option>;";
                            }
                        });
                        var splt = output.split(";");
                        $.each(splt, function (k, v) {
                            $("#ContentPlaceHolder1_ListBoxAll").append(v);
                        });
                    }
                } else {
                    GetDefault();
                }
            });

            $("#ContentPlaceHolder1_TextBoxSortChosen").bind("keyup", function (e) {
                var input = $("#ContentPlaceHolder1_TextBoxSortChosen").val();
                if (input != "") {
                    if (input.match(/[a-zA-Z]/i)) {
                        $("#ContentPlaceHolder1_ListBoxChosen").empty();
                        var output = "";
                        var list = "";
                        var splitList = [];
                        $.each(availableTagsChoSort, function (k, v) {
                            console.log(v);
                            input = input.toLowerCase();
                            val = v.split(":");
                            id = val[1].replace(/(.*?)_/, "");
                            val = val[0];
                            val = val.toLowerCase();
                            if (val.indexOf(input) != -1) {
                                list += val + ":" + id + ";";
                            }
                        });
                        splitList = list.split(";");
                        $.each(splitList, function (key, full) {
                            var split = full.split(":");
                            if (split[0] != "" && split[1] != undefined) {
                                output += "<option value='" + split[1] + "'>" + ucwords(split[0]) + "</option>;";
                            }
                        });
                        var splt = output.split(";");
                        $.each(splt, function (k, v) {
                            $("#ContentPlaceHolder1_ListBoxChosen").append(v);
                        });
                    } else {
                        $("#ContentPlaceHolder1_ListBoxChosen").empty();
                        var output = "";
                        var list = "";
                        var splitList = [];
                        $.each(availableTagsChoSort, function (k, v) {
                            input = input.toLowerCase();
                            val = v.split(":");
                            id = val[1].replace(/(.*?)_/, "");
                            golf_id = val[1];
                            val = val[0];
                            val = val.toLowerCase();
                            if (golf_id.indexOf(input) != -1) {
                                list += val + ":" + id + ";";
                            }
                        });
                        splitList = list.split(";");
                        $.each(splitList, function (key, full) {
                            var split = full.split(":");
                            if (split[0] != "" && split[1] != undefined) {
                                output += "<option value='" + split[1] + "'>" + ucwords(split[0]) + "</option>;";
                            }
                        });
                        var splt = output.split(";");
                        $.each(splt, function (k, v) {
                            $("#ContentPlaceHolder1_ListBoxChosen").append(v);
                        });
                    }
                } else {
                    GetDefaultCho();
                }
            });
        });

        function GetDefault() {
            $("#ContentPlaceHolder1_ListBoxAll").empty();
            var availableTagsDefault = [ <%= SuggestionListDefault %>];
            var splitted = availableTagsDefault.toString().split(";");
            $.each(splitted, function (k, v) {
                v = v.replace(",", "");
                $("#ContentPlaceHolder1_ListBoxAll").append(v);
            });
        }
        function GetDefaultCho() {
            $("#ContentPlaceHolder1_ListBoxChosen").empty();
            var availableTagsCho = [ <%= SuggestionListCho %>];
            var splitted2 = availableTagsCho.toString().split(";");
            $.each(splitted2, function (k2, v2) {
                v2 = v2.replace(",", "");
                $("#ContentPlaceHolder1_ListBoxChosen").append(v2);
            });
        }
        $(document).ready(function () {
            $("#ContentPlaceHolder1_DropDownListTournament").focus(function () {
                $("ContentPlaceHolder1_DropDownListTournament").find("option").eq(0).remove();
            });
            $("#ContentPlaceHolder1_DropDownListTournament").blur(function () {
                console.log("blur");
            });

            $("#ContentPlaceHolder1_txtTeamName").bind("keyup", function () {
                if ($(this).val() != "") {
                    $("#ContentPlaceHolder1_ButtonNewTeam").show();
                } else {
                    $("#ContentPlaceHolder1_ButtonNewTeam").hide();
                }
            });
        });

        GetDefault();
        GetDefaultCho();
        </script>
        <style>
            #ContentPlaceHolder1_PanelStartStop1 {
                float: left;
                width: 48%;
                margin-right: 4%;
            }

            #ContentPlaceHolder1_PanelStartStop2 {
                float: left;
                width: 48%;
            }

            .hide-me {
                display: none;
            }

            .btn-remove {
                margin-bottom: 30px;
            }

            .btn-upd {
                margin-bottom: 15px;
                margin-left: auto;
                margin-right: auto;
                display: block;
            }
        </style>
        <asp:Label ID="Label1" runat="server" Text="<h2>Välj tävling</h2>"></asp:Label><br />
        <asp:DropDownList ID="DropDownListTournament" runat="server" CssClass="form-control" AutoPostBack="true" EnableViewState="true"></asp:DropDownList><br />
        <asp:Panel ID="PanelHideShow" runat="server">

            <asp:Panel ID="TeamFormst" runat="server">
                <asp:Label ID="Label2" runat="server" Text="<h2>Lag</h2>"></asp:Label><br />
                <asp:Label ID="lblNewTeam" runat="server" Text="Välj existerande lag: "></asp:Label>
                <asp:DropDownList ID="DropLiTeam" runat="server" AutoPostBack="true" EnableViewState="true" CssClass="form-control"></asp:DropDownList>
                <br />
                <asp:Label ID="Label3" runat="server" Text="Eller skapa ett nytt lag"></asp:Label><br />
                <asp:TextBox ID="txtTeamName" runat="server" CssClass="form-control"></asp:TextBox>
                <asp:Button ID="ButtonNewTeam" runat="server" Text="Lägg till" CssClass="btn btn-success btn-add" />
                <br />
            </asp:Panel>
            <br />

            <asp:Panel ID="PanelNTeam" runat="server">
                <asp:Label ID="Label16" runat="server" Text="<h2>Deltagare</h2>"></asp:Label><br />
                <asp:Label ID="Label19" runat="server" Text="Sortera (Sök)"></asp:Label>
                <asp:Label ID="Label20" runat="server" Text="Sortera (Sök)"></asp:Label>
                <asp:TextBox ID="TextBoxSortAll" runat="server" CssClass="form-control"></asp:TextBox><br />
                <asp:TextBox ID="TextBoxSortChosen" runat="server" CssClass="form-control"></asp:TextBox><br />
                <asp:Label ID="Label17" runat="server" Text="Alla medlemmar"></asp:Label>
                <asp:Label ID="Label18" runat="server" Text="Valda medlemmar"></asp:Label>
                <asp:ListBox ID="ListBoxAll" runat="server"></asp:ListBox>
                <asp:ListBox ID="ListBoxChosen" runat="server"></asp:ListBox>
                <asp:Panel ID="Panel1" runat="server" CssClass="button-memb">
                    <asp:Button ID="ButtonA" runat="server" Text="Lägg till" CssClass="btn btn-success btn-add" OnClientClick="return false;" />
                </asp:Panel>
                <asp:Panel ID="Panel2" runat="server" CssClass="button-memb">
                    <asp:Button ID="ButtonR" runat="server" Text="Ta bort" CssClass="btn btn-danger btn-remove" OnClientClick="return false;" />
                </asp:Panel>
                <asp:Button ID="ButtonUpdate" runat="server" Text="Uppdatera" CssClass="btn btn-primary btn-upd" OnClientClick="MinMetod(); return false;" />
                <asp:Panel ID="PanelResponse" runat="server" CssClass="alert alert-success">
                    <asp:Label ID="LabelResponse" runat="server" Text="Uppdaterat!"></asp:Label>
                </asp:Panel>
            </asp:Panel>
            <asp:Label runat="server" ID="lblTeamComp"></asp:Label>
            <asp:Label ID="LabelTest" runat="server" Text=""></asp:Label>
        </asp:Panel>
    </form>
</asp:Content>
