<%@ Page Language="C#" MasterPageFile="~/headSite.Master" AutoEventWireup="true" CodeBehind="cms.aspx.cs" Inherits="GolfBokning.cms" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<h1>Redigera hemsida</h1>
<form id="formAddCms" runat="server">
<asp:ScriptManager ID="ScriptManagerCms" EnablePartialRendering="true" runat="server"></asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanelCms" runat="server">
    <ContentTemplate>
        <asp:Label ID="LabelPage" runat="server" Text="Välj sida:"></asp:Label>
        <asp:DropDownList ID="DropDownListPage" runat="server" CssClass="form-control"></asp:DropDownList><br /><br />
        <asp:Panel ID="PanelSuccess" runat="server" CssClass="alert alert-success">
            <asp:Label ID="LabelSuccess" runat="server" Text=""></asp:Label>
        </asp:Panel>
        <asp:Panel ID="PanelUpdate" runat="server">
            <asp:Label ID="LabelWhatPage" runat="server" Text="Label"></asp:Label><br />
            <asp:Label ID="LabelImage" runat="server" Text="Bild"></asp:Label><br />
            <asp:Image ID="ImageExist" runat="server" /><br /><br />
            <asp:FileUpload ID="FileUpload1" runat="server" /><br />
            <asp:Label ID="LabelText" runat="server" Text="Text"></asp:Label><br />
            <asp:TextBox ID="TextBoxText" TextMode="multiline" Columns="50" Rows="5" CssClass="form-control" runat="server"></asp:TextBox><br />
            <asp:Button ID="ButtonSave" runat="server" Text="Spara" CssClass="btn btn-primary" /><br /><br />
        </asp:Panel>
        <br /><br /><br /><br /><br /><br />
    </ContentTemplate>
</asp:UpdatePanel>
</form>
</asp:Content>