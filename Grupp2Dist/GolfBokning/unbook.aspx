﻿<%@ Page Title="" Language="C#" MasterPageFile="~/headSite.Master" AutoEventWireup="true" CodeBehind="unbook.aspx.cs" Inherits="GolfBokning.unbook" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    



    <form runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" />
        <script type='text/javascript'>
            $(document).ready(function () {
                var id_member_cookie = GetCookieID();
                // Handler for .ready() called.
                $('#bookingDetail').modal({ backdrop: 'static', keyboard: false });
                $('#bookingDetail').modal('hide');
                $('#bookingDetail').on('hidden.bs.modal', function () {
                    $('#<%=listBookingmany.ClientID %>').empty();
                   
                    $('#ContentPlaceHolder1_TextBoxSearch>').val('');
                });
                //addPer
                document.getElementById("<%=addPer.ClientID %>").style.visibility = 'hidden';               
                document.getElementById("bookMany").style.visibility = 'hidden';
                document.getElementById("makeBooking").style.visibility = 'hidden';
                
            });

           

            function GetMessage(cellID, date) {
                //alert(cellID + "  " + date.substring(0, 10));
                PageMethods.makeBoking(cellID, id_member_cookie, date, null, false, OnGetMessageSuccess, OnGetMessageFailure);
            }
            function BookMany(cellID, date)
            {
                var many = "";
                console.log("aadasf");
                $("#<%=listBookingmany.ClientID %> > option").each(function () {
                    alert("aa");
                    many += this.value + ";";
                });
                console.log(many + "  aaa ");
                PageMethods.makeBoking(cellID, id_member_cookie, date, many, true, OnGetMessageSuccess, OnGetMessageFailure);
            }
            function OnGetMessageSuccess(result, userContext, methodName) {
                $('#<%=listBookingmany.ClientID %>').empty();
                alert(result);
                location.reload();
            }
            function OnGetMessageFailure(error, userContext, methodName) {
                alert(error.get_message());

            }
          <%--  $('#bookingDetail').on('hidden.bs.modal', function () {
               
            })--%>
           

            function addValToList()
            {
                //alert($('#<%=listBookingmany.ClientID %>').children().size());

                if ($('#<%=listBookingmany.ClientID %>').children().size() > 3)
                {
                    alert("Max 4:a åt gången");
                    return;
                }
                
                var str = $("#<%= TextBoxSearch.ClientID %>").val();
                
                var regExp = /\(([^)]+)\)/;
                var matches = regExp.exec(str);
                var exists = false;
                $('#<%=listBookingmany.ClientID %>  option').each(function () {
                    if (this.value == matches[1]) {
                        exists = true;
                    }
                });

                if (!exists)
                {
                    $('#<%=listBookingmany.ClientID %>').append('<option value="' + matches[1]  + '">' + str + '</option>'); // adds item 5 at the end
                   
                }
                else
                {
                    
                }
                $('#ContentPlaceHolder1_TextBoxSearch').val('');
                console.log(matches[1]);
                if ($('#<%=listBookingmany.ClientID %>').children().size() > 3)
                {
                    document.getElementById("makeBooking").style.visibility = 'hidden';
                }
                else
                {
                    document.getElementById("makeBooking").style.visibility = 'visible';
                }
            }
            function removeFromList(){
               
                var sqlID = ($("#<%=listBookingmany.ClientID %> option:selected").val());
                //fillListbox
                $("#<%=listBookingmany.ClientID %> option:selected").remove();
                PageMethods.deletePass(sqlID, showMem, OnGetMessageFailure);
            }       
            function showMem(result, userContext, methodName)
            {
                alert("Försvann: " + result);
                location.reload();
            }
            


        </script>

        <div class="modal fade" id="bookingDetail" role="dialog">
            <div class="modal-dialog modal-lg">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title">Boka tid</h4>
                    </div>
                    <div class="modal-body">
                        <h5>Vill du boka tiden: </h5>
                        <asp:Label ID="lblBookingDetailDateTime" runat="server" Text="Date/Time" class="lblBookingDetailClass" data-provide="typeahead"></asp:Label><br />
                        <asp:TextBox ID="TextBoxSearch" runat="server" AutoPostBack="false" CssClass="form-control" />
                        <asp:Button ID="addPer" onclientclick="addValToList(); return false;" CssClass="btn btn-default" data-dismiss="static" Text="Lägg till spelare" runat="server"/>
                        <asp:Button ID="remPer" onclientclick="removeFromList(); return false;" CssClass="btn btn-default" data-dismiss="static" Text="Avboka tider" runat="server"/>
                       
                         <br />
                        <br />
                        <asp:ListBox ID="listBookingmany" runat="server" CssClass="form-control"></asp:ListBox>


                    </div>
                    <div class="modal-footer">
                        <button id="bookMany" type="button" class="btn btn-default" data-dismiss="modal" >Boka flera</button>
                        <button id="makeBooking" type="button" class="btn btn-default" data-dismiss="modal" >Boka tid</button>
                        <asp:Button type="button" class="btn btn-default" data-dismiss="modal" Text="Close" runat="server"/>
                    </div>
                </div>
            </div>
        </div>
        <!-- </div> -->
        <div class:"UnbookCalendar">            
            <asp:Calendar ID="table_calender" runat="server" BackColor="White" BorderColor="#3366CC" BorderWidth="1px" CellPadding="1" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" ForeColor="#003399" Height="200px" OnSelectionChanged="table_calender_SelectionChanged" Width="220px">
                <DayHeaderStyle BackColor="#99CCCC" ForeColor="#336666" Height="1px" />
                <NextPrevStyle Font-Size="8pt" ForeColor="#CCCCFF" />
                <OtherMonthDayStyle ForeColor="#999999" />
                <SelectedDayStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                <SelectorStyle BackColor="#99CCCC" ForeColor="#336666" />
                <TitleStyle BackColor="#003399" BorderColor="#3366CC" BorderWidth="1px" Font-Bold="True" Font-Size="10pt" ForeColor="#CCCCFF" Height="25px" />
                <TodayDayStyle BackColor="#99CCCC" ForeColor="White" />
                <WeekendDayStyle BackColor="#CCCCFF" />
            </asp:Calendar>
        </div>

    </form>

    <div>
        <div id="visaDatumDiv" class="visaDatumDivClass" runat="server">
            <asp:Label ID="lblVisaDatum" runat="server" Text="" class="lblVisaDatumClass"></asp:Label>
        </div>
        <table id="bookingtable" style="height: auto;" runat="server" class="table table-bordered table-striped">
            <thead>
                <tr>
                    <th>05</th>
                    <th>06</th>
                    <th>07</th>
                    <th>08</th>
                    <th>09</th>
                    <th>10</th>
                    <th>11</th>
                    <th>12</th>
                    <th>13</th>
                    <th>14</th>
                    <th>15</th>
                    <th>16</th>
                    <th>17</th>
                    <th>18</th>
                    <th>19</th>
                </tr>
            </thead>
            <tr runat="server">
                <td class="auto-style9" id="cell0500" runat="server" onclick="myFunction()">
                    <p class="time">:00</p>
                    <asp:Label ID="lbl0500" runat="server" Text="Hcp" class="lblHandicap"></asp:Label>
                    <br />
                    <br />
                    <p class="first players" id="p10500" runat="server"></p>
                    <p class="players" id="p20500" runat="server"></p>
                    <p class="players" id="p30500" runat="server"></p>
                    <p class="last players" id="p40500" runat="server"></p>
                </td>
                <td class="auto-style9" id="cell0600" runat="server">
                    <p class="time">:00</p>
                    <asp:Label ID="lbl0600" runat="server" Text="Hcp" class="lblHandicap"></asp:Label>
                    <br />
                    <br />
                    <p class="first players" id="p10600" runat="server"></p>
                    <p class="players" id="p20600" runat="server"></p>
                    <p class="players" id="p30600" runat="server"></p>
                    <p class="last players" id="p40600" runat="server"></p>
                </td>
                <td class="auto-style9" id="cell0700" runat="server">
                    <p class="time">:00</p>
                    <asp:Label ID="lbl0700" runat="server" Text="Hcp" class="lblHandicap"></asp:Label>
                    <br />
                    <br />
                    <p class="first players" id="p10700" runat="server"></p>
                    <p class="players" id="p20700" runat="server"></p>
                    <p class="players" id="p30700" runat="server"></p>
                    <p class="last players" id="p40700" runat="server"></p>
                </td>
                <td class="auto-style9" id="cell0800" runat="server">
                    <p class="time">:00</p>
                    <asp:Label ID="lbl0800" runat="server" Text="Hcp" class="lblHandicap"></asp:Label>
                    <br />
                    <br />
                    <p class="first players" id="p10800" runat="server"></p>
                    <p class="players" id="p20800" runat="server"></p>
                    <p class="players" id="p30800" runat="server"></p>
                    <p class="last players" id="p40800" runat="server"></p>
                </td>
                <td class="auto-style9" id="cell0900" runat="server">
                    <p class="time">:00</p>
                    <asp:Label ID="lbl0900" runat="server" Text="Hcp" class="lblHandicap"></asp:Label>
                    <br />
                    <br />
                    <p class="first players" id="p10900" runat="server"></p>
                    <p class="players" id="p20900" runat="server"></p>
                    <p class="players" id="p30900" runat="server"></p>
                    <p class="last players" id="p40900" runat="server"></p>
                </td>
                <td class="auto-style9" id="cell1000" runat="server">

                    <p class="time">:00</p>
                    <asp:Label ID="lbl1000" runat="server" Text="Hcp" class="lblHandicap"></asp:Label>
                    <br />
                    <br />
                    <p class="first players" id="p11000" runat="server"></p>
                    <p class="players" id="p21000" runat="server"></p>
                    <p class="players" id="p31000" runat="server"></p>
                    <p class="last players" id="p41000" runat="server"></p>
                </td>
                <td class="auto-style9" id="cell1100" runat="server">

                    <p class="time">:00</p>
                    <asp:Label ID="lbl1100" runat="server" Text="Hcp" class="lblHandicap"></asp:Label>
                    <br />
                    <br />
                    <p class="first players" id="p11100" runat="server"></p>
                    <p class="players" id="p21100" runat="server"></p>
                    <p class="players" id="p31100" runat="server"></p>
                    <p class="last players" id="p41100" runat="server"></p>
                </td>
                <td class="auto-style9" id="cell1200" runat="server">

                    <p class="time">:00</p>
                    <asp:Label ID="lbl1200" runat="server" Text="Hcp" class="lblHandicap"></asp:Label>
                    <br />
                    <br />
                    <p class="first players" id="p11200" runat="server"></p>
                    <p class="players" id="p21200" runat="server"></p>
                    <p class="players" id="p31200" runat="server"></p>
                    <p class="last players" id="p41200" runat="server"></p>
                </td>
                <td class="auto-style9" id="cell1300" runat="server">

                    <p class="time">:00</p>
                    <asp:Label ID="lbl1300" runat="server" Text="Hcp" class="lblHandicap"></asp:Label>
                    <br />
                    <br />
                    <p class="first players" id="p11300" runat="server"></p>
                    <p class="players" id="p21300" runat="server"></p>
                    <p class="players" id="p31300" runat="server"></p>
                    <p class="last players" id="p41300" runat="server"></p>
                </td>
                <td class="auto-style9" id="cell1400" runat="server">

                    <p class="time">:00</p>
                    <asp:Label ID="lbl1400" runat="server" Text="Hcp" class="lblHandicap"></asp:Label>
                    <br />
                    <br />
                    <p class="first players" id="p11400" runat="server"></p>
                    <p class="players" id="p21400" runat="server"></p>
                    <p class="players" id="p31400" runat="server"></p>
                    <p class="last players" id="p41400" runat="server"></p>
                </td>
                <td class="auto-style9" id="cell1500" runat="server">

                    <p class="time">:00</p>
                    <asp:Label ID="lbl1500" runat="server" Text="Hcp" class="lblHandicap"></asp:Label>
                    <br />
                    <br />
                    <p class="first players" id="p11500" runat="server"></p>
                    <p class="players" id="p21500" runat="server"></p>
                    <p class="players" id="p31500" runat="server"></p>
                    <p class="last players" id="p41500" runat="server"></p>
                </td>
                <td class="auto-style9" id="cell1600" runat="server">

                    <p class="time">:00</p>
                    <asp:Label ID="lbl1600" runat="server" Text="Hcp" class="lblHandicap"></asp:Label>
                    <br />
                    <br />
                    <p class="first players" id="p11600" runat="server"></p>
                    <p class="players" id="p21600" runat="server"></p>
                    <p class="players" id="p31600" runat="server"></p>
                    <p class="last players" id="p41600" runat="server"></p>
                </td>
                <td class="auto-style9" id="cell1700" runat="server">

                    <p class="time">:00</p>
                    <asp:Label ID="lbl1700" runat="server" Text="Hcp" class="lblHandicap"></asp:Label>
                    <br />
                    <br />
                    <p class="first players" id="p11700" runat="server"></p>
                    <p class="players" id="p21700" runat="server"></p>
                    <p class="players" id="p31700" runat="server"></p>
                    <p class="last players" id="p41700" runat="server"></p>
                </td>
                <td class="auto-style9" id="cell1800" runat="server">

                    <p class="time">:00</p>
                    <asp:Label ID="lbl1800" runat="server" Text="Hcp" class="lblHandicap"></asp:Label>
                    <br />
                    <br />
                    <p class="first players" id="p11800" runat="server"></p>
                    <p class="players" id="p21800" runat="server"></p>
                    <p class="players" id="p31800" runat="server"></p>
                    <p class="last players" id="p41800" runat="server"></p>
                </td>
                <td class="auto-style9" id="cell1900" runat="server">

                    <p class="time">:00</p>
                    <asp:Label ID="lbl1900" runat="server" Text="Hcp" class="lblHandicap"></asp:Label>
                    <br />
                    <br />
                    <p class="first players" id="p11900" runat="server"></p>
                    <p class="players" id="p21900" runat="server"></p>
                    <p class="players" id="p31900" runat="server"></p>
                    <p class="last players" id="p41900" runat="server"></p>
                </td>
            </tr>
            <tr>
                <td class="auto-style9" id="cell0510" runat="server">

                    <p class="time">:10</p>
                    <asp:Label ID="lbl0510" runat="server" Text="Hcp" class="lblHandicap"></asp:Label>
                    <br />
                    <br />
                    <p class="first players" id="p10510" runat="server"></p>
                    <p class="players" id="p20510" runat="server"></p>
                    <p class="players" id="p30510" runat="server"></p>
                    <p class="last players" id="p40510" runat="server"></p>
                </td>
                <td class="auto-style9" id="cell0610" runat="server">

                    <p class="time">:10</p>
                    <asp:Label ID="lbl0610" runat="server" Text="Hcp" class="lblHandicap"></asp:Label>
                    <br />
                    <br />
                    <p class="first players" id="p10610" runat="server"></p>
                    <p class="players" id="p20610" runat="server"></p>
                    <p class="players" id="p30610" runat="server"></p>
                    <p class="last players" id="p40610" runat="server"></p>
                </td>
                <td class="auto-style9" id="cell0710" runat="server">

                    <p class="time">:10</p>
                    <asp:Label ID="lbl0710" runat="server" Text="Hcp" class="lblHandicap"></asp:Label>
                    <br />
                    <br />
                    <p class="first players" id="p10710" runat="server"></p>
                    <p class="players" id="p20710" runat="server"></p>
                    <p class="players" id="p30710" runat="server"></p>
                    <p class="last players" id="p40710" runat="server"></p>
                </td>
                <td class="auto-style9" id="cell0810" runat="server">

                    <p class="time">:10</p>
                    <asp:Label ID="lbl0810" runat="server" Text="Hcp" class="lblHandicap"></asp:Label>
                    <br />
                    <br />
                    <p class="first players" id="p10810" runat="server"></p>
                    <p class="players" id="p20810" runat="server"></p>
                    <p class="players" id="p30810" runat="server"></p>
                    <p class="last players" id="p40810" runat="server"></p>
                </td>
                <td class="auto-style9" id="cell0910" runat="server">

                    <p class="time">:10</p>
                    <asp:Label ID="lbl0910" runat="server" Text="Hcp" class="lblHandicap"></asp:Label>
                    <br />
                    <br />
                    <p class="first players" id="p10910" runat="server"></p>
                    <p class="players" id="p20910" runat="server"></p>
                    <p class="players" id="p30910" runat="server"></p>
                    <p class="last players" id="p40910" runat="server"></p>
                </td>
                <td class="auto-style9" id="cell1010" runat="server">

                    <p class="time">:10</p>
                    <asp:Label ID="lbl1010" runat="server" Text="Hcp" class="lblHandicap"></asp:Label>
                    <br />
                    <br />
                    <p class="first players" id="p11010" runat="server"></p>
                    <p class="players" id="p21010" runat="server"></p>
                    <p class="players" id="p31010" runat="server"></p>
                    <p class="last players" id="p41010" runat="server"></p>
                </td>
                <td class="auto-style9" id="cell1110" runat="server">

                    <p class="time">:10</p>
                    <asp:Label ID="lbl1110" runat="server" Text="Hcp" class="lblHandicap"></asp:Label>
                    <br />
                    <br />
                    <p class="first players" id="p11110" runat="server"></p>
                    <p class="players" id="p21110" runat="server"></p>
                    <p class="players" id="p31110" runat="server"></p>
                    <p class="last players" id="p41110" runat="server"></p>
                </td>
                <td class="auto-style9" id="cell1210" runat="server">

                    <p class="time">:10</p>
                    <asp:Label ID="lbl1210" runat="server" Text="Hcp" class="lblHandicap"></asp:Label>
                    <br />
                    <br />
                    <p class="first players" id="p11210" runat="server"></p>
                    <p class="players" id="p21210" runat="server"></p>
                    <p class="players" id="p31210" runat="server"></p>
                    <p class="last players" id="p41210" runat="server"></p>
                </td>
                <td class="auto-style9" id="cell1310" runat="server">

                    <p class="time">:10</p>
                    <asp:Label ID="lbl1310" runat="server" Text="Hcp" class="lblHandicap"></asp:Label>
                    <br />
                    <br />
                    <p class="first players" id="p11310" runat="server"></p>
                    <p class="players" id="p21310" runat="server"></p>
                    <p class="players" id="p31310" runat="server"></p>
                    <p class="last players" id="p41310" runat="server"></p>
                </td>
                <td class="auto-style9" id="cell1410" runat="server">

                    <p class="time">:10</p>
                    <asp:Label ID="lbl1410" runat="server" Text="Hcp" class="lblHandicap"></asp:Label>
                    <br />
                    <br />
                    <p class="first players" id="p11410" runat="server"></p>
                    <p class="players" id="p21410" runat="server"></p>
                    <p class="players" id="p31410" runat="server"></p>
                    <p class="last players" id="p41410" runat="server"></p>
                </td>
                <td class="auto-style9" id="cell1510" runat="server">

                    <p class="time">:10</p>
                    <asp:Label ID="lbl1510" runat="server" Text="Hcp" class="lblHandicap"></asp:Label>
                    <br />
                    <br />
                    <p class="first players" id="p11510" runat="server"></p>
                    <p class="players" id="p21510" runat="server"></p>
                    <p class="players" id="p31510" runat="server"></p>
                    <p class="last players" id="p41510" runat="server"></p>
                </td>
                <td class="auto-style9" id="cell1610" runat="server">

                    <p class="time">:10</p>
                    <asp:Label ID="lbl1610" runat="server" Text="Hcp" class="lblHandicap"></asp:Label>
                    <br />
                    <br />
                    <p class="first players" id="p11610" runat="server"></p>
                    <p class="players" id="p21610" runat="server"></p>
                    <p class="players" id="p31610" runat="server"></p>
                    <p class="last players" id="p41610" runat="server"></p>
                </td>
                <td class="auto-style9" id="cell1710" runat="server">

                    <p class="time">:10</p>
                    <asp:Label ID="lbl1710" runat="server" Text="Hcp" class="lblHandicap"></asp:Label>
                    <br />
                    <br />
                    <p class="first players" id="p11710" runat="server"></p>
                    <p class="players" id="p21710" runat="server"></p>
                    <p class="players" id="p31710" runat="server"></p>
                    <p class="last players" id="p41710" runat="server"></p>
                </td>
                <td class="auto-style9" id="cell1810" runat="server">

                    <p class="time">:10</p>
                    <asp:Label ID="lbl1810" runat="server" Text="Hcp" class="lblHandicap"></asp:Label>
                    <br />
                    <br />
                    <p class="first players" id="p11810" runat="server"></p>
                    <p class="players" id="p21810" runat="server"></p>
                    <p class="players" id="p31810" runat="server"></p>
                    <p class="last players" id="p41810" runat="server"></p>
                </td>
                <td class="auto-style9" id="cell1910" runat="server">

                    <p class="time">:10</p>
                    <asp:Label ID="lbl1910" runat="server" Text="Hcp" class="lblHandicap"></asp:Label>
                    <br />
                    <br />
                    <p class="first players" id="p11910" runat="server"></p>
                    <p class="players" id="p21910" runat="server"></p>
                    <p class="players" id="p31910" runat="server"></p>
                    <p class="last players" id="p41910" runat="server"></p>
                </td>
            </tr>
            <tr>
                <td class="auto-style9" id="cell0520" runat="server">

                    <p class="time">:20</p>
                    <asp:Label ID="lbl0520" runat="server" Text="Hcp" class="lblHandicap"></asp:Label>
                    <br />
                    <br />
                    <p class="first players" id="p10520" runat="server"></p>
                    <p class="players" id="p20520" runat="server"></p>
                    <p class="players" id="p30520" runat="server"></p>
                    <p class="last players" id="p40520" runat="server"></p>
                </td>
                <td class="auto-style9" id="cell0620" runat="server">

                    <p class="time">:20</p>
                    <asp:Label ID="lbl0620" runat="server" Text="Hcp" class="lblHandicap"></asp:Label>
                    <br />
                    <br />
                    <p class="first players" id="p10620" runat="server"></p>
                    <p class="players" id="p20620" runat="server"></p>
                    <p class="players" id="p30620" runat="server"></p>
                    <p class="last players" id="p40620" runat="server"></p>
                </td>
                <td class="auto-style9" id="cell0720" runat="server">

                    <p class="time">:20</p>
                    <asp:Label ID="lbl0720" runat="server" Text="Hcp" class="lblHandicap"></asp:Label>
                    <br />
                    <br />
                    <p class="first players" id="p10720" runat="server"></p>
                    <p class="players" id="p20720" runat="server"></p>
                    <p class="players" id="p30720" runat="server"></p>
                    <p class="last players" id="p40720" runat="server"></p>
                </td>
                <td class="auto-style9" id="cell0820" runat="server">

                    <p class="time">:20</p>
                    <asp:Label ID="lbl0820" runat="server" Text="Hcp" class="lblHandicap"></asp:Label>
                    <br />
                    <br />
                    <p class="first players" id="p10820" runat="server"></p>
                    <p class="players" id="p20820" runat="server"></p>
                    <p class="players" id="p30820" runat="server"></p>
                    <p class="last players" id="p40820" runat="server"></p>
                </td>
                <td class="auto-style9" id="cell0920" runat="server">

                    <p class="time">:20</p>
                    <asp:Label ID="lbl0920" runat="server" Text="Hcp" class="lblHandicap"></asp:Label>
                    <br />
                    <br />
                    <p class="first players" id="p10920" runat="server"></p>
                    <p class="players" id="p20920" runat="server"></p>
                    <p class="players" id="p30920" runat="server"></p>
                    <p class="last players" id="p40920" runat="server"></p>
                </td>
                <td class="auto-style9" id="cell1020" runat="server">

                    <p class="time">:20</p>
                    <asp:Label ID="lbl1020" runat="server" Text="Hcp" class="lblHandicap"></asp:Label>
                    <br />
                    <br />
                    <p class="first players" id="p11020" runat="server"></p>
                    <p class="players" id="p21020" runat="server"></p>
                    <p class="players" id="p31020" runat="server"></p>
                    <p class="last players" id="p41020" runat="server"></p>
                </td>
                <td class="auto-style9" id="cell1120" runat="server">

                    <p class="time">:20</p>
                    <asp:Label ID="lbl1120" runat="server" Text="Hcp" class="lblHandicap"></asp:Label>
                    <br />
                    <br />
                    <p class="first players" id="p11120" runat="server"></p>
                    <p class="players" id="p21120" runat="server"></p>
                    <p class="players" id="p31120" runat="server"></p>
                    <p class="last players" id="p41120" runat="server"></p>
                </td>
                <td class="auto-style9" id="cell1220" runat="server">

                    <p class="time">:20</p>
                    <asp:Label ID="lbl1220" runat="server" Text="Hcp" class="lblHandicap"></asp:Label>
                    <br />
                    <br />
                    <p class="first players" id="p11220" runat="server"></p>
                    <p class="players" id="p21220" runat="server"></p>
                    <p class="players" id="p31220" runat="server"></p>
                    <p class="last players" id="p41220" runat="server"></p>
                </td>
                <td class="auto-style9" id="cell1320" runat="server">

                    <p class="time">:20</p>
                    <asp:Label ID="lbl1320" runat="server" Text="Hcp" class="lblHandicap"></asp:Label>
                    <br />
                    <br />
                    <p class="first players" id="p11320" runat="server"></p>
                    <p class="players" id="p21320" runat="server"></p>
                    <p class="players" id="p31320" runat="server"></p>
                    <p class="last players" id="p41320" runat="server"></p>
                </td>
                <td class="auto-style9" id="cell1420" runat="server">

                    <p class="time">:20</p>
                    <asp:Label ID="lbl1420" runat="server" Text="Hcp" class="lblHandicap"></asp:Label>
                    <br />
                    <br />
                    <p class="first players" id="p11420" runat="server"></p>
                    <p class="players" id="p21420" runat="server"></p>
                    <p class="players" id="p31420" runat="server"></p>
                    <p class="last players" id="p41420" runat="server"></p>
                </td>
                <td class="auto-style9" id="cell1520" runat="server">

                    <p class="time">:20</p>
                    <asp:Label ID="lbl1520" runat="server" Text="Hcp" class="lblHandicap"></asp:Label>
                    <br />
                    <br />
                    <p class="first players" id="p11520" runat="server"></p>
                    <p class="players" id="p21520" runat="server"></p>
                    <p class="players" id="p31520" runat="server"></p>
                    <p class="last players" id="p41520" runat="server"></p>
                </td>
                <td class="auto-style9" id="cell1620" runat="server">

                    <p class="time">:20</p>
                    <asp:Label ID="lbl1620" runat="server" Text="Hcp" class="lblHandicap"></asp:Label>
                    <br />
                    <br />
                    <p class="first players" id="p11620" runat="server"></p>
                    <p class="players" id="p21620" runat="server"></p>
                    <p class="players" id="p31620" runat="server"></p>
                    <p class="last players" id="p41620" runat="server"></p>
                </td>
                <td class="auto-style9" id="cell1720" runat="server">

                    <p class="time">:20</p>
                    <asp:Label ID="lbl1720" runat="server" Text="Hcp" class="lblHandicap"></asp:Label>
                    <br />
                    <br />
                    <p class="first players" id="p11720" runat="server"></p>
                    <p class="players" id="p21720" runat="server"></p>
                    <p class="players" id="p31720" runat="server"></p>
                    <p class="last players" id="p41720" runat="server"></p>
                </td>
                <td class="auto-style9" id="cell1820" runat="server">

                    <p class="time">:20</p>
                    <asp:Label ID="lbl1820" runat="server" Text="Hcp" class="lblHandicap"></asp:Label>
                    <br />
                    <br />
                    <p class="first players" id="p11820" runat="server"></p>
                    <p class="players" id="p21820" runat="server"></p>
                    <p class="players" id="p31820" runat="server"></p>
                    <p class="last players" id="p41820" runat="server"></p>
                </td>
                <td class="auto-style9" id="cell1920" runat="server">

                    <p class="time">:20</p>
                    <asp:Label ID="lbl1920" runat="server" Text="Hcp" class="lblHandicap"></asp:Label>
                    <br />
                    <br />
                    <p class="first players" id="p11920" runat="server"></p>
                    <p class="players" id="p21920" runat="server"></p>
                    <p class="players" id="p31920" runat="server"></p>
                    <p class="last players" id="p41920" runat="server"></p>
                </td>
            </tr>
            <tr>
                <td class="auto-style9" id="cell0530" runat="server">

                    <p class="time">:30</p>
                    <asp:Label ID="lbl0530" runat="server" Text="Hcp" class="lblHandicap"></asp:Label>
                    <br />
                    <br />
                    <p class="first players" id="p10530" runat="server"></p>
                    <p class="players" id="p20530" runat="server"></p>
                    <p class="players" id="p30530" runat="server"></p>
                    <p class="last players" id="p40530" runat="server"></p>
                </td>
                <td class="auto-style9" id="cell0630" runat="server">

                    <p class="time">:30</p>
                    <asp:Label ID="lbl0630" runat="server" Text="Hcp" class="lblHandicap"></asp:Label>
                    <br />
                    <br />
                    <p class="first players" id="p10630" runat="server"></p>
                    <p class="players" id="p20630" runat="server"></p>
                    <p class="players" id="p30630" runat="server"></p>
                    <p class="last players" id="p40630" runat="server"></p>
                </td>
                <td class="auto-style9" id="cell0730" runat="server">

                    <p class="time">:30</p>
                    <asp:Label ID="lbl0730" runat="server" Text="Hcp" class="lblHandicap"></asp:Label>
                    <br />
                    <br />
                    <p class="first players" id="p10730" runat="server"></p>
                    <p class="players" id="p20730" runat="server"></p>
                    <p class="players" id="p30730" runat="server"></p>
                    <p class="last players" id="p40730" runat="server"></p>
                </td>
                <td class="auto-style9" id="cell0830" runat="server">

                    <p class="time">:30</p>
                    <asp:Label ID="lbl0830" runat="server" Text="Hcp" class="lblHandicap"></asp:Label>
                    <br />
                    <br />
                    <p class="first players" id="p10830" runat="server"></p>
                    <p class="players" id="p20830" runat="server"></p>
                    <p class="players" id="p30830" runat="server"></p>
                    <p class="last players" id="p40830" runat="server"></p>
                </td>
                <td class="auto-style9" id="cell0930" runat="server">

                    <p class="time">:30</p>
                    <asp:Label ID="lbl0930" runat="server" Text="Hcp" class="lblHandicap"></asp:Label>
                    <br />
                    <br />
                    <p class="first players" id="p10930" runat="server"></p>
                    <p class="players" id="p20930" runat="server"></p>
                    <p class="players" id="p30930" runat="server"></p>
                    <p class="last players" id="p40930" runat="server"></p>
                </td>
                <td class="auto-style9" id="cell1030" runat="server">

                    <p class="time">:30</p>
                    <asp:Label ID="lbl1030" runat="server" Text="Hcp" class="lblHandicap"></asp:Label>
                    <br />
                    <br />
                    <p class="first players" id="p11030" runat="server"></p>
                    <p class="players" id="p21030" runat="server"></p>
                    <p class="players" id="p31030" runat="server"></p>
                    <p class="last players" id="p41030" runat="server"></p>
                </td>
                <td class="auto-style9" id="cell1130" runat="server">
                    <p class="time">:30</p>
                    <asp:Label ID="lbl1130" runat="server" Text="Hcp" class="lblHandicap"></asp:Label>
                    <br />
                    <br />
                    <p class="first players" id="p11130" runat="server"></p>
                    <p class="players" id="p21130" runat="server"></p>
                    <p class="players" id="p31130" runat="server"></p>
                    <p class="last players" id="p41130" runat="server"></p>
                </td>
                <td class="auto-style9" id="cell1230" runat="server">
                    <p class="time">:30</p>
                    <asp:Label ID="lbl1230" runat="server" Text="Hcp" class="lblHandicap"></asp:Label>
                    <br />
                    <br />
                    <p class="first players" id="p11230" runat="server"></p>
                    <p class="players" id="p21230" runat="server"></p>
                    <p class="players" id="p31230" runat="server"></p>
                    <p class="last players" id="p41230" runat="server"></p>
                </td>
                <td class="auto-style9" id="cell1330" runat="server">
                    <p class="time">:30</p>
                    <asp:Label ID="lbl1330" runat="server" Text="Hcp" class="lblHandicap"></asp:Label>
                    <br />
                    <br />
                    <p class="first players" id="p11330" runat="server"></p>
                    <p class="players" id="p21330" runat="server"></p>
                    <p class="players" id="p31330" runat="server"></p>
                    <p class="last players" id="p41330" runat="server"></p>
                </td>
                <td class="auto-style9" id="cell1430" runat="server">
                    <p class="time">:30</p>
                    <asp:Label ID="lbl1430" runat="server" Text="Hcp" class="lblHandicap"></asp:Label>
                    <br />
                    <br />
                    <p class="first players" id="p11430" runat="server"></p>
                    <p class="players" id="p21430" runat="server"></p>
                    <p class="players" id="p31430" runat="server"></p>
                    <p class="last players" id="p41430" runat="server"></p>
                </td>
                <td class="auto-style9" id="cell1530" runat="server">
                    <p class="time">:30</p>
                    <asp:Label ID="lbl1530" runat="server" Text="Hcp" class="lblHandicap"></asp:Label>
                    <br />
                    <br />
                    <p class="first players" id="p11530" runat="server"></p>
                    <p class="players" id="p21530" runat="server"></p>
                    <p class="players" id="p31530" runat="server"></p>
                    <p class="last players" id="p41530" runat="server"></p>
                </td>
                <td class="auto-style9" id="cell1630" runat="server">
                    <p class="time">:30</p>
                    <asp:Label ID="lbl1630" runat="server" Text="Hcp" class="lblHandicap"></asp:Label>
                    <br />
                    <br />
                    <p class="first players" id="p11630" runat="server"></p>
                    <p class="players" id="p21630" runat="server"></p>
                    <p class="players" id="p31630" runat="server"></p>
                    <p class="last players" id="p41630" runat="server"></p>

                </td>
                <td class="auto-style9" id="cell1730" runat="server">
                    <p class="time">:30</p>
                    <asp:Label ID="lbl1730" runat="server" Text="Hcp" class="lblHandicap"></asp:Label>
                    <br />
                    <br />
                    <p class="first players" id="p11730" runat="server"></p>
                    <p class="players" id="p21730" runat="server"></p>
                    <p class="players" id="p31730" runat="server"></p>
                    <p class="last players" id="p41730" runat="server"></p>
                </td>
                <td class="auto-style9" id="cell1830" runat="server">
                    <p class="time">:30</p>
                    <asp:Label ID="lbl1830" runat="server" Text="Hcp" class="lblHandicap"></asp:Label>
                    <br />
                    <br />
                    <p class="first players" id="p11830" runat="server"></p>
                    <p class="players" id="p21830" runat="server"></p>
                    <p class="players" id="p31830" runat="server"></p>
                    <p class="last players" id="p41830" runat="server"></p>
                </td>
                <td class="auto-style9" id="cell1930" runat="server">
                    <p class="time">:30</p>
                    <asp:Label ID="lbl1930" runat="server" Text="Hcp" class="lblHandicap"></asp:Label>
                    <br />
                    <br />
                    <p class="first players" id="p11930" runat="server"></p>
                    <p class="players" id="p21930" runat="server"></p>
                    <p class="players" id="p31930" runat="server"></p>
                    <p class="last players" id="p41930" runat="server"></p>
                </td>
            </tr>
            <tr>
                <td class="auto-style9" id="cell0540" runat="server">
                    <p class="time">:40</p>
                    <asp:Label ID="lbl0540" runat="server" Text="Hcp" class="lblHandicap"></asp:Label>
                    <br />
                    <br />
                    <p class="first players" id="p10540" runat="server"></p>
                    <p class="players" id="p20540" runat="server"></p>
                    <p class="players" id="p30540" runat="server"></p>
                    <p class="last players" id="p40540" runat="server"></p>
                </td>
                <td class="auto-style9" id="cell0640" runat="server">
                    <p class="time">:40</p>
                    <asp:Label ID="lbl0640" runat="server" Text="Hcp" class="lblHandicap"></asp:Label>
                    <br />
                    <br />
                    <p class="first players" id="p10640"></p>
                    <p class="players" id="p20640"></p>
                    <p class="players" id="p30640" runat="server"></p>
                    <p class="last players" id="p40640" runat="server"></p>
                </td>
                <td class="auto-style9" id="cell0740" runat="server">
                    <p class="time">:40</p>
                    <asp:Label ID="lbl0740" runat="server" Text="Hcp" class="lblHandicap"></asp:Label>
                    <br />
                    <br />
                    <p class="first players" id="p10740" runat="server"></p>
                    <p class="players" id="p20740" runat="server"></p>
                    <p class="players" id="p30740" runat="server"></p>
                    <p class="last players" id="p40740" runat="server"></p>
                </td>
                <td class="auto-style9" id="cell0840" runat="server">
                    <p class="time">:40</p>
                    <asp:Label ID="lbl0840" runat="server" Text="Hcp" class="lblHandicap"></asp:Label>
                    <br />
                    <br />
                    <p class="first players" id="p10840" runat="server"></p>
                    <p class="players" id="p20840" runat="server"></p>
                    <p class="players" id="p30840" runat="server"></p>
                    <p class="last players" id="p40840" runat="server"></p>
                </td>
                <td class="auto-style9" id="cell0940" runat="server">
                    <p class="time">:40</p>
                    <asp:Label ID="lbl0940" runat="server" Text="Hcp" class="lblHandicap"></asp:Label>
                    <br />
                    <br />
                    <p class="first players" id="p10940" runat="server"></p>
                    <p class="players" id="p20940" runat="server"></p>
                    <p class="players" id="p30940" runat="server"></p>
                    <p class="last players" id="p40940" runat="server"></p>
                </td>
                <td class="auto-style9" id="cell1040" runat="server">
                    <p class="time">:40</p>
                    <asp:Label ID="lbl1040" runat="server" Text="Hcp" class="lblHandicap"></asp:Label>
                    <br />
                    <br />
                    <p class="first players" id="p11040" runat="server"></p>
                    <p class="players" id="p21040" runat="server"></p>
                    <p class="players" id="p31040" runat="server"></p>
                    <p class="last players" id="p41040" runat="server"></p>
                </td>
                <td class="auto-style9" id="cell1140" runat="server">
                    <p class="time">:40</p>
                    <asp:Label ID="lbl1140" runat="server" Text="Hcp" class="lblHandicap"></asp:Label>
                    <br />
                    <br />
                    <p class="first players" id="p11140" runat="server"></p>
                    <p class="players" id="p21140" runat="server"></p>
                    <p class="players" id="p31140" runat="server"></p>
                    <p class="last players" id="p41140" runat="server"></p>
                </td>
                <td class="auto-style9" id="cell1240" runat="server">
                    <p class="time">:40</p>
                    <asp:Label ID="lbl1240" runat="server" Text="Hcp" class="lblHandicap"></asp:Label>
                    <br />
                    <br />
                    <p class="first players" id="p11240" runat="server"></p>
                    <p class="players" id="p21240" runat="server"></p>
                    <p class="players" id="p31240" runat="server"></p>
                    <p class="last players" id="p41240" runat="server"></p>
                </td>
                <td class="auto-style9" id="cell1340" runat="server">
                    <p class="time">:40</p>
                    <asp:Label ID="lbl1340" runat="server" Text="Hcp" class="lblHandicap"></asp:Label>
                    <br />
                    <br />
                    <p class="first players" id="p11340" runat="server"></p>
                    <p class="players" id="p21340" runat="server"></p>
                    <p class="players" id="p31340" runat="server"></p>
                    <p class="last players" id="p41340" runat="server"></p>
                </td>
                <td class="auto-style9" id="cell1440" runat="server">
                    <p class="time">:40</p>
                    <asp:Label ID="lbl1440" runat="server" Text="Hcp" class="lblHandicap"></asp:Label>
                    <br />
                    <br />
                    <p class="first players" id="p11440" runat="server"></p>
                    <p class="players" id="p21440" runat="server"></p>
                    <p class="players" id="p31440" runat="server"></p>
                    <p class="last players" id="p41440" runat="server"></p>
                </td>
                <td class="auto-style9" id="cell1540" runat="server">
                    <p class="time">:40</p>
                    <asp:Label ID="lbl1540" runat="server" Text="Hcp" class="lblHandicap"></asp:Label>
                    <br />
                    <br />
                    <p class="first players" id="p11540" runat="server"></p>
                    <p class="players" id="p21540" runat="server"></p>
                    <p class="players" id="p31540" runat="server"></p>
                    <p class="last players" id="p41540" runat="server"></p>
                </td>
                <td class="auto-style9" id="cell1640" runat="server">
                    <p class="time">:40</p>
                    <asp:Label ID="lbl1640" runat="server" Text="Hcp" class="lblHandicap"></asp:Label>
                    <br />
                    <br />
                    <p class="first players" id="p11640" runat="server"></p>
                    <p class="players" id="p21640" runat="server"></p>
                    <p class="players" id="p31640" runat="server"></p>
                    <p class="last players" id="p41640" runat="server"></p>
                </td>
                <td class="auto-style9" id="cell1740" runat="server">
                    <p class="time">:40</p>
                    <asp:Label ID="lbl1740" runat="server" Text="Hcp" class="lblHandicap"></asp:Label>
                    <br />
                    <br />
                    <p class="first players" id="p11740" runat="server"></p>
                    <p class="players" id="p21740" runat="server"></p>
                    <p class="players" id="p31740" runat="server"></p>
                    <p class="last players" id="p41740" runat="server"></p>
                </td>
                <td class="auto-style9" id="cell1840" runat="server">
                    <p class="time">:40</p>
                    <asp:Label ID="lbl1840" runat="server" Text="Hcp" class="lblHandicap"></asp:Label>
                    <br />
                    <br />
                    <p class="first players" id="p11840" runat="server"></p>
                    <p class="players" id="p21840" runat="server"></p>
                    <p class="players" id="p31840" runat="server"></p>
                    <p class="last players" id="p41840" runat="server"></p>
                </td>
                <td class="auto-style9" id="cell1940" runat="server">
                    <p class="time">:40</p>
                    <asp:Label ID="lbl1940" runat="server" Text="Hcp" class="lblHandicap"></asp:Label>
                    <br />
                    <br />
                    <p class="first players" id="p11940" runat="server"></p>
                    <p class="players" id="p21940" runat="server"></p>
                    <p class="players" id="p31940" runat="server"></p>
                    <p class="last players" id="p41940" runat="server"></p>
                </td>
            </tr>
            <tr>
                <td class="auto-style9" id="cell0550" runat="server">
                    <p class="time">:50</p>
                    <asp:Label ID="lbl0550" runat="server" Text="Hcp" class="lblHandicap"></asp:Label>

                    <br />
                    <br />
                    <p class="first players" id="p10550" runat="server"></p>
                    <p class="players" id="p20550" runat="server"></p>
                    <p class="players" id="p30550" runat="server"></p>
                    <p class="last players" id="p40550" runat="server"></p>
                </td>
                <td class="auto-style9" id="cell0650" runat="server">
                    <p class="time">:50</p>
                    <asp:Label ID="lbl0650" runat="server" Text="Hcp" class="lblHandicap"></asp:Label>

                    <br />
                    <br />
                    <p class="first players" id="p10650" runat="server"></p>
                    <p class="players" id="p20650" runat="server"></p>
                    <p class="players" id="p30650" runat="server"></p>
                    <p class="last players" id="p40650" runat="server"></p>
                </td>
                <td class="auto-style9" id="cell0750" runat="server">
                    <p class="time">:50</p>
                    <asp:Label ID="lbl0750" runat="server" Text="Hcp" class="lblHandicap"></asp:Label>

                    <br />
                    <br />
                    <p class="first players" id="p10750" runat="server"></p>
                    <p class="players" id="p20750" runat="server"></p>
                    <p class="players" id="p30750" runat="server"></p>
                    <p class="last players" id="p40750" runat="server"></p>
                </td>
                <td class="auto-style9" id="cell0850" runat="server">
                    <p class="time">:50</p>
                    <asp:Label ID="lbl0850" runat="server" Text="Hcp" class="lblHandicap"></asp:Label>

                    <br />
                    <br />
                    <p class="first players" id="p10850" runat="server"></p>
                    <p class="players" id="p20850" runat="server"></p>
                    <p class="players" id="p30850" runat="server"></p>
                    <p class="last players" id="p40850" runat="server"></p>
                </td>
                <td class="auto-style9" id="cell0950" runat="server">
                    <p class="time">:50</p>
                    <asp:Label ID="lbl0950" runat="server" Text="Hcp" class="lblHandicap"></asp:Label>
                    <br />
                    <br />
                    <p class="first players" id="p10950" runat="server"></p>
                    <p class="players" id="p20950" runat="server"></p>
                    <p class="players" id="p30950" runat="server"></p>
                    <p class="last players" id="p40950" runat="server"></p>
                </td>
                <td class="auto-style9" id="cell1050" runat="server">
                    <p class="time">:50</p>
                    <asp:Label ID="lbl1050" runat="server" Text="Hcp" class="lblHandicap"></asp:Label>
                    <br />
                    <br />
                    <p class="first players" id="p11050" runat="server"></p>
                    <p class="players" id="p21050" runat="server"></p>
                    <p class="players" id="p31050" runat="server"></p>
                    <p class="last players" id="p41050" runat="server"></p>
                </td>
                <td class="auto-style9" id="cell1150" runat="server">
                    <p class="time">:50</p>
                    <asp:Label ID="lbl1150" runat="server" Text="Hcp" class="lblHandicap"></asp:Label>
                    <br />
                    <br />
                    <p class="first players" id="p11150" runat="server"></p>
                    <p class="players" id="p21150" runat="server"></p>
                    <p class="players" id="p31150" runat="server"></p>
                    <p class="last players" id="p41150" runat="server"></p>
                </td>
                <td class="auto-style9" id="cell1250" runat="server">
                    <p class="time">:50</p>
                    <asp:Label ID="lbl1250" runat="server" Text="Hcp" class="lblHandicap"></asp:Label>
                    <br />
                    <br />
                    <p class="first players" id="p11250" runat="server"></p>
                    <p class="players" id="p21250" runat="server"></p>
                    <p class="players" id="p31250" runat="server"></p>
                    <p class="last players" id="p41250" runat="server"></p>
                </td>
                <td class="auto-style9" id="cell1350" runat="server">
                    <p class="time">:50</p>
                    <asp:Label ID="lbl1350" runat="server" Text="Hcp" class="lblHandicap"></asp:Label>
                    <br />
                    <br />
                    <p class="first players" id="p11350" runat="server"></p>
                    <p class="players" id="p21350" runat="server"></p>
                    <p class="players" id="p31350" runat="server"></p>
                    <p class="last players" id="p41350" runat="server"></p>
                </td>
                <td class="auto-style9" id="cell1450" runat="server">
                    <p class="time">:50</p>
                    <asp:Label ID="lbl1450" runat="server" Text="Hcp" class="lblHandicap"></asp:Label>
                    <br />
                    <br />
                    <p class="first players" id="p11450" runat="server"></p>
                    <p class="players" id="p21450" runat="server"></p>
                    <p class="players" id="p31450" runat="server"></p>
                    <p class="last players" id="p41450" runat="server"></p>
                </td>
                <td class="auto-style9" id="cell1550" runat="server">
                    <p class="time">:50</p>
                    <asp:Label ID="lbl1550" runat="server" Text="Hcp" class="lblHandicap"></asp:Label>
                    <br />
                    <br />
                    <p class="first players" id="p11550" runat="server"></p>
                    <p class="players" id="p21550" runat="server"></p>
                    <p class="players" id="p31550" runat="server"></p>
                    <p class="last players" id="p41550" runat="server"></p>
                </td>
                <td class="auto-style9" id="cell1650" runat="server">
                    <p class="time">:50</p>
                    <asp:Label ID="lbl1650" runat="server" Text="Hcp" class="lblHandicap"></asp:Label>
                    <br />
                    <br />
                    <p class="first players" id="p11650" runat="server"></p>
                    <p class="players" id="p21650" runat="server"></p>
                    <p class="players" id="p31650" runat="server"></p>
                    <p class="last players" id="p41650" runat="server"></p>
                </td>
                <td class="auto-style9" id="cell1750" runat="server">
                    <p class="time">:50</p>
                    <asp:Label ID="lbl1750" runat="server" Text="Hcp" class="lblHandicap"></asp:Label>
                    <br />
                    <br />
                    <p class="first players" id="p11750" runat="server"></p>
                    <p class="players" id="p21750" runat="server"></p>
                    <p class="players" id="p31750" runat="server"></p>
                    <p class="last players" id="p41750" runat="server"></p>
                </td>
                <td class="auto-style9" id="cell1850" runat="server">
                    <p class="time">:50</p>
                    <asp:Label ID="lbl1850" runat="server" Text="Hcp" class="lblHandicap"></asp:Label>
                    <br />
                    <br />
                    <p class="first players" id="p11850" runat="server"></p>
                    <p class="players" id="p21850" runat="server"></p>
                    <p class="players" id="p31850" runat="server"></p>
                    <p class="last players" id="p41850" runat="server"></p>
                </td>
                <td class="auto-style9" id="cell1950" runat="server">
                    <p class="time">:50</p>
                    <asp:Label ID="lbl1950" runat="server" Text="Hcp" class="lblHandicap"></asp:Label>
                    <br />
                    <br />
                    <p class="first players" id="p11950" runat="server"></p>
                    <p class="players" id="p21950" runat="server"></p>
                    <p class="players" id="p31950" runat="server"></p>
                    <p class="last players" id="p41950" runat="server"></p>
                </td>
            </tr>
        </table>
    </div>

    <script type="text/javascript">
        function myFunction(id) {

            /*var time = $("#ContentPlaceHolder1_cell0500").attr("id")
            alert(time);*/
            $("#ContentPlaceHolder1_bookingDetail").css("display", "block");

            //alert(id);
            //  $("#ContentPlaceHolder1_bookingDetail").css("display", "block");
            $("#ContentPlaceHolder1_lblBookingDetailDateTime").text(id);
        }
        //bookingtable
        $(document).ready(function () {
            $("#ContentPlaceHolder1_bookingtable td").click(function () {
                $("#ContentPlaceHolder1_bookingDetail").css("display", "block");
                var dat = '<%= table_calender.SelectedDate %>';
                var colID = ($(this).attr("id"));                
                dat = dat.substring(0, 10);
                var tim = colID.replace("ContentPlaceHolder1_cell", "");
                tim = tim.substring(0, 2) + ":" + tim.substring(2, 4) + ":00";
                
                var writ = dat + " " + tim;
               
                PageMethods.fillListbox(writ, fillList, OnGetMessageFailure);
                // GetMessage(column_num, tim); listBookingmany
                document.getElementById("")

                $("#bookingDetail").modal("show");
                //$("#myModal").modal();
                //myModal
                $("#ContentPlaceHolder1_lblBookingDetailDateTime").text(writ);
                //$("#result").html("Row_num =" + row_num + "  ,  Rolumn_num =" + column_num);
            });
        });
        function fillList(result, userContext, methodName)
        {
            document.getElementById("<%= listBookingmany.ClientID %>").innerHTML = result;
        }
    </script>
    </form>
</asp:Content>
