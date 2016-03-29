<%@ Page Title="" Language="C#" MasterPageFile="~/headSite.Master" AutoEventWireup="true" CodeBehind="tournamentdraw.aspx.cs" Inherits="GolfBokning.tournamentdraw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form runat="server">
        <h2>Lotta starttider på tävlingar</h2>
        <div class="drawdiv table">
        <div class="innerdrawdiv">
            <b>Välj tävling</b><br />
            <asp:DropDownList ID="tournament" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="tournament_SelectedIndexChanged">            
            </asp:DropDownList>
        </div>
        <div class="innerdrawdiv">
            <b>Antal lag/spelar per boll</b><br />
            <asp:DropDownList ID="nrplayerperball" runat="server" CssClass="form-control">
            <asp:ListItem Text="1"></asp:ListItem>
            <asp:ListItem Text="2"></asp:ListItem>
            <asp:ListItem Text="3"></asp:ListItem>
            <asp:ListItem Text="4"></asp:ListItem>
        </asp:DropDownList>
        </div>
        <div class="innerdrawdiv clearfix">
            <b>Minuter mellan bollar</b><br />
             <asp:DropDownList ID="time" runat="server" CssClass="form-control">
            <asp:ListItem Text="10"></asp:ListItem>
            <asp:ListItem Text="20"></asp:ListItem>
            <asp:ListItem Text="30"></asp:ListItem>
            </asp:DropDownList>
        </div>
        </div>
        
        <asp:Button ID="btnShuffle" runat="server" Text="Slumpa" CssClass="btn btn-primary" OnClick="btnShuffle_Click" />
        <br />
        <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered"></asp:GridView>
    </form>
    
</asp:Content>
