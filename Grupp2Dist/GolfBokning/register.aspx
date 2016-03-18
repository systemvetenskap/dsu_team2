<%@ Page Language="C#" MasterPageFile="~/headSite.Master" AutoEventWireup="true" CodeBehind="register.aspx.cs" Inherits="GolfBokning.register" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="StyleSheet.css" />
    <script type="text/javascript" src="Scripts/registerjs.js"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<form id="formRegister" runat="server">
<asp:ScriptManager ID="ScriptManagerRegister" EnablePartialRendering="true" runat="server"></asp:ScriptManager>
 <asp:UpdatePanel ID="UpdatePanelRegister" runat="server">
    <ContentTemplate>
        <h1 class="register-h1">Registrera konto</h1>
        <div class="registerForm">
            <label>E-mail: </label>
            <asp:TextBox id="TextEmail" runat="server" required="required" CssClass="form-control register-control" /><br />
            <label>Lösenord: </label>
            <asp:TextBox id="TextPassword" TextMode="Password" runat="server" required="required" CssClass="form-control register-control" /><br />
            <label>Förnamn: </label>
            <asp:TextBox id="TextFirstName" runat="server" required="required" CssClass="form-control register-control" /><br />
            <label>Efternamn: </label>
            <asp:TextBox id="TextLastName" runat="server" required="required" CssClass="form-control register-control" /><br />
            <label>Personnummer: (ex. 1970-01-01)</label>
            <asp:TextBox id="TextSSN" runat="server" required="required" CssClass="form-control register-control" /><br />
            <label>Adress: </label>
            <asp:TextBox id="TextAddress" runat="server" required="required" CssClass="form-control register-control" /><br />
            <label>Postnummer: </label>
            <asp:TextBox id="TextZipCode" runat="server" maxLength="9" required="required" CssClass="form-control register-control" /><br />
            <label>Ort: </label>
            <asp:TextBox id="TextPlace" runat="server" required="required" CssClass="form-control register-control" /><br />
            <label>Kön: </label><br />
            <asp:RadioButtonList ID="RadioGender" runat="server" CssClass="radio-inline">
                <asp:ListItem Value="Male" Selected="true">Man</asp:ListItem>
                <asp:ListItem Value="Female">Kvinna</asp:ListItem>
            </asp:RadioButtonList><br />
            <label>Medlemsnivå: </label>
            <asp:DropDownList ID="DropCategory" runat="server" CssClass="form-control">

            </asp:DropDownList><br />
            <asp:Button ID="ButtonRegister" runat="server" Text="Registrera" CssClass="btn btn-primary btn-lg" />
            <asp:Label ID="LabelResponse" runat="server" Text=""></asp:Label>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
</form>
</asp:Content>
