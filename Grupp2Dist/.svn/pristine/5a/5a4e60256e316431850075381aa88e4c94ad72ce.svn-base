<%@ Page Language="C#" MasterPageFile="~/headSite.Master" AutoEventWireup="true" CodeBehind="staffmember.aspx.cs" Inherits="GolfBokning.staffmember" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Hantera personal</h1>
    <form id="formStaffMember" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" />
            <script>
                    function Save() {
                        var sendString = "";
                        $("#ContentPlaceHolder1_ListBoxStaff option").each(function () {
                            sendString += $(this).val() + ";";
                        });
                        PageMethods.SaveDb(sendString, MyMethod_Success, MyMethod_Fail);
                    }

                    function MyMethod_Success(msg) {
                        console.log(msg);
                        if (msg == "Y") {
                            $("#ContentPlaceHolder1_PanelResponse").show();
                        }
                    }

                    function MyMethod_Fail(msg) {
                        console.log(msg);
                    }

                    $(function () {
                        var availableTagsDefault = [ <%= SuggestionListDefault %>];
                var availableTagsCho = [ <%= SuggestionListCho %>];
                var availableTagsAll = [ <%= SuggestionListAll %>];
                var availableTagsChoSort = [ <%= SuggestionListChoSort %>];

                function ucwords(str) {
                    return (str + '').replace(/^([a-z])|\s+([a-z])/g, function ($1) {
                        return $1.toUpperCase();
                    });
                }

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

                function GetDefault() {
                    $("#ContentPlaceHolder1_ListBoxAll").empty();
                    var availableTagsDefault = [ <%= SuggestionListDefault %>];
                    var splitted = availableTagsDefault.toString().split(";");
                    $.each(splitted, function (k, v) {
                        v = v.replace(",", "");
                        $("#ContentPlaceHolder1_ListBoxAll").append(v);
                    });
                }

                $.each(availableTagsDefault, function (a, b) {
                    $("#ContentPlaceHolder1_ListBoxMembers").append(b);
                });

                $.each(availableTagsCho, function (a, b) {
                    $("#ContentPlaceHolder1_ListBoxStaff").append(b);
                });

                $("#ContentPlaceHolder1_ButtonSave").click(function () {
                    $.each(availableTagsDefault, function (a, b) {
                        $("#ContentPlaceHolder1_ListBoxMembers").append(b);
                    });

                    $.each(availableTagsCho, function (a, b) {
                        $("#ContentPlaceHolder1_ListBoxStaff").append(b);
                    });

                });

                $("#ContentPlaceHolder1_TextBoxSearch").bind("keyup", function (e) {
                    var input = $("#ContentPlaceHolder1_TextBoxSearch").val();
                    if (input != "") {
                        if (input.match(/[a-zA-Z]/i)) {
                            $("#ContentPlaceHolder1_ListBoxMembers").empty();
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
                                $("#ContentPlaceHolder1_ListBoxMembers").append(v);
                            });
                        } else {
                            $("#ContentPlaceHolder1_ListBoxMembers").empty();
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
                                $("#ContentPlaceHolder1_ListBoxMembers").append(v);
                            });
                        }
                    } else {
                        GetDefault();
                    }
                });

                $("#ContentPlaceHolder1_TextBoxStaffSearch").bind("keyup", function (e) {
                    var input = $("#ContentPlaceHolder1_TextBoxStaffSearch").val();
                    if (input != "") {
                        if (input.match(/[a-zA-Z]/i)) {
                            $("#ContentPlaceHolder1_ListBoxStaff").empty();
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
                                $("#ContentPlaceHolder1_ListBoxStaff").append(v);
                            });
                        } else {
                            $("#ContentPlaceHolder1_ListBoxStaff").empty();
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
                                $("#ContentPlaceHolder1_ListBoxStaff").append(v);
                            });
                        }
                    } else {
                        GetDefaultCho();
                    }
                });

                $("#ContentPlaceHolder1_BtnAdd").bind("click", function () {
                    var value = $("#ContentPlaceHolder1_ListBoxMembers").find(":selected").val();
                    var name = $("#ContentPlaceHolder1_ListBoxMembers").find(":selected").text();
                    var remove = $("#ContentPlaceHolder1_ListBoxMembers").find(":selected");
                    if (name != "") {
                        remove.remove();
                        $("#ContentPlaceHolder1_ListBoxStaff").append("<option value='" + value + "'>" + name + "</option>");
                        SortText("ContentPlaceHolder1_ListBoxStaff");
                    }
                });
                $("#ContentPlaceHolder1_BtnRemove").bind("click", function () {
                    var value = $("#ContentPlaceHolder1_ListBoxStaff").find(":selected").val();
                    var name = $("#ContentPlaceHolder1_ListBoxStaff").find(":selected").text();
                    var remove = $("#ContentPlaceHolder1_ListBoxStaff").find(":selected");
                    if (name != "") {
                        remove.remove();
                        $("#ContentPlaceHolder1_ListBoxMembers").append("<option value='" + value + "'>" + name + "</option>");
                        SortText("ContentPlaceHolder1_ListBoxMembers");
                    }
                });
            });
        </script>
        <asp:UpdatePanel ID="UpdatePanelLogin" runat="server">
            <ContentTemplate>
                <div class="formLeft">
                    <br />
                    <asp:Label ID="LblMembers" runat="server" Text="Alla medlemmar"></asp:Label><br />
                    <br />
                    <asp:Label ID="LblSearch" runat="server" Text="Sortera på Golf ID: "></asp:Label><br />
                    <asp:TextBox ID="TextBoxSearch" runat="server" CssClass="form-control movealil"></asp:TextBox><br />
                    <br />
                    <asp:Label ID="LabelBoxMembers" runat="server" Text="Medlemmar: " CssClass="LabelBox"></asp:Label>
                    <asp:ListBox ID="ListBoxMembers" runat="server" CssClass="form-control"></asp:ListBox><br />
                    <br />
                </div>
                <div class="formMiddle">
                    <asp:Button ID="BtnAdd" runat="server" Text="Lägg till >" OnClientClick="return false;" CssClass="btn btn-success" />
                    <asp:Button ID="BtnRemove" runat="server" Text="< Ta bort" OnClientClick="return false;" CssClass="btn btn-danger" />
                </div>
                <div class="formRight">
                    <br />
                    <asp:Label ID="LblStaff" runat="server" Text="All personal"></asp:Label><br />
                    <br />
                    <asp:Label ID="LblStaffSearch" runat="server" Text="Sortera på Golf ID: "></asp:Label><br />
                    <asp:TextBox ID="TextBoxStaffSearch" runat="server" CssClass="form-control"></asp:TextBox><br />
                    <br />
                    <asp:Label ID="LabelBoxStaff" runat="server" Text="Personal: " CssClass="LabelBox"></asp:Label>
                    <asp:ListBox ID="ListBoxStaff" runat="server" CssClass="form-control"></asp:ListBox><br />
                    <br />
                    <br />
                </div>
                <div class="full" style="width: 100%; float:left; text-align:center;margin-bottom: 30px;">
                    <asp:Button ID="ButtonTest" runat="server" Text="Spara" OnClientClick="Save(); return false;" CssClass="btn btn-primary" />
                </div>
                <asp:Panel ID="PanelResponse" runat="server"  CssClass="alert alert-success">
                    <asp:Label ID="LabelTest" runat="server" Text="Sparat!"></asp:Label>
                </asp:Panel>
                
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</asp:Content>
