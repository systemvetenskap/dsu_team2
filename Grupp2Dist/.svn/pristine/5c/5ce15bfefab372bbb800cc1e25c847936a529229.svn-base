<%@ Page Language="C#" MasterPageFile="~/headSite.Master" AutoEventWireup="true" CodeBehind="member.aspx.cs" Inherits="GolfBokning.members" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" src="Scripts/member.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Hantera medlem</h1>
<form id="formEditMember" runat="server">
    <script>
        $(function () {
            var availableTags = [ <%= SuggestionList %>];

            $("#<%= TextGolfId.ClientID %>").autocomplete({
                source: availableTags

            });
        });
    </script>

    <asp:Panel ID="PanelResponse" runat="server" CssClass="alert alert-warning">
        <asp:Label ID="LabelResponse" runat="server" Text=""></asp:Label>
    </asp:Panel><asp:Panel ID="PanelSuccess" runat="server" CssClass="alert alert-success success-align">
        <asp:Label ID="LabelSuccess" runat="server" Text=""></asp:Label>
    </asp:Panel><br /><br />
    <asp:Label ID="LabelMemberID" runat="server" Text="Medlem"></asp:Label>
    <asp:TextBox ID="TextGolfId" runat="server" CssClass="form-control"></asp:TextBox> <br />
    <asp:Button ID="ButtonCollectMember" runat="server" Text="Hämta medlem" CssClass="btn btn-default" formnovalidate="formnovalidate" /><br /><br />
    <asp:Panel runat="server" ID="PanelPopupForm">
        <asp:TextBox ID="TextBoxHiddenID" runat="server"></asp:TextBox><br />
        <asp:Label ID="LabelEmail" runat="server" Text="E-mail"></asp:Label>
        <asp:TextBox ID="TextEmail" runat="server" CssClass="form-control" required="required"></asp:TextBox><br />
        <asp:Label ID="LabelPhone" runat="server" Text="Telefonnummer"></asp:Label>
        <asp:TextBox ID="TextPhone" runat="server" CssClass="form-control" required="required"></asp:TextBox><br />
        <asp:Label ID="LabelFirstName" runat="server" Text="Förnamn"></asp:Label>
        <asp:TextBox ID="TextFirstName" runat="server" CssClass="form-control uc-all" required="required"></asp:TextBox><br />
        <asp:Label ID="LabelLastName" runat="server" Text="Efternamn"></asp:Label>
        <asp:TextBox ID="TextLastName" runat="server" CssClass="form-control uc-all" required="required"></asp:TextBox><br />
        <asp:Label ID="Label1" runat="server" Text="Personnummer (ex. 1970-01-01)"></asp:Label>
        <asp:TextBox ID="TextBoxSSN" runat="server" CssClass="form-control" MaxLength="10" required="required"></asp:TextBox><br />
        <asp:Label ID="LabelAddress" runat="server" Text="Adress"></asp:Label>
        <asp:TextBox ID="TextAddress" runat="server" CssClass="form-control uc-first" required="required"></asp:TextBox><br />
        <asp:Label ID="LabelZipcode" runat="server" Text="Postnummer"></asp:Label>
        <asp:TextBox ID="TextZipcode" runat="server" CssClass="form-control" MaxLength="6" required="required"></asp:TextBox><br />
        <asp:Label ID="LabelPlace" runat="server" Text="Ort"></asp:Label>
        <asp:TextBox ID="TextPlace" runat="server" CssClass="form-control uc-all" required="required"></asp:TextBox><br />
        <asp:Label ID="LabelGender" runat="server" Text="Kön"></asp:Label><br />
        <asp:RadioButtonList ID="RadioButtonGender" runat="server" CssClass="radio-inline">
            <asp:ListItem Value="Male">Man</asp:ListItem>
            <asp:ListItem Value="Female">Kvinna</asp:ListItem>
        </asp:RadioButtonList><br />
        <asp:Label ID="LabelHcp" runat="server" Text="Handikap"></asp:Label>
        <asp:TextBox ID="TextHcp" runat="server" CssClass="form-control"></asp:TextBox><br />
        <asp:Label ID="LabelCategory" runat="server" Text="Medlemskategori"></asp:Label><br />
        <asp:DropDownList ID="DropDownCategories" runat="server" CssClass="form-control"></asp:DropDownList><br />
        <asp:Label ID="LabelPayed" runat="server" Text="Betalt?"></asp:Label><br />
        <asp:CheckBox ID="CheckBoxPayed" runat="server" /><br /><br />
        <asp:Button ID="ButtonUppdate" runat="server" Text="Uppdatera" CssClass="btn btn-primary btn-lg"/><br /><br />
    </asp:Panel>
</form>
</asp:Content>
