﻿<%@ Page Title="" Language="C#" MasterPageFile="~/headSite.Master" AutoEventWireup="true" CodeBehind="startside.aspx.cs" Inherits="GolfBokning.startside" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>denna sida</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<form id="startpageForm" runat="server">
    <asp:ScriptManager ID="ScriptManagerStartpage" EnablePartialRendering="true" runat="server"></asp:ScriptManager>
     <asp:UpdatePanel ID="UpdatePanelRegister" runat="server">
        <ContentTemplate>
            <asp:Panel ID="PanelStartLeft" runat="server">
                <asp:Image ID="ImageCMS" runat="server" />
                <asp:Label ID="LabelCMS" runat="server" Text="Label"></asp:Label>
                <asp:Panel ID="PanelButtons" runat="server">
                    <asp:HyperLink ID="HyperLinkRegister" runat="server">
                        <asp:Panel ID="PanelRegister" runat="server" CssClass="button_block">
                            <asp:Label ID="LabelRegisterIcon" runat="server" Text="<span class='glyphicon glyphicon-user'></span>" CssClass="button_icon"></asp:Label>
                            <asp:Label ID="LabelRegisterText" runat="server" Text="Registrera" CssClass="button_text"></asp:Label>
                        </asp:Panel>
                    </asp:HyperLink>
                   <%-- <asp:HyperLink ID="HyperLinkAbout" runat="server">
                        <asp:Panel ID="PanelAbout" runat="server" CssClass="button_block">
                            <asp:Label ID="LabelAboutIcon" runat="server" Text="<span class='glyphicon glyphicon-map-marker'></span>" CssClass="button_icon"></asp:Label>
                            <asp:Label ID="LabelAboutText" runat="server" Text="Om klubben" CssClass="button_text"></asp:Label>
                        </asp:Panel>
                    </asp:HyperLink>
                    <asp:HyperLink ID="HyperLink1" runat="server">
                        <asp:Panel ID="PanelCourse" runat="server" CssClass="button_block">
                            <asp:Label ID="LabelCourseIcon" runat="server" Text="<span class='glyphicon glyphicon-tree-conifer'></span>" CssClass="button_icon"></asp:Label>
                            <asp:Label ID="LabelCourseText" runat="server" Text="Baninformation" CssClass="button_text"></asp:Label>
                        </asp:Panel>
                    </asp:HyperLink>--%>
                </asp:Panel>
            </asp:Panel>
            <asp:Panel ID="PanelStartRight" runat="server">
                <asp:Label ID="Label" runat="server" Text=""></asp:Label>
                <asp:Panel ID="PanelNews" runat="server"></asp:Panel>
                <asp:Button ID="ButtonMoreNews" runat="server" Text="Nyhetsarkiv" CssClass="btn btn-default"/><br /><br />
            </asp:Panel>
        </ContentTemplate>
     </asp:UpdatePanel>
</form>
</asp:Content>
