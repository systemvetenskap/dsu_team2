﻿<%@ Page Language="C#" MasterPageFile="~/headSite.Master" AutoEventWireup="true" CodeBehind="staffmember.aspx.cs" Inherits="GolfBokning.staffmember" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        $(document).on("keydown", "#ContentPlaceHolder1_TextBoxSearch", function (e) {
            setTimeout(function () { $("#ContentPlaceHolder1_TextBoxSearch").focus() }, 1300);
            setTimeout(function () { $("#ContentPlaceHolder1_TextBoxSearch").setCursorToTextEnd() }, 1350);
        }).on("keyup", "#ContentPlaceHolder1_TextBoxSearch", function (e) {
            $("#ContentPlaceHolder1_TextBoxSearch").blur();
        });

        $(document).on("keydown", "#ContentPlaceHolder1_TextBoxStaffSearch", function (e) {
            setTimeout(function () { $("#ContentPlaceHolder1_TextBoxStaffSearch").focus() }, 1300);
            setTimeout(function () { $("#ContentPlaceHolder1_TextBoxStaffSearch").setCursorToTextEnd() }, 1350);
        }).on("keyup", "#ContentPlaceHolder1_TextBoxStaffSearch", function (e) {
            $("#ContentPlaceHolder1_TextBoxStaffSearch").blur();
        });

        (function ($) {
            $.fn.setCursorToTextEnd = function () {
                $initialVal = this.val();
                this.val($initialVal + ' ');
                this.val($initialVal);
            };
        })(jQuery);

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Hantera personal</h1>
<form id="formStaffMember" runat="server">
    <asp:ScriptManager ID="ScriptManagerLogin" EnablePartialRendering="true" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanelLogin" runat="server">
        <ContentTemplate>
        <div class="formLeft">
            <br />
            <asp:Label ID="LblMembers" runat="server" Text="Alla medlemmar"></asp:Label><br /><br />
            <asp:Label ID="LblSearch" runat="server" Text="Sortera på Golf ID: "></asp:Label><br />
            <asp:TextBox ID="TextBoxSearch" runat="server" AutoPostBack="true" CssClass="form-control"></asp:TextBox><br /><br />
            <asp:Label ID="LabelBoxMembers" runat="server" Text="Medlemmar: " CssClass="LabelBox"></asp:Label>
            <asp:ListBox ID="ListBoxMembers" runat="server" CssClass="form-control" AutoPostBack="true"></asp:ListBox><br /><br />
        </div>
        <div class="formMiddle">
            <asp:Button ID="BtnAdd" runat="server" Text="Lägg till >" CssClass="btn btn-success" />
            <asp:Button ID="BtnRemove" runat="server" Text="< Ta bort" CssClass="btn btn-danger" />
        </div>
        <div class="formRight">
            <br />
            <asp:Label ID="LblStaff" runat="server" Text="All personal"></asp:Label><br /><br />
            <asp:Label ID="LblStaffSearch" runat="server" Text="Sortera på Golf ID: "></asp:Label><br />
            <asp:TextBox ID="TextBoxStaffSearch" runat="server" AutoPostBack="true" CssClass="form-control"></asp:TextBox><br /><br />
            <asp:Label ID="LabelBoxStaff" runat="server" Text="Personal: " CssClass="LabelBox"></asp:Label>
            <asp:ListBox ID="ListBoxStaff" runat="server" CssClass="form-control" AutoPostBack="true"></asp:ListBox><br /><br /><br />
        </div>
        <asp:Label ID="LabelTest" runat="server" Text="asd" CssClass="alert alert-info"></asp:Label>
        </ContentTemplate>
    </asp:UpdatePanel>
</form>
</asp:Content>