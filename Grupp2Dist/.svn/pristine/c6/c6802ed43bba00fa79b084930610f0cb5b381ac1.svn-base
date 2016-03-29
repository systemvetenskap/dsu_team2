<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/headSite.Master" CodeBehind="myAccount.aspx.cs" Inherits="GolfBokning.myAccount" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Medlemsuppgifter</title>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/css/bootstrap.min.css" />
    <link rel="stylesheet" href="StyleSheet.css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="form1" runat="server">
        <!-- Container -->
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" EnablePartialRendering="true" />
        <script type='text/javascript'>
           
            //function RemoveTournament(id_tournament) {
            //    var id_member = $(".lbl-hid").html();
            //    PageMethods.RemoveTournamentBooking(id_tournament, id_member, Remove_Success, Remove_Fail);
            //}

            //function Remove_Success(msg) {
            //    alert("Avbokat tävling!");
            //    window.location = "minasidor.aspx";
            //}

            //function Remove_Fail(msg) {
            //    console.log(msg);
            //}
        </script>

        <div class="containerMinaSidor">
            <asp:Label ID="LabelHidden" runat="server" Text="" CssClass="lbl-hid"></asp:Label>
            <!-- main content -->
            <div class="mainContent">

                <h2>Medlemsuppgifter</h2>
                <asp:Panel ID="PanelSuccess" runat="server" CssClass="alert alert-success">
                    <asp:Label ID="LabelSuccess" runat="server" Text=""></asp:Label>
                </asp:Panel>
                <asp:Panel ID="PanelMySideUpdate" runat="server" CssClass="myChanges">
                    <label>Golf ID: </label><br />
                    <asp:TextBox ID="TextBoxGolfId" runat="server" Enabled="false" Text="" CssClass="form-control"></asp:TextBox><br />
                    <label>E-mail: </label>
                    <asp:TextBox ID="TextEmail" runat="server" required="required" Enabled="False" CssClass="form-control" /><br />
                    <label>Nytt lösenord: </label>
                    <asp:TextBox ID="TextPassword" runat="server" TextMode="Password" CssClass="form-control" /><br />
                    <label>Förnamn: </label>
                    <asp:TextBox ID="TextFirstName" runat="server" required="required" CssClass="form-control"  /><br />
                    <label>Efternamn: </label>
                    <asp:TextBox ID="TextLastName" runat="server" required="required" CssClass="form-control"/><br />
                    <label>Telefonnummer: </label>
                    <asp:TextBox ID="TextBoxPhone" runat="server" required="required" CssClass="form-control"/><br />
                    <label>Personnummer: (ex. 1970-01-01)</label>
                    <asp:TextBox ID="TextSSN" runat="server" required="required" Enabled="False" CssClass="form-control"/><br />
                    <label>Adress: </label>
                    <asp:TextBox ID="TextAddress" runat="server" required="required" CssClass="form-control"/><br />
                    <label>Postnummer: </label>
                    <asp:TextBox ID="TextZipCode" runat="server" MaxLength="9" required="required" CssClass="form-control"/><br />
                    <label>Ort: </label>
                    <asp:TextBox ID="TextPlace" runat="server" required="required" CssClass="form-control"/><br />
                    <label>Kön: </label>
                    <asp:RadioButtonList ID="RadioButtonGender" runat="server" Enabled="False">
                        <asp:ListItem Value="Male">Man</asp:ListItem>
                        <asp:ListItem Value="Female">Kvinna</asp:ListItem>
                    </asp:RadioButtonList>                                     
                    <asp:Label ID="LabelHcp" runat="server" Text="Handikap"></asp:Label>
                    <asp:TextBox ID="TextHcp" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox><br />                            
                    <asp:Button ID="ButtonMembUpdate" runat="server" Text="Uppdatera" CssClass="btn btn-primary" />
                </asp:Panel>
                <asp:Label ID="LabelTest" runat="server" Text=""></asp:Label>
            </div>
        </div>
    </form>
</asp:Content>
