<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/headSite.Master" CodeBehind="minasidor.aspx.cs" Inherits="GolfBokning.minasidor" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Mina sidor</title>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/css/bootstrap.min.css" />
    <link rel="stylesheet" href="StyleSheet.css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="form1" runat="server">
        <!-- Container -->
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" />
        <script type='text/javascript'>
            function GetMessage(id) {
                PageMethods.removeBooking(id, OnGetMessageSuccess, OnGetMessageFailure);
            }
            function OnGetMessageSuccess(result, userContext, methodName) {
                alert(result);
            }
            function OnGetMessageFailure(error, userContext, methodName) {
                alert(error.get_message());
            }
        </script>

        <div class="container">
            <!-- Local navigation -->
            <div class="localNavigation">
                <ul>
                    <li><a class="active" href="minasidor.aspx">Mina sidor</a></li>
                    <li><a href="#">Boka tid</a></li>
                    <li><a href="#">Tävlingar</a></li>
                    <li><a href="#">Medlemslista</a></li>
                    <li><a href="#">Staistik</a></li>
                    <li><a href="#">Redigera nyheter</a></li>
                    <li><a href="#">Mina uppgifter</a></li>
                </ul>
            </div>

            <!-- main content -->
            <div class="mainContent">
                <!--  Mina bokningar -->
                <div class="mina.bokningar">
                    <h2>Mina bokningar</h2>

                    <table class="tableMinaBokningar">
                        <thead>
                            <tr>
                                <th>Date</th>
                                <th>Time</th>
                                <th>Ändra</th>
                            </tr>
                        </thead>
                        <tbody id="tbTable" runat="server">
                        </tbody>
                    </table>
                    <asp:Button class="Button ButtonBokaTid" ID="ButtonBokaTid" runat="server" Text="Boka ny tid" />
                </div>
                <asp:Table ID="Table1" runat="server"></asp:Table>
                <asp:Panel ID="PanelMySideUpdate" runat="server" CssClass="myChanges">
                    <span onclick="makeBiggerRegister()">Redigera medlemar</span>
                    <br />
                    <label>E-mail: </label>
                    <asp:TextBox ID="TextEmail" runat="server" required="required" Enabled="False" /><br />
                    <label>Lösenord: </label>
                    <asp:TextBox ID="TextPassword" runat="server" required="required" Enabled="False" /><br />
                    <label>Förnamn: </label>
                    <asp:TextBox ID="TextFirstName" runat="server" required="required" /><br />
                    <label>Efternamn: </label>
                    <asp:TextBox ID="TextLastName" runat="server" required="required" /><br />
                    <label>Personnummer: (ex. 1970-01-01)</label>
                    <asp:TextBox ID="TextSSN" runat="server" required="required" Enabled="False" /><br />
                    <label>Adress: </label>
                    <asp:TextBox ID="TextAddress" runat="server" required="required" /><br />
                    <label>Postnummer: </label>
                    <asp:TextBox ID="TextZipCode" runat="server" MaxLength="9" required="required" /><br />
                    <label>Ort: </label>
                    <asp:TextBox ID="TextPlace" runat="server" required="required" /><br />
                    <label>Kön: </label>
                    <asp:RadioButtonList ID="RadioButtonGender" runat="server" Enabled="False">
                        <asp:ListItem Value="Male">Man</asp:ListItem>
                        <asp:ListItem Value="Female">Kvinna</asp:ListItem>
                    </asp:RadioButtonList>                                     
                    <asp:Label ID="LabelHcp" runat="server" Text="Handikap"></asp:Label>
                    <asp:TextBox ID="TextHcp" runat="server"></asp:TextBox><br />                            
                    <asp:Button ID="ButtonMembUpdate" runat="server" Text="Uppdatera" />
                </asp:Panel>

                <!--  Links -->
                <div class="minaSidorLinks clear">
                    <!-- Link 1-->
                    <div class="link link1">
                        <a href="#">
                            <img src="images\golf-test.jpg" alt="Tävlingar" />
                            <h2>Tävlingar</h2>
                        </a>
                    </div>
                    <!-- Link 2-->
                    <div class="link link2">
                        <a href="#">
                            <img src="images\golf-test.jpg" alt="Statistik" />
                            <h2>Statistik</h2>
                        </a>
                    </div>
                    <!-- Link 3-->
                    <div class="link link3">
                        <a href="#">
                            <img src="images\golf-test.jpg" alt="Medlemslista" />
                            <h2>Medlemslista</h2>
                        </a>
                    </div>
                    <!-- Link 4-->
                    <div class="link link1">
                        <a href="#">
                            <img src="images\golf-test.jpg" alt="Redigera nyheter" />
                            <h2>Redigera nyheter</h2>
                        </a>
                    </div>
                </div>
            </div>
        </div>
    </form>
</asp:Content>
