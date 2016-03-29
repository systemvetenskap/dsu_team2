<%@ Page Language="C#" MasterPageFile="~/headSite.Master" AutoEventWireup="true" CodeBehind="tournamentpers.aspx.cs" Inherits="GolfBokning.tournamentpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<form runat="server" id="formTournamentPers">
<asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" EnablePartialRendering="true" />
    <script>
        $(function () {
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
                var value = $("#ContentPlaceHolder1_ListBoxChosen").find(":selected").val();
                var name = $("#ContentPlaceHolder1_ListBoxChosen").find(":selected").text();
                var remove = $("#ContentPlaceHolder1_ListBoxChosen").find(":selected");
                if (name != "") {
                    remove.remove();
                    $("#ContentPlaceHolder1_ListBoxAll").append("<option value='" + value + "'>" + name + "</option>");
                    SortText("ContentPlaceHolder1_ListBoxAll");
                }
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
                            val = val[0];
                            val = val.toLowerCase();
                            if (val.indexOf(input) != -1) {
                                list += val + ":" + k + ";";
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
                            val = val[0];
                            val = val.toLowerCase();
                            if (golf_id.indexOf(input) != -1) {
                                list += val + ":" + k + ";";
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
                            input = input.toLowerCase();
                            val = v.split(":");
                            val = val[0];
                            val = val.toLowerCase();
                            if (val.indexOf(input) != -1) {
                                list += val + ":" + k + ";";
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
                            golf_id = val[1];
                            val = val[0];
                            val = val.toLowerCase();
                            if (golf_id.indexOf(input) != -1) {
                                list += val + ":" + k + ";";
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
    <asp:DropDownList ID="DropDownListTournament" runat="server" CssClass="form-control"></asp:DropDownList><br />
    <asp:Panel ID="PanelHideShow" runat="server">
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
        <asp:Button ID="ButtonA" runat="server" Text="Lägg till" CssClass="btn btn-success btn-add" />
    </asp:Panel>
    <asp:Panel ID="Panel2" runat="server" CssClass="button-memb">
        <asp:Button ID="ButtonR" runat="server" Text="Ta bort" CssClass="btn btn-danger btn-remove" />
    </asp:Panel>
    <asp:Button ID="ButtonUpdate" runat="server" Text="Uppdatera" CssClass="btn btn-primary btn-upd" />
    <asp:Label ID="LabelTest" runat="server" Text="Label"></asp:Label>
    </asp:Panel>
</form>
</asp:Content>