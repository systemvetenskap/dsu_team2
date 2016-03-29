<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/headSite.Master" ValidateRequest="false" CodeBehind="sendMail.aspx.cs" Inherits="GolfBokning.sendMail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Send mail</title>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/css/bootstrap.min.css" />
    <link rel="stylesheet" href="StyleSheet.css" />
   
    <script src="//cdn.tinymce.com/4/tinymce.min.js"></script>
    <script>tinymce.init({ selector: 'textarea' });</script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="form1" runat="server">
        
    

        <div class="containerMinaSidor">
            <div class="mainContent">
                <div id="SendTill">
                    <div id="Meddelande">
                        <asp:Label ID="LabelMailSkickades" runat="server" Text="" CssClass="alert alert-success" Visible="false"></asp:Label><br />
                    </div>
                    <label>Till:</label><br />
                    <asp:DropDownList ID="DropDownListSendMail" runat="server" CssClass="dropDownMail form-control">
                        <asp:ListItem Value="1">Medlemmar</asp:ListItem>
                        <asp:ListItem Value="2">Personal</asp:ListItem>
                        <asp:ListItem Value="3">Alla</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div id="Headline">
                    <label>Rubrik: <span class="required">*</span></label><br />
                    <asp:TextBox id="TextMailRubrik" runat="server" required="required" CssClass="mailRubrik form-control" /><br />
                </div>
             
                <div id="Texteditor">
                    <textarea id="bodyHtml" runat="server" class="tinymce" rows="20" cols="120" ></textarea>
                </div>
                <div id="ButtonMail">
                    <asp:Button ID="ButtonSendMail" runat="server" Text="Skicka mailet" CssClass="btn btn-primary btn-md" OnClick="ButtonSendMail_Click" />
                </div>
            </div>
        </div>
    </form>
</asp:Content>