<%@ Page Language="C#" MasterPageFile="~/headSite.Master" AutoEventWireup="True" CodeBehind="loginform.aspx.cs" Inherits="GolfBokning.loginform" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="StyleSheet.css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<h1 class="h1_login">Logga in</h1>
<form id="formLogin" runat="server">
<asp:ScriptManager ID="ScriptManagerLogin" EnablePartialRendering="true" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanelLogin" runat="server">
        <ContentTemplate>
            <div class="loginForm">
                <label>E-mail: </label>
                <asp:TextBox runat="server" id="TextEmailLogin" CssClass="form-control" required="required" /><br />
                <label>Lösenord: </label>
                <asp:TextBox runat="server" id="TextPasswordLogin" CssClass="form-control" TextMode="Password" required="required" /><br />
                <asp:Button id="ButtonLogin" runat="server" Text="Logga in" CssClass="btn btn-primary btn-lg" /><br /><br />
                <asp:Panel ID="PanelResponse" runat="server" CssClass="alert alert-warning">
                    <asp:Label ID="LabelResponse" runat="server" Text="asd"></asp:Label>
                </asp:Panel>
            </div>
            <a href="register.aspx" class="btn btn-default buttonRegister">Bli medlem</a>
        </ContentTemplate>
    </asp:UpdatePanel>
</form>
</asp:Content>