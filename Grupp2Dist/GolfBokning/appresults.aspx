<%@ Page Title="" Language="C#" MasterPageFile="~/headSite.Master" AutoEventWireup="true" CodeBehind="appresults.aspx.cs" Inherits="GolfBokning.appresults" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="formAppResults" runat="server">
       <div id="appResultsContainer">
           <div id="appResultsEntry">
               <asp:DropDownList ID="DropDownTourApp" runat="server"></asp:DropDownList><br /><br />
               <asp:RadioButtonList ID="rbtLstTee" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Text="62 Vit" Value="62 Vit"></asp:ListItem>
                        <asp:ListItem Selected="True" Text="57 Gul" Value="57 Gul"></asp:ListItem>
                        <asp:ListItem Text="54 Blå" Value="54 Blå"></asp:ListItem>
                        <asp:ListItem Text="50 Röd" Value="50 Röd"></asp:ListItem>
                    </asp:RadioButtonList><br />
               <asp:Label ID="lblHoleNrApp" runat="server" Text="Hål Nr: "></asp:Label><br />
               <asp:Label ID="lblPlayerPlaceApp" runat="server" Text="Nuvarande placering: "></asp:Label>
            <table id="resultsTable" runat="server" style="width: 100%;" visible="true" class="table table-bordered">
                <tr>
                    <%--<th>Hål Nr</th>--%>
                    <th><asp:Label CssClass="hcpIndexLabelApp" ID="Label21" runat="server" Text="HcpIndex"></asp:Label></th>
                    <th><asp:Label CssClass="hcpIndexLabelApp" ID="Label24" runat="server" Text="Par"></asp:Label></th>
                    <th><asp:Label CssClass="hcpIndexLabelApp" ID="Label27" runat="server" Text="Slag"></asp:Label></th>  
                    <th><asp:Label CssClass="hcpIndexLabelApp" ID="Label31" runat="server" Text="Erhållna slag"></asp:Label></th>
                    <th><asp:Label CssClass="hcpIndexLabelApp" ID="Label35" runat="server" Text="Netto"></asp:Label></th>
                    <th><asp:Label CssClass="hcpIndexLabelApp" ID="Label39" runat="server" Text="+/-"></asp:Label></th>      
                </tr>
                <tr>
                    <%--<td class="hcpIndexLabelApp" id="lblHoleNrApp1" runat="server"></td>--%>
                    <td class="hcpIndexLabelApp" id="lblHcpIndexApp" runat="server"></td>
                    <td class="hcpIndexLabelApp" id="lblParApp" runat="server"></td>
                    <td><asp:TextBox MaxLength="2" Width="20px" ID="TextBox1" runat="server"></asp:TextBox></td>   
                    <td class="hcpIndexLabelApp" id="lblPlayHcpApp1" runat="server"></td>
                    <td class="hcpIndexLabelApp" id="lblNettoApp1" runat="server"></td>
                    <td class="hcpIndexLabelApp" id="lblPlusMinus1" runat="server"></td>
                    <%--<asp:Label CssClass="hcpIndexLabelApp" ID="lblPlayHcpApp1" runat="server" Text=""></asp:Label>  --%>         
                </tr>
                <%--<tr>
                    <td>Hål 2</td>
                   <td><asp:Label CssClass="hcpIndexLabelApp" ID="Label1" runat="server" Text="14"></asp:Label></td>
                    <td><asp:Label CssClass="hcpIndexLabelApp" ID="Label19" runat="server" Text="4"></asp:Label></td>
                    <td><asp:TextBox MaxLength="2" Width="20px" ID="TextBox2" runat="server"></asp:TextBox></td>   
                    <td class="hcpIndexLabelApp" id="lblPlayHcpApp2" runat="server"></td>
                    <td class="hcpIndexLabelApp" id="lblNettoApp2" runat="server"></td>
                    <td class="hcpIndexLabelApp" id="lblPlusMinus2" runat="server"></td>
                </tr>
                <tr>
                    <td>Hål 3</td>
                    <td><asp:Label CssClass="hcpIndexLabelApp" ID="Label2" runat="server" Text="10"></asp:Label></td>
                    <td><asp:Label CssClass="hcpIndexLabelApp" ID="Label20" runat="server" Text="3"></asp:Label></td>
                    <td><asp:TextBox MaxLength="2" Width="20px" ID="TextBox3" runat="server"></asp:TextBox></td>   
                    <td class="hcpIndexLabelApp" id="lblPlayHcpApp3" runat="server"></td>
                    <td class="hcpIndexLabelApp" id="lblNettoApp3" runat="server"></td>
                    <td class="hcpIndexLabelApp" id="lblPlusMinus3" runat="server"></td>
                </tr>
                <tr>
                    <td>Hål 4</td>
                    <td><asp:Label CssClass="hcpIndexLabelApp" ID="Label3" runat="server" Text="2"></asp:Label></td>
                    <td><asp:Label CssClass="hcpIndexLabelApp" ID="Label30" runat="server" Text="4"></asp:Label></td>
                    <td><asp:TextBox MaxLength="2" Width="20px" ID="TextBox4" runat="server"></asp:TextBox></td>   
                    <td class="hcpIndexLabelApp" id="lblPlayHcpApp4" runat="server"></td>
                    <td class="hcpIndexLabelApp" id="lblNettoApp4" runat="server"></td>
                    <td class="hcpIndexLabelApp" id="lblPlusMinus4" runat="server"></td>  
                </tr>
                <tr>
                    <td>Hål 5</td>
                   <td><asp:Label CssClass="hcpIndexLabelApp" ID="Label4" runat="server" Text="4"></asp:Label></td>
                    <td><asp:Label CssClass="hcpIndexLabelApp" ID="Label34" runat="server" Text="4"></asp:Label></td>
                    <td><asp:TextBox MaxLength="2" Width="20px" ID="TextBox5" runat="server"></asp:TextBox></td>   
                    <td class="hcpIndexLabelApp" id="lblPlayHcpApp5" runat="server"></td>
                    <td class="hcpIndexLabelApp" id="lblNettoApp5" runat="server"></td>
                    <td class="hcpIndexLabelApp" id="lblPlusMinus5" runat="server"></td>
                </tr>
                <tr>
                    <td>Hål 6</td>
                   <td><asp:Label CssClass="hcpIndexLabelApp" ID="Label5" runat="server" Text="18"></asp:Label></td>
                    <td><asp:Label CssClass="hcpIndexLabelApp" ID="Label38" runat="server" Text="3"></asp:Label></td>
                    <td><asp:TextBox MaxLength="2" Width="20px" ID="TextBox6" runat="server"></asp:TextBox></td>   
                    <td class="hcpIndexLabelApp" id="lblPlayHcpApp6" runat="server"></td>
                    <td class="hcpIndexLabelApp" id="lblNettoApp6" runat="server"></td>
                    <td class="hcpIndexLabelApp" id="lblPlusMinus6" runat="server"></td>
                </tr>
                <tr>
                    <td>Hål 7</td>
                    <td><asp:Label CssClass="hcpIndexLabelApp" ID="Label6" runat="server" Text="8"></asp:Label></td>
                    <td><asp:Label CssClass="hcpIndexLabelApp" ID="Label42" runat="server" Text="4"></asp:Label></td>
                    <td><asp:TextBox MaxLength="2" Width="20px" ID="TextBox7" runat="server"></asp:TextBox></td>   
                    <td class="hcpIndexLabelApp" id="lblPlayHcpApp7" runat="server"></td>
                    <td class="hcpIndexLabelApp" id="lblNettoApp7" runat="server"></td>
                    <td class="hcpIndexLabelApp" id="lblPlusMinus7" runat="server"></td>
                </tr>
                <tr>
                    <td>Hål 8</td>
                    <td><asp:Label CssClass="hcpIndexLabelApp" ID="Label7" runat="server" Text="12"></asp:Label></td>
                    <td><asp:Label CssClass="hcpIndexLabelApp" ID="Label46" runat="server" Text="5"></asp:Label></td>
                    <td><asp:TextBox MaxLength="2" Width="20px" ID="TextBox8" runat="server"></asp:TextBox></td>   
                    <td class="hcpIndexLabelApp" id="lblPlayHcpApp8" runat="server"></td>
                    <td class="hcpIndexLabelApp" id="lblNettoApp8" runat="server"></td>
                    <td class="hcpIndexLabelApp" id="lblPlusMinus8" runat="server"></td>
                </tr>
                <tr>
                    <td>Hål 9</td>
                   <td><asp:Label CssClass="hcpIndexLabelApp" ID="Label8" runat="server" Text="6"></asp:Label></td>
                    <td><asp:Label CssClass="hcpIndexLabelApp" ID="Label50" runat="server" Text="4"></asp:Label></td>
                    <td><asp:TextBox MaxLength="2" Width="20px" ID="TextBox9" runat="server"></asp:TextBox></td>   
                    <td class="hcpIndexLabelApp" id="lblPlayHcpApp9" runat="server"></td>
                    <td class="hcpIndexLabelApp" id="lblNettoApp9" runat="server"></td>
                    <td class="hcpIndexLabelApp" id="lblPlusMinus9" runat="server"></td>
                </tr>
                <tr>
                    <td>Hål 10</td>
                   <td><asp:Label CssClass="hcpIndexLabelApp" ID="Label9" runat="server" Text="9"></asp:Label></td>
                    <td><asp:Label CssClass="hcpIndexLabelApp" ID="Label54" runat="server" Text="4"></asp:Label></td>
                    <td><asp:TextBox MaxLength="2" Width="20px" ID="TextBox10" runat="server"></asp:TextBox></td>   
                    <td class="hcpIndexLabelApp" id="lblPlayHcpApp10" runat="server"></td>
                    <td class="hcpIndexLabelApp" id="lblNettoApp10" runat="server"></td>
                    <td class="hcpIndexLabelApp" id="lblPlusMinus10" runat="server"></td>
                </tr>
                <tr>
                    <td>Hål 11</td>
                  <td><asp:Label CssClass="hcpIndexLabelApp" ID="Label10" runat="server" Text="13"></asp:Label></td>
                    <td><asp:Label CssClass="hcpIndexLabelApp" ID="Label58" runat="server" Text="4"></asp:Label></td>
                    <td><asp:TextBox MaxLength="2" Width="20px" ID="TextBox11" runat="server"></asp:TextBox></td>   
                    <td class="hcpIndexLabelApp" id="lblPlayHcpApp11" runat="server"></td>
                    <td class="hcpIndexLabelApp" id="lblNettoApp11" runat="server"></td>
                    <td class="hcpIndexLabelApp" id="lblPlusMinus11" runat="server"></td>
                </tr>
                <tr>
                    <td>Hål 12</td>
                    <td><asp:Label CssClass="hcpIndexLabelApp" ID="Label11" runat="server" Text="7"></asp:Label></td>
                    <td><asp:Label CssClass="hcpIndexLabelApp" ID="Label62" runat="server" Text="5"></asp:Label></td>
                    <td><asp:TextBox MaxLength="2" Width="20px" ID="TextBox12" runat="server"></asp:TextBox></td>   
                    <td class="hcpIndexLabelApp" id="lblPlayHcpApp12" runat="server"></td>
                    <td class="hcpIndexLabelApp" id="lblNettoApp12" runat="server"></td>
                    <td class="hcpIndexLabelApp" id="lblPlusMinus12" runat="server"></td> 
                </tr>
                <tr>
                    <td>Hål 13</td>
                    <td><asp:Label CssClass="hcpIndexLabelApp" ID="Label12" runat="server" Text="3"></asp:Label></td>
                    <td><asp:Label CssClass="hcpIndexLabelApp" ID="Label66" runat="server" Text="4"></asp:Label></td>
                    <td><asp:TextBox MaxLength="2" Width="20px" ID="TextBox13" runat="server"></asp:TextBox></td>   
                    <td class="hcpIndexLabelApp" id="lblPlayHcpApp13" runat="server"></td>
                    <td class="hcpIndexLabelApp" id="lblNettoApp13" runat="server"></td>
                    <td class="hcpIndexLabelApp" id="lblPlusMinus13" runat="server"></td>
                </tr>
                <tr>
                    <td>Hål 14</td>
                    <td><asp:Label CssClass="hcpIndexLabelApp" ID="Label13" runat="server" Text="17"></asp:Label></td>
                    <td><asp:Label CssClass="hcpIndexLabelApp" ID="Label70" runat="server" Text="3"></asp:Label></td>
                    <td><asp:TextBox MaxLength="2" Width="20px" ID="TextBox14" runat="server"></asp:TextBox></td>   
                    <td class="hcpIndexLabelApp" id="lblPlayHcpApp14" runat="server"></td>
                    <td class="hcpIndexLabelApp" id="lblNettoApp14" runat="server"></td>
                    <td class="hcpIndexLabelApp" id="lblPlusMinus14" runat="server"></td>
                </tr>
                <tr>
                    <td>Hål 15</td>
                   <td><asp:Label CssClass="hcpIndexLabelApp" ID="Label14" runat="server" Text="1"></asp:Label></td>
                    <td><asp:Label CssClass="hcpIndexLabelApp" ID="Label74" runat="server" Text="4"></asp:Label></td>
                    <td><asp:TextBox MaxLength="2" Width="20px" ID="TextBox15" runat="server"></asp:TextBox></td>   
                    <td class="hcpIndexLabelApp" id="lblPlayHcpApp15" runat="server"></td>
                    <td class="hcpIndexLabelApp" id="lblNettoApp15" runat="server"></td>
                    <td class="hcpIndexLabelApp" id="lblPlusMinus15" runat="server"></td> 
                </tr>
                <tr>
                    <td>Hål 16</td>
                    <td><asp:Label CssClass="hcpIndexLabelApp" ID="Label15" runat="server" Text="15"></asp:Label></td>
                    <td><asp:Label CssClass="hcpIndexLabelApp" ID="Label78" runat="server" Text="5"></asp:Label></td>
                    <td><asp:TextBox MaxLength="2" Width="20px" ID="TextBox16" runat="server"></asp:TextBox></td>   
                    <td class="hcpIndexLabelApp" id="lblPlayHcpApp16" runat="server"></td>
                    <td class="hcpIndexLabelApp" id="lblNettoApp16" runat="server"></td>
                    <td class="hcpIndexLabelApp" id="lblPlusMinus16" runat="server"></td>
                </tr>
                <tr>
                    <td>Hål 17</td>
                    <td><asp:Label CssClass="hcpIndexLabelApp" ID="Label16" runat="server" Text="11"></asp:Label></td>
                    <td><asp:Label CssClass="hcpIndexLabelApp" ID="Label82" runat="server" Text="3"></asp:Label></td>
                    <td><asp:TextBox MaxLength="2" Width="20px" ID="TextBox17" runat="server"></asp:TextBox></td>   
                    <td class="hcpIndexLabelApp" id="lblPlayHcpApp17" runat="server"></td>
                    <td class="hcpIndexLabelApp" id="lblNettoApp17" runat="server"></td>
                    <td class="hcpIndexLabelApp" id="lblPlusMinus17" runat="server"></td>  
                </tr>
                <tr>
                    <td>Hål 18</td>
                  <td><asp:Label CssClass="hcpIndexLabelApp" ID="Label17" runat="server" Text="5"></asp:Label></td>
                    <td><asp:Label CssClass="hcpIndexLabelApp" ID="Label86" runat="server" Text="4"></asp:Label></td>
                    <td><asp:TextBox MaxLength="2" Width="20px" ID="TextBox18" runat="server"></asp:TextBox></td>   
                    <td class="hcpIndexLabelApp" id="lblPlayHcpApp18" runat="server"></td>
                    <td class="hcpIndexLabelApp" id="lblNettoApp18" runat="server"></td>
                    <td class="hcpIndexLabelApp" id="lblPlusMinus18" runat="server"></td>
                </tr>--%>
            </table><br />
               
               <asp:Button ID="btnBack" runat="server" Text="Föregående Hål" OnClick="btnBack_Click" />
               <asp:Button ID="btnNext" runat="server" Text="Nästa Hål" OnClick="btnNext_Click" />
               <asp:Button ID="btnCalc" runat="server" Text="Uppdatera" OnClick="btnCalc_Click" />
           </div>
           <div id="appResultsScoreview">

           </div>
       </div>
    </form>
</asp:Content>