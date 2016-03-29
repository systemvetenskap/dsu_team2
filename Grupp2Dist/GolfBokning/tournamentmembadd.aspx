<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/headSite.Master" CodeBehind="tournamentmembadd.aspx.cs" Inherits="GolfBokning.tournamentmembadd" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<form id="formMembAdd" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" />
    <script>
        function Join(id_tournament) {
            var id_member = $(".hidden_id").html();
            PageMethods.JoinTournament(id_tournament, id_member, Join_Success, Join_Fail);
        }
        function JoinTeam(id_tournament)
        {
            var id_member = $(".hidden_id").html();
            var many = id_member + ";"; //ContentPlaceHolder1_txtTeam
            var txt = $("#<%=txtTeam.ClientID %>").val();
            if (txt.length < 1)
            {
                alert("Du måste ge laget ett namn");
                return false;
            }

            $("#<%=liTeamMember.ClientID %> > option").each(function () {
                //alert("aa");
                many += this.value + ";";
            });
            PageMethods.JoinTeamTournament(id_tournament, many, txt, Join_Success, Join_Fail);
        }
        function Join_Success(msg) {
            alert("Registrerat på tävling!");
            window.location = "minasidor.aspx";
        }

        function Join_Fail(msg) {
            console.log(msg);
        }

        function CheckJoin(id_tournament) {
            var id_member = $(".hidden_id").html();
            PageMethods.CheckJoinTournament(id_tournament, id_member, CheckJoin_Success, CheckJoin_Fail);
        }

        function CheckJoin_Success(msg) {
            if (msg.indexOf("error") != -1) {
                var id_tournament = msg.replace("error ", "");
                $(".btn").each(function () {
                    if ($(this).hasClass(id_tournament)) {
                        $(this).attr("disabled", true);
                        $(this).html("Anmäld redan");
                    }
                });
            }
        }

        function CheckJoin_Fail(msg) {
            console.log(msg);
        }
        function CheckJoinFull(id_tournament, nbr) {
            PageMethods.CheckJoinTournamentFull(id_tournament, nbr, CheckJoinFull_Success, CheckJoinFull_Fail);
        }

        function CheckJoinFull_Success(msg) {
            var temp = msg.replace("error ", "");
            temp = temp.split(';');

            var id_tournament = temp[0];
            var nbr = temp[1];

            $(".btn").each(function () {
                if ($(this).hasClass(id_tournament)) {
                    $(this).attr("disabled", true);
                    $(this).html("Fullt");
                }
            });
        }

        function CheckJoinFull_Fail(msg) {
            console.log(msg);
        }

        $(document).ready(function () {
            $(".btn").each(function () {
                var id = $(this).attr("data");
                var nbr = $(this).attr("data-nbr");
                CheckJoin(id);
                CheckJoinFull(id, nbr);
            });
        });

        function addInMemb()
        {
            if ($('#<%=liTeamMember.ClientID %>').children().size() >= 3) {
                alert("Får max vara 4:a i varje lag");
                return;
            }

            var str = $("#<%= txtGolfID.ClientID %>").val();
            //if (str.indexOf('(') < 1) {
                PageMethods.getGolfer(str, addFromGolf, OnGetMessageFailure);
                
            //}
            //PageMethods.getMemb()
        }

        function OnGetMessageFailure(error, userContext, methodName) {
            //alert(error.get_message());
            alert("Fel");
        }
        function addFromGolf(result, userContext, methodName) {

            if (result == "FALSE") {
                alert("Ingen golfare med det golf ID:et");
                return;
            }
            var regExp = /\(([^)]+)\)/;
            var matches = regExp.exec(result);
            console.log(matches.length);
            var exists = false;
            $('#<%=liTeamMember.ClientID %>  option').each(function () {
                if (this.value == matches[1]) {
                    exists = true;
                }
            });//btnMembAdd

            if (!exists) {
                $('#<%=liTeamMember.ClientID %>').append(result); // adds item 5 at the end

            }
            else {

            }
            $('#ContentPlaceHolder1_TextBoxSearch').val('');
            console.log(matches[1]);
            if ($('#<%=liTeamMember.ClientID %>').children().size() > 3) {
                document.getElementById("makeBooking").style.visibility = 'hidden';
            }
            else {
                document.getElementById("makeBooking").style.visibility = 'visible';
            }
        }

        $(document).ready(function () {
            var wholeCookie = $.cookie("LoginCookie");
            var partCookie = wholeCookie.split('&');

            $(partCookie).each(function () {
                var crumble = this.split('=');
                if (crumble[0] == "_gender") {
                    $(".btn").each(function () {
                        if ($(this).attr("data-gender") != null) {
                            if ($(this).attr("data-gender") != "Mixed") {
                                if ($(this).attr("data-gender") == crumble[1]) {
                                    $(this).attr("disabled", false);
                                } else {
                                    $(this).attr("disabled", true);
                                    if (crumble[1] == "Female") {
                                        $(this).text("Tävlingen är bara för män");
                                    } else if (crumble[1] == "Male") {
                                        $(this).text("Tävlingen är bara för kvinnor");
                                    }
                                }
                            }
                        }
                    });
                }
            });


        });

        $(document).ready(function () {
            if ($("#ContentPlaceHolder1_liTeamMember").is(":visible")) {
                if ($(this).children().length != 0) {
                    $(".btn").each(function () {
                        if ($(this).text() == "Anmäl") {
                            $(this).text("Skapa lag först");
                            $(this).attr("disabled", true);
                        }
                    });
                }
            }

            $("#ContentPlaceHolder1_btnMembAdd").click(function () {
                $(".btn").each(function () {
                    if ($(this).text() == "Skapa lag först") {
                        $(this).text("Måste vara 4 i laget");
                        $(this).attr("disabled", true);
                    }
                });
                var length = $("#ContentPlaceHolder1_liTeamMember").children().length;
                if (length <= 3) {
                    if (length >= 2) {
                        $(".btn").each(function () {
                            if ($(this).text() == "Måste vara 4 i laget") {
                                $(this).text("Anmäl");
                                $(this).attr("disabled", false);
                            }
                        });
                    } else {
                        $(".btn").each(function () {
                            if ($(this).text() == "Anmäl") {
                                $(this).text("Måste vara 4 i laget");
                                $(this).attr("disabled", true);
                            }
                        });
                    }
                    $(".btn").each(function () {
                        if ($(this).text() == "Skapa lag först") {
                            $(this).text("Anmäl");
                            $(this).attr("disabled", false);
                        }
                    });
                }
                $("#ContentPlaceHolder1_txtGolfID").val("");
                $("#ButtonRM").show();
            });

            $("#ButtonRM").click(function () {
                var length = $("#ContentPlaceHolder1_liTeamMember").children().length;
                if (length <= 3) {
                    $(".btn").each(function () {
                        if ($(this).text() == "Anmäl") {
                            $(this).text("Måste vara 4 i laget");
                            $(this).attr("disabled", true);
                        }
                    });
                } else {
                    $(".btn").each(function () {
                        if ($(this).text() == "Måste vara 4 i laget") {
                            $(this).text("Anmäl");
                            $(this).attr("disabled", false);
                        }
                    });
                }

                $("#ContentPlaceHolder1_liTeamMember").find('option:selected').remove();
                $("#ContentPlaceHolder1_txtGolfID").val("");
                $(this).hide();
            });

            $("#ContentPlaceHolder1_liTeamMember").change(function () {
                $("#ButtonRM").show();
            });
        });
    </script>
    <style>
        #ButtonRM {
            display: none;
            width: 140px;
        }
        .hidden_id {
            display: none;
        }
    </style>
    <asp:Label ID="LabelHiddenId" runat="server" Text=""></asp:Label>
    <asp:Label ID="Label1" runat="server" Text="<h1>Kommande tävlingar</h1>"></asp:Label>  
    <asp:Label ID="Label2" runat="server" Text="Välj tävlingsklass"></asp:Label>
    <asp:RadioButtonList ID="RadioButtonListClass" runat="server" CssClass="radio-inline">

    </asp:RadioButtonList>
    <br />
    <br />
    <br />
    <asp:Panel runat="server" ID="PanelTeam">
        <h2 style="text-align: left;font-size: 1.4em;">Skapa lag</h2>
        <asp:Label runat="server" Text="Lagnamn:"></asp:Label>
        <asp:TextBox runat="server" ID="txtTeam" CssClass="form-control txtname" style="margin-bottom: 5px;"></asp:TextBox>
        <asp:Label runat="server" Text="GolfID på lagmedlemmar:"></asp:Label>
        <br />
        <asp:TextBox runat="server" ID="txtGolfID" style="width:100px;" CssClass="form-control tbme"></asp:TextBox>
        <asp:Button runat="server" ID="btnMembAdd" OnClientClick="addInMemb(); return false;" Text="Lägg till medlem" CssClass="btn btn-default" style="margin-left: 5px;" />
        <br />
        <asp:ListBox size="4" runat="server" id="liTeamMember" CssClass="form-control liboxAddMember" style="margin-top: 6px;margin-bottom: 10px;width: 238px;"> </asp:ListBox>
        <div id="ButtonRM" class="btn btn-danger">Ta bort medlem</div>
        <br /><br />
    </asp:Panel>
    <asp:Panel ID="PanelTournaments" runat="server"></asp:Panel>
    <asp:Label ID="LabelTest" runat="server" Text=""></asp:Label>
    <asp:Panel ID="PanelBox" runat="server"></asp:Panel>
</form>
</asp:Content>