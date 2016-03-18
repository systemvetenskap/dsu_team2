<%@ Page Language="C#" MasterPageFile="~/headSite.Master" AutoEventWireup="True" CodeBehind="loginform.aspx.cs" Inherits="GolfBokning.loginform" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="StyleSheet.css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<h1>Logga in</h1>
<form id="formLogin" runat="server">
<asp:ScriptManager ID="ScriptManagerLogin" EnablePartialRendering="true" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanelLogin" runat="server">
        <ContentTemplate>
            <div class="loginForm">
                <label>E-mail: </label>
                <asp:TextBox runat="server" id="TextEmailLogin" CssClass="form-control" required="required" /><br />
                <label>Lösenord: </label>
                <asp:TextBox runat="server" id="TextPasswordLogin" CssClass="form-control" required="required" /><br />
                <asp:Button id="ButtonLogin" runat="server" Text="Logga in" CssClass="btn btn-primary btn-lg" /><br /><br />
                <asp:Label ID="LabelResponse" runat="server" Text=""></asp:Label>
            </div>
            <asp:Button ID="ButtonRegister" runat="server" Text="Registrera konto" CssClass="btn btn-default buttonRegister" />
        </ContentTemplate>
    </asp:UpdatePanel>
</form>
</asp:Content>