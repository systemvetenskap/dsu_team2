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
                <%--<div id="rbtLstHolder">
                    <asp:RadioButtonList ID="rbtLstTee" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="rbtLstTee_SelectedIndexChanged">
                        <asp:ListItem Text="62 Vit" Value="62 Vit"></asp:ListItem>
                        <asp:ListItem Text="57 Gul" Value="57 Gul"></asp:ListItem>
                        <asp:ListItem Text="54 Blå" Value="54 Blå"></asp:ListItem>
                        <asp:ListItem Text="50 Röd" Value="50 Röd"></asp:ListItem>
                    </asp:RadioButtonList> 
                </div>--%>
                   
<%--                <input runat="server" id="searchMember" type="text" />
            <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>--%>
                <div id="tableTeamHolder" runat="server">
                    <table id="tableTeam" runat="server" style="width: 100%;" visible="true" class="table table-bordered">
                        <tr style="visibility:visible">
                            <th>Hål Nr</th>
                            <th id="player1lbl" runat="server">Spelare 1</th>
                            <th id="player2lbl" runat="server">Spelare 2</th>
                            <th id="player3lbl" runat="server">Spelare 3</th>
                            <th id="player4lbl" runat="server">Spelare 4</th>
                        </tr>
                        <tr>
                            <td>hål 1</td>
                            <td><asp:TextBox ID="player1h1" runat="server"></asp:TextBox></td>
                            <td><asp:TextBox ID="player2h1" runat="server"></asp:TextBox></td>
                            <td><asp:TextBox ID="player3h1" runat="server"></asp:TextBox></td>
                            <td><asp:TextBox ID="player4h1" runat="server"></asp:TextBox></td>                 
                        </tr>
                        <tr>
                            <td>hål 2</td>
                            <td><asp:TextBox ID="player1h2" runat="server"></asp:TextBox></td>
                            <td><asp:TextBox ID="player2h2" runat="server"></asp:TextBox></td>
                            <td><asp:TextBox ID="player3h2" runat="server"></asp:TextBox></td>
                            <td><asp:TextBox ID="player4h2" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>hål 3</td>
                            <td><asp:TextBox ID="player1h3" runat="server"></asp:TextBox></td>     
                            <td><asp:TextBox ID="player2h3" runat="server"></asp:TextBox></td>
                            <td><asp:TextBox ID="player3h3" runat="server"></asp:TextBox></td>
                            <td><asp:TextBox ID="player4h3" runat="server"></asp:TextBox></td>               
                        </tr>
                        <tr>
                            <td>hål 4</td>
                            <td><asp:TextBox ID="player1h4" runat="server"></asp:TextBox></td>    
                            <td><asp:TextBox ID="player2h4" runat="server"></asp:TextBox></td>
                            <td><asp:TextBox ID="player3h4" runat="server"></asp:TextBox></td>
                            <td><asp:TextBox ID="player4h4" runat="server"></asp:TextBox></td>                
                        </tr>
                        <tr>
                            <td>hål 5</td>
                            <td><asp:TextBox ID="player1h5" runat="server"></asp:TextBox></td>
                            <td><asp:TextBox ID="player2h5" runat="server"></asp:TextBox></td>
                            <td><asp:TextBox ID="player3h5" runat="server"></asp:TextBox></td>
                            <td><asp:TextBox ID="player4h5" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>hål 6</td>
                            <td><asp:TextBox ID="player1h6" runat="server"></asp:TextBox></td>
                            <td><asp:TextBox ID="player2h6" runat="server"></asp:TextBox></td>
                            <td><asp:TextBox ID="player3h6" runat="server"></asp:TextBox></td>
                            <td><asp:TextBox ID="player4h6" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>hål 7</td>
                            <td><asp:TextBox ID="player1h7" runat="server"></asp:TextBox></td>
                            <td><asp:TextBox ID="player2h7" runat="server"></asp:TextBox></td>
                            <td><asp:TextBox ID="player3h7" runat="server"></asp:TextBox></td>
                            <td><asp:TextBox ID="player4h7" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>hål 8</td>
                            <td><asp:TextBox ID="player1h8" runat="server"></asp:TextBox></td>
                            <td><asp:TextBox ID="player2h8" runat="server"></asp:TextBox></td>
                            <td><asp:TextBox ID="player3h8" runat="server"></asp:TextBox></td>
                            <td><asp:TextBox ID="player4h8" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>hål 9</td>
                            <td><asp:TextBox ID="player1h9" runat="server"></asp:TextBox></td>
                            <td><asp:TextBox ID="player2h9" runat="server"></asp:TextBox></td>
                            <td><asp:TextBox ID="player3h9" runat="server"></asp:TextBox></td>
                            <td><asp:TextBox ID="player4h9" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>hål 10</td>
                            <td><asp:TextBox ID="player1h10" runat="server"></asp:TextBox></td>
                            <td><asp:TextBox ID="player2h10" runat="server"></asp:TextBox></td>
                            <td><asp:TextBox ID="player3h10" runat="server"></asp:TextBox></td>
                            <td><asp:TextBox ID="player4h10" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>hål 11</td>
                            <td><asp:TextBox ID="player1h11" runat="server"></asp:TextBox></td>
                            <td><asp:TextBox ID="player2h11" runat="server"></asp:TextBox></td>
                            <td><asp:TextBox ID="player3h11" runat="server"></asp:TextBox></td>
                            <td><asp:TextBox ID="player4h11" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>hål 12</td>
                            <td><asp:TextBox ID="player1h12" runat="server"></asp:TextBox></td>
                            <td><asp:TextBox ID="player2h12" runat="server"></asp:TextBox></td>
                            <td><asp:TextBox ID="player3h12" runat="server"></asp:TextBox></td>
                            <td><asp:TextBox ID="player4h12" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>hål 13</td>
                            <td><asp:TextBox ID="player1h13" runat="server"></asp:TextBox></td>
                            <td><asp:TextBox ID="player2h13" runat="server"></asp:TextBox></td>
                            <td><asp:TextBox ID="player3h13" runat="server"></asp:TextBox></td>
                            <td><asp:TextBox ID="player4h13" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>hål 14</td>
                            <td><asp:TextBox ID="player1h14" runat="server"></asp:TextBox></td>
                            <td><asp:TextBox ID="player2h14" runat="server"></asp:TextBox></td>
                            <td><asp:TextBox ID="player3h14" runat="server"></asp:TextBox></td>
                            <td><asp:TextBox ID="player4h14" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>hål 15</td>
                            <td><asp:TextBox ID="player1h15" runat="server"></asp:TextBox></td>
                            <td><asp:TextBox ID="player2h15" runat="server"></asp:TextBox></td>
                            <td><asp:TextBox ID="player3h15" runat="server"></asp:TextBox></td>
                            <td><asp:TextBox ID="player4h15" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>hål 16</td>
                            <td><asp:TextBox ID="player1h16" runat="server"></asp:TextBox></td>
                            <td><asp:TextBox ID="player2h16" runat="server"></asp:TextBox></td>
                            <td><asp:TextBox ID="player3h16" runat="server"></asp:TextBox></td>
                            <td><asp:TextBox ID="player4h16" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>hål 17</td>
                            <td><asp:TextBox ID="player1h17" runat="server"></asp:TextBox></td>
                            <td><asp:TextBox ID="player2h17" runat="server"></asp:TextBox></td>
                            <td><asp:TextBox ID="player3h17" runat="server"></asp:TextBox></td>
                            <td><asp:TextBox ID="player4h17" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>hål 18</td>
                            <td><asp:TextBox ID="player1h18" runat="server"></asp:TextBox></td>
                            <td><asp:TextBox ID="player2h18" runat="server"></asp:TextBox></td>
                            <td><asp:TextBox ID="player3h18" runat="server"></asp:TextBox></td>
                            <td><asp:TextBox ID="player4h18" runat="server"></asp:TextBox></td>
                        </tr>
                    </table>
                    <asp:Button ID="btnRegTeamResults" runat="server" Text="Registrera" OnClick="btnRegTeamResults_Click" />
                </div>
              <div id="resultsTableHolder" runat="server">


            <table id="resultsTable" runat="server" style="width: 100%;" visible="true" class="table table-bordered">
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