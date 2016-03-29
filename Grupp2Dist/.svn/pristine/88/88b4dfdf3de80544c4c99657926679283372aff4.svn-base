<%@ Page Language="C#" MasterPageFile="~/headSite.Master" AutoEventWireup="true" CodeBehind="news.aspx.cs" Inherits="GolfBokning.news" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<form id="formAddNews" runat="server">
<asp:ScriptManager ID="ScriptManagerLogin" EnablePartialRendering="true" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanelLogin" runat="server">
        <ContentTemplate>
        <h1>Nyheter</h1>
            <div class="news">
                <asp:Label ID="LabelDrop" runat="server" Text="Välj artikel"></asp:Label><br />
                <asp:DropDownList ID="DropDownNews" runat="server" CssClass="dropDownNews form-control"></asp:DropDownList><br />
                <asp:LinkButton ID="ButtonRemoveNews" runat="server" Text="Ta bort" CssClass="btn btn-danger" />
                <asp:LinkButton ID="ButtonCreateNew" runat="server" Text="Skapa ny" CssClass="btn btn-success createNewNews" /><br /><br />
                <asp:Panel ID="PanelRemove" runat="server" CssClass="alert alert-success" >
                    <asp:Label ID="LabelRemove" runat="server" Text="Borttagen"></asp:Label>
                </asp:Panel>
                <asp:Label ID="LabelH2" runat="server" Text="<h2>Skapa nyhet</h2>"></asp:Label><br />
                <asp:Label ID="LabelHeading" runat="server" Text="Rubrik"></asp:Label><br />
                <asp:TextBox ID="TextBoxHeading" runat="server" CssClass="form-control" required="required"></asp:TextBox><br />
                <asp:Label ID="LabelSmallHeading" runat="server" Text="Underrubrik"></asp:Label><br />
                <asp:TextBox ID="TextBoxSmallHeading" runat="server" CssClass="form-control" required="required"></asp:TextBox><br />
                <asp:Label ID="LabelText" runat="server" Text="Brödtext"></asp:Label><br /> 
                <asp:TextBox ID="TextBoxText" runat="server" TextMode="multiline" Columns="50" Rows="5" CssClass="form-control" required="required"></asp:TextBox><br /><br />
                <asp:Label ID="LabelImage" runat="server" Text="Ladda upp bild (URL)"></asp:Label><br />
                <asp:Image ID="ImageExist" runat="server" /><br />
                <asp:TextBox ID="TextBoxImageUrl" runat="server" required="required" CssClass="form-control"></asp:TextBox><br />
                <asp:Label ID="LabelImageText" runat="server" Text="Bildtext"></asp:Label><br />
                <asp:TextBox ID="TextBoxImageText" runat="server" CssClass="form-control"></asp:TextBox><br /><br />
                <asp:Button ID="ButtonUpdate" runat="server" Text="Uppdatera" CssClass="btn btn-primary btn-lg" />
                <asp:Button ID="ButtonCreate" runat="server" Text="Skapa" CssClass="btn btn-primary btn-lg"/><br /><br />
                <asp:Panel ID="PanelSuccess" runat="server" CssClass="alert alert-success">
                    <asp:Label ID="LabelSuccess" runat="server" Text=""></asp:Label>
                </asp:Panel>
                <asp:Panel ID="PanelResponse" runat="server" CssClass="alert alert-warning">
                    <asp:Label ID="LabelResponse" runat="server" Text=""></asp:Label>
                </asp:Panel>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</form>
</asp:Content>