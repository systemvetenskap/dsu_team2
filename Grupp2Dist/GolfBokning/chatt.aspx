<%@ Page Title="" Language="C#" MasterPageFile="~/headSite.Master" AutoEventWireup="true" CodeBehind="chatt.aspx.cs" Inherits="GolfBokning.chatt" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  
    <form id="idFormChat" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" />
       
        <script>
            function addChat() {
                var txt = $('#<%=txtAddMemb.ClientID%>').val();
                PageMethods.startChatt(GetCookieID(), txt, OnGetMessageSuccess, OnGetMessageFailure);
                //justOkeay
            }
            function OnGetMessageSuccess(result, userContext, methodName) {
                if (result == "OK")
                {
                    location.reload();

                }
                else
                {
                    alert(result);
                }
                
            }
            function OnGetMessageFailure(error, userContext, methodName) {
                alert(error);
            }
            function justOkeay(result, userContext, methodName) {
               
            }
            function sendMessage() {
                var chatId = $('#<%=lblChattID.ClientID%>').text();///text();
                var txt = $('#<%=txtChattMessage.ClientID%>').val();
                console.log(GetCookieID() + "  a  " + chatId + " a " + txt);
                PageMethods.messSend(GetCookieID(), chatId, txt, justOkeay, OnGetMessageFailure);
            }
            function getMessage() {
                $('#<%=chaF.ClientID%> tr:last').after('<tr><td>adl</td></tr>');
                $('#<%=chaF.ClientID%>').animate({
                    scrollTop: $('#chatF').get(0).scrollHeight
                }, 1500);
            } //
            $(document).ready(function () {

                $("#<%=chaF.ClientID%>").html("");
                $("#<%=lbLastSend.ClientID%>").html("0");

                askForMessage();
                setInterval(function () { askForMessage(); }, 3000);
            });

            function askForMessage() {
                var max = 0;
                $('.chattro').each(function () {
                    max = Math.max(this.id, max);
                });

                console.log(max + " asd ");
                var chatId = $('#<%=lblChattID.ClientID%>').text();///text();
                //var lastTime = $('#<%=lbLastSend.ClientID%>').text();///text();
                if (chatId.length > 0) {
                    PageMethods.getMessage(chatId, max, inputAllMessage, OnGetMessageFailure);
                }

            }
        
            function inputAllMessage(inHtml) {
                //

                document.getElementById("<%=chaF.ClientID%>").innerHTML += inHtml;
            }
        </script>
        <div class="chatContainer">
            <div class="chattPers">
                 <div class="chattNew">
                    <label id="lblChattrum">Lägg till ny chatt:</label><br />
                    <asp:TextBox ID="txtAddMemb" placeholder="Skriv medlems ID" runat="server" ></asp:TextBox> 
                    <button id="startChat" onclick="addChat(); return false;" class="btn btn-primary btn-md">Lägg till</button> 
                </div>

                <div id="Chattrum">
                    <asp:ListBox ID="liBox" runat="server" CssClass="form-control chattuse" AutoPostBack="true"></asp:ListBox>
                </div>

               <div class="bottomChat">
                    <asp:TextBox runat="server" ID="txtChattMessage" CssClass="chattMess" TextMode="multiline" width="100%" Height="120px"></asp:TextBox>
                    <button onclick="sendMessage(); return false;" id="buttonSendChat" class="btn btn-primary btn-md new-chat">Sänd</button>
                </div>

            </div>
            <div class="chattBox">

                <div class="chattFlow" id="chatF">
                    <table id="chaF" class="chattLines" runat="server" >
                        <tr>
                       
                        </tr>
                    </table>
                </div>
                <%--<div class="bottomChat">
                    <asp:TextBox runat="server" ID="txtChattMessage" CssClass="chattMess" TextMode="multiline" Columns="50" Rows="6"></asp:TextBox>
                    <button onclick="sendMessage(); return false;" id="buttonSendChat" class="btn btn-primary btn-md new-chat">Sänd</button>
                </div>--%>
            </div>

           <%-- <div class="chattNew">
                <asp:TextBox ID="txtAddMemb" runat="server"></asp:TextBox> 
                <button id="startChat" onclick="addChat(); return false;" class="btn btn-primary btn-md">Skapa ny chatt</button>        
              
            </div>--%>
              <asp:Label ID="lblChattID" runat="server" style="visibility:hidden;"></asp:Label>
              <asp:Label ID="lbLastSend" runat="server"  style="visibility:hidden;"></asp:Label>
        </div>
    </form>
</asp:Content>
