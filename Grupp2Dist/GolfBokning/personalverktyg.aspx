<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/headSite.Master" CodeBehind="personalverktyg.aspx.cs" Inherits="GolfBokning.personalverktyg" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Personalverktyg</title>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/css/bootstrap.min.css" />
    <link rel="stylesheet" href="StyleSheet.css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="form1" runat="server">
        
        <div class="containerMinaSidor">
            <asp:Label ID="LabelHidden2" runat="server" Text="" CssClass="lbl-hid"></asp:Label>
            <!-- main content -->
            <div class="mainContent">

                <!--  Links -->
                <div class="minaSidorLinks clear">
                    <!-- Link 1-->
                    <div class="link link1">
                        <a href="tournament.aspx">
                            <img src="images\trophy.png" alt="Hantera tävlingar" />
                            <h2>Tävlingar</h2>
                        </a>
                    </div>
                    <!-- Link 2-->
                    <div class="link link2">
                        <a href="#">
                            <img src="images\statistik.png" alt="Statistik" />
                            <h2>Statistik</h2>
                        </a>
                    </div>
                    <!-- Link 3-->
                    <div class="link link3">
                        <a href="member.aspx">
                            <img src="images\members.png" alt="Redigera medlemmar" />
                            <h2>Medlemslista</h2>
                        </a>
                    </div>
                    <!-- Link 4-->
                    <div class="link link1">
                        <a href="news.aspx">
                            <img src="images\edit-news.png" alt="Redigera nyheter" />
                            <h2>Redigera nyheter</h2>
                        </a>
                    </div>
                </div>
            </div>
        </div>
    </form>
</asp:Content>
