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
            <label>E-mail: <span class="required">*</span></label>
            <asp:TextBox id="TextEmail" runat="server" required="required" CssClass="form-control register-control" /><br />
            <label>Lösenord: <span class="required">*</span></label>
            <asp:TextBox id="TextPassword" TextMode="Password" runat="server" required="required" CssClass="form-control register-control" /><br />
            <label>Telefonnummer: <span class="required">*</span></label>
            <asp:TextBox id="TextPhone" runat="server" required="required" MaxLength="10" CssClass="form-control register-control" /><br />
            <label>Förnamn: <span class="required">*</span></label>
            <asp:TextBox id="TextFirstName" runat="server" required="required" CssClass="form-control register-control uc-all" /><br />
            <label>Efternamn: <span class="required">*</span></label>
            <asp:TextBox id="TextLastName" runat="server" required="required" CssClass="form-control register-control uc-all" /><br />
            <label>Födelsedatum: (ex. 1970-01-01) <span class="required">*</span></label>
            <asp:TextBox id="TextSSN" runat="server" required="required" CssClass="form-control register-control" /><br />
            <label>Adress: <span class="required">*</span></label>
            <asp:TextBox id="TextAddress" runat="server" required="required" CssClass="form-control register-control uc-first" /><br />
            <label>Postnummer: <span class="required">*</span></label>
            <asp:TextBox id="TextZipCode" runat="server" maxLength="9" required="required" CssClass="form-control register-control" /><br />
            <label>Ort: <span class="required">*</span></label>
            <asp:TextBox id="TextPlace" runat="server" required="required" CssClass="form-control register-control uc-all" /><br />
            <label>Medlemsnivå: <span class="required">*</span></label>
            <asp:DropDownList ID="DropCategory" runat="server" CssClass="form-control">

            </asp:DropDownList><br />
            <label>Kön: <span class="required">*</span></label><br />
            <asp:RadioButtonList ID="RadioGender" runat="server" CssClass="radio-inline">
                <asp:ListItem Value="Male" Selected="true">Man</asp:ListItem>
                <asp:ListItem Value="Female">Kvinna</asp:ListItem>
            </asp:RadioButtonList><br />
            <asp:Button ID="ButtonRegister" runat="server" Text="Registrera" CssClass="btn btn-primary btn-lg" /><br />
            <br /><asp:Panel ID="PanelResponse" runat="server" CssClass="alert alert-warning">
                <asp:Label ID="LabelResponse" runat="server" Text=""></asp:Label>
            </asp:Panel><asp:Panel ID="PanelSuccess" runat="server" CssClass="alert alert-success success-align">
                <asp:Label ID="LabelSuccess" runat="server" Text=""></asp:Label>
            </asp:Panel>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
</form>
</asp:Content>
