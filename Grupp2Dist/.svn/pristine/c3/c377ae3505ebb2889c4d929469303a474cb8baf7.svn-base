<%@ Page Title="" Language="C#" MasterPageFile="~/headSite.Master" AutoEventWireup="true" CodeBehind="tournamententry.aspx.cs" Inherits="GolfBokning.tournamententry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" EnablePartialRendering="true" />
        <script>
            var teamID = "";
            $(document).ready(function () {
                // Handler for .ready() called.
                $('#bookingDetail').modal({ backdrop: 'static', keyboard: false });
                $('#bookingDetail').modal('hide');
                $('#bookingDetail').on('hidden.bs.modal', function () {
                    $('#<%=listBookingmany.ClientID %>').empty();

                $('#ContentPlaceHolder1_TextBoxSearch>').val('');
            });
            var availableTags = [ <%= SuggestionList %>];

            $("#<%= TextBoxSearch.ClientID %>").autocomplete({
                source: availableTags

            }); //getTeams
                
        });
            function BookOne(id) {
                //TextBoxTeamName
                var t = "<%= TextBoxSearch.Text %>";
            PageMethods.addTeam(id,t , OnGetLoadSuccess, OnGetMessageFailure);
        }
        function BookMany(id) {
            var many = "";
            $("#<%=listBookingmany.ClientID %> > option").each(function () {
                //alert("aa");
                many += this.value + ";";
            });
           //var aa =  $("#<%=listBookingmany.ClientID %> option:selected").text();
            PageMethods.BookMan(id, many, teamID, getResult, OnGetMessageFailure);
        }
        function GetMessage(id) {
            // PageMethods.removeBooking(id, OnGetMessageSuccess, OnGetMessageFailure);
            $("#bookingDetail").modal("show");
        }
        function OnGetMessageSuccess(result, userContext, methodName) {
            PageMethods.fillTable(OnGetLoadSuccess, OnGetMessageFailure);
            // alert(result);

        }
        function OnGetMessageFailure(error, userContext, methodName) {
            alert(error.get_message());
        }
        $(document).ready(function () {
            PageMethods.fillTable(OnGetLoadSuccess, OnGetMessageFailure);
        });
        function OnGetLoadSuccess(result, userContext, methodName) {
            //$("#ContentPlaceHolder1_tbTable").text(result);

            document.getElementById("ContentPlaceHolder1_tbTable").innerHTML = result;
        }
        function getResult(result, userContext, methodName) {
            //$("#ContentPlaceHolder1_tbTable").text(result);
            alert(result);
            //document.getElementById("ContentPlaceHolder1_tbTable").innerHTML = result;
        }

        function showEntry(id) {
            $("#bookingDetail").modal("show");
            $("#makeBooking").attr("onclick", "BookOne('" + id + "'); return false;");
            $("#bookMany").attr("onclick", "BookMany('" + id + "'); return false;");
            PageMethods.getTeams(id, resultFromListbox, OnGetMessageFailure)
        }
        function resultFromListbox(result, userContext, methodName) {
            //$("#ContentPlaceHolder1_tbTable").text(result);
            var re = result.split("|");
            $('#<%=listTeam.ClientID %>').empty();
            $.each(re, function (a, b) {
                $("#<%=listTeam.ClientID %>").append(b);
            });
          // document.getElementById("'#<%=listTeam.ClientID %>'").innerHTML = result;
        }

        function addValToList() {
            //alert($('#<%=listBookingmany.ClientID %>').children().size());

            if ($('#<%=listBookingmany.ClientID %>').children().size() > 3) {
                // alert("Max 4:a åt gången");
                //return;
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

            if (!exists) {
                $('#<%=listBookingmany.ClientID %>').append('<option value="' + matches[1] + '">' + str + '</option>'); // adds item 5 at the end

            }
            else {

            }
            $('#ContentPlaceHolder1_TextBoxSearch').val('');
            console.log(matches[1]);
            if ($('#<%=listBookingmany.ClientID %>').children().size() > 3) {
                document.getElementById("makeBooking").style.visibility = 'hidden';
            }
            else {
                document.getElementById("makeBooking").style.visibility = 'visible';
            }
        }
            function addToTeam() {
            if ($('#<%=listBookingmany.ClientID %>').children().size() > 3) {
                // alert("Max 4:a åt gången");
                //return;
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

            if (!exists) {
                $('#<%=listBookingmany.ClientID %>').append('<option value="' + matches[1] + '">' + str + '</option>'); // adds item 5 at the end

            }
            else {

            }
            $('#ContentPlaceHolder1_TextBoxSearch').val('');
            console.log(matches[1]);
            if ($('#<%=listBookingmany.ClientID %>').children().size() > 3) {
                document.getElementById("makeBooking").style.visibility = 'hidden';
            }
            else {
                document.getElementById("makeBooking").style.visibility = 'visible';
            }
            }
            function addTeam()
            {
               
                var matches = "<%=TextBoxTeamName.ClientID%>";
                matches = document.getElementById(matches).value;
                var exists = false;
                $('#<%=listTeam.ClientID %>  option').each(function () {
                    if (this.value == matches) {
                        exists = true;
                    }
                });

                if (!exists) {
                    $('#<%=listTeam.ClientID %>').append('<option value="">' + matches + '</option>'); // adds item 5 at the end

                }
            }
        function removeFromList() {

            $("#<%=listBookingmany.ClientID %> option:selected").remove();

        }
            function dowork()
            {
                var aa = ($("#<%=listTeam.ClientID%> option:selected").val());
                teamID = aa;
                PageMethods.getMember(aa, getMemb, OnGetMessageFailure)
            }
            function getMemb(result, userContext, methodName) {
                //$("#ContentPlaceHolder1_tbTable").text(result);
                //<%=listBookingmany.ClientID %>
                //document.getElementById("ContentPlaceHolder1_tbTable").innerHTML = result;
                $("#<%=listBookingmany.ClientID %>").empty();
                var re = result.split("|");
                $.each(re, function (a, b) {
                    $("#<%=listBookingmany.ClientID %>").append(b);
                });
            }

        </script>
        <h2></h2>

        <div class="modal fade" id="bookingDetail" role="dialog">
            <div class="modal-dialog modal-lg">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title">Boka tid</h4>
                    </div>
                    <div class="modal-body">
                        <h5>Vill du anmäla dig till: </h5>
                        <asp:Label ID="lblBookingDetailDateTime" runat="server" Text="Date/Time" class="lblBookingDetailClass" data-provide="typeahead"></asp:Label><br />

                        <asp:TextBox ID="TextBoxSearch" runat="server" AutoPostBack="false" CssClass="form-control" />
                        <asp:Button ID="addPer" OnClientClick="addValToList(); return false;" CssClass="btn btn-default" data-dismiss="static" Text="Lägg till spelare" runat="server" />
                        <asp:Button ID="remPer" OnClientClick="removeFromList(); return false;" CssClass="btn btn-default" data-dismiss="static" Text="Ta bort markerad spelare" runat="server" />
                        <asp:Button ID="btnaddToTeam" OnClientClick="addToTeam(); return false;" CssClass="btn btn-default" data-dismiss="static" Text="Lägg till i lag" runat="server" />
                         <asp:Button ID="btnAddTeam" OnClientClick="addTeam(); return false;" CssClass="btn btn-default" data-dismiss="static" Text="Lägg till lag"  AutoPostBack="false" runat="server" />
                        <br />
                        <asp:TextBox ID="TextBoxTeamName" runat="server" AutoPostBack="false" CssClass="form-control"></asp:TextBox>

                        <br />
                        <asp:ListBox ID="listTeam" onchange="dowork(); return false;" runat="server" CssClass="form-control listBoxright"></asp:ListBox>
                        <asp:ListBox ID="listBookingmany" runat="server" CssClass="form-control listboxLeft"></asp:ListBox>


                    </div>
                    <div class="modal-footer">
                        <button id="bookMany" type="button" class="btn btn-default" data-dismiss="modal">Boka flera</button>
                        <button id="makeBooking" type="button" class="btn btn-default" data-dismiss="modal">Boka tid</button>
                        <asp:Button type="button" class="btn btn-default" data-dismiss="modal" Text="Close" runat="server" />
                    </div>
                </div>
            </div>
        </div>

        <table class="tableMinaBokningar table table-bordered table-responsive table-striped">
            <thead>
                <tr>
                    <th>Tid</th>
                    <th>Datum</th>
                    <th>Ändra</th>
                </tr>
            </thead>
            <tbody id="tbTable" runat="server">
            </tbody>
        </table>
    </form>
</asp:Content>
