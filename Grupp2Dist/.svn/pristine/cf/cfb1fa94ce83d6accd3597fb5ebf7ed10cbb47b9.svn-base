<%@ Page Title="" Language="C#" MasterPageFile="~/headSite.Master" AutoEventWireup="true" CodeBehind="addResults.aspx.cs" Inherits="GolfBokning.addResults" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="resultForm" runat="server">
        <div>
            <div>
            </div>
            <div>
                <asp:ListBox ID="ListBox2" CssClass="resultsLstBox2" runat="server"  Rows="10" OnSelectedIndexChanged="ListBox2_SelectedIndexChanged" AutoPostBack="true"></asp:ListBox>
                <asp:ListBox ID="ListBox1" CssClass="resultsLstBox1" runat="server"  Rows="10" Visible="false" AutoPostBack="True" OnSelectedIndexChanged="ListBox1_SelectedIndexChanged"></asp:ListBox>
                <div id="rbtLstHolder">
                    <asp:RadioButtonList ID="rbtLstTee" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="rbtLstTee_SelectedIndexChanged">
                        <asp:ListItem Text="62 Vit" Value="62 Vit"></asp:ListItem>
                        <asp:ListItem Text="57 Gul" Value="57 Gul"></asp:ListItem>
                        <asp:ListItem Text="54 Blå" Value="54 Blå"></asp:ListItem>
                        <asp:ListItem Text="50 Röd" Value="50 Röd"></asp:ListItem>
                    </asp:RadioButtonList> 
                </div>
                   
<%--                <input runat="server" id="searchMember" type="text" />
            <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>--%>
              <div id="resultsTableHolder">


            <table id="resultsTable" runat="server" style="width: 100%;" visible="true">
                <tr>
                    <td>hål 1</td>
                    <td><asp:TextBox ID="TextBox1" runat="server"></asp:TextBox></td>                 
                </tr>
                <tr>
                    <td>hål 2</td>
                    <td><asp:TextBox ID="TextBox2" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>hål 3</td>
                    <td><asp:TextBox ID="TextBox3" runat="server"></asp:TextBox></td>                    
                </tr>
                <tr>
                    <td>hål 4</td>
                    <td><asp:TextBox ID="TextBox4" runat="server"></asp:TextBox></td>                    
                </tr>
                <tr>
                    <td>hål 5</td>
                    <td><asp:TextBox ID="TextBox5" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>hål 6</td>
                    <td><asp:TextBox ID="TextBox6" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>hål 7</td>
                    <td><asp:TextBox ID="TextBox7" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>hål 8</td>
                    <td><asp:TextBox ID="TextBox8" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>hål 9</td>
                    <td><asp:TextBox ID="TextBox9" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>hål 10</td>
                    <td><asp:TextBox ID="TextBox10" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>hål 11</td>
                    <td><asp:TextBox ID="TextBox11" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>hål 12</td>
                    <td><asp:TextBox ID="TextBox12" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>hål 13</td>
                    <td><asp:TextBox ID="TextBox13" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>hål 14</td>
                    <td><asp:TextBox ID="TextBox14" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>hål 15</td>
                    <td><asp:TextBox ID="TextBox15" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>hål 16</td>
                    <td><asp:TextBox ID="TextBox16" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>hål 17</td>
                    <td><asp:TextBox ID="TextBox17" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>hål 18</td>
                    <td><asp:TextBox ID="TextBox18" runat="server"></asp:TextBox></td>
                </tr>
            </table>
        <asp:Button ID="Button1" runat="server" Text="Registrera slag" OnClick="Button1_Click" CssClass="btn btn-primary" UseSubmitBehavior="false"  /><br />
                  <asp:Label ID="lblConfirm" runat="server" Text="" Visible="false"></asp:Label>
             </div>
            </div>
        </div>
    </form>
</asp:Content>