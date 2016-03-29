<%@ Page Language="C#" MasterPageFile="~/headSite.Master" AutoEventWireup="true" CodeBehind="tournament.aspx.cs" Inherits="GolfBokning.tournament" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        var ticks = {
            create: function (tp_inst, obj, unit, val, min, max, step) {
                $('<input class="ui-timepicker-input" value="' + val + '" style="width:50%">')
                    .appendTo(obj)
                    .spinner({
                        min: min,
                        max: max,
                        step: step,
                        change: function (e, ui) { // key events
                            // don't call if api was used and not key press
                            if (e.originalEvent !== undefined)
                                tp_inst._onTimeChange();
                            tp_inst._onSelectHandler();
                        },
                        spin: function (e, ui) { // spin events
                            tp_inst.control.value(tp_inst, obj, unit, ui.value);
                            tp_inst._onTimeChange();
                            tp_inst._onSelectHandler();
                        }
                    });
                return obj;
            },
            options: function (tp_inst, obj, unit, opts, val) {
                if (typeof (opts) == 'string' && val !== undefined)
                    return obj.find('.ui-timepicker-input').spinner(opts, val);
                return obj.find('.ui-timepicker-input').spinner(opts);
            },
            value: function (tp_inst, obj, unit, val) {
                if (val !== undefined)
                    return obj.find('.ui-timepicker-input').spinner('value', val);
                return obj.find('.ui-timepicker-input').spinner('value');
            }
        };

        $(function () {
            $("#ContentPlaceHolder1_TextBoxDatePicker").datepicker({
                dateFormat: 'yy-mm-dd'
            });
        });

        $(function () {
            $("#ContentPlaceHolder1_TextBoxPublish").datetimepicker({
                controlType: ticks,
                timeFormat: 'HH:mm',
                timeOnlyTitle: 'Välj starttid',
                timeText: 'Tid: ',
                hourText: 'Timme: ',
                minuteText: 'Minut: ',
                secondText: 'Sekund: ',
                currentText: 'NU',
                closeText: 'OK',
                dateFormat: 'yy-mm-dd'
            });
        });
        $(function () {
            $("#ContentPlaceHolder1_TextBoxStart").timepicker({
                controlType: ticks,
                timeFormat: 'HH:mm',
                timeOnlyTitle: 'Välj starttid',
                timeText: 'Tid: ',
                hourText: 'Timme: ',
                minuteText: 'Minut: ',
                secondText: 'Sekund: ',
                currentText: 'NU',
                closeText: 'OK',
                addSliderAccess: true,
                sliderAccessArgs: { touchonly: false }
            });
        });
        $(function () {
            $("#ContentPlaceHolder1_TextBoxStop").timepicker({
                controlType: ticks,
                timeFormat: 'HH:mm',
                timeOnlyTitle: 'Välj sluttid',
                timeText: 'Tid: ',
                hourText: 'Timme: ',
                minuteText: 'Minut: ',
                secondText: 'Sekund: ',
                currentText: 'NU',
                closeText: 'OK',
                stepMinute: 10
            });            
                       
        });

        $(function () {
            var sel = $(this).val();
            $('#ContentPlaceHolder1_rbtn_singelgender').hide();
            $('#ContentPlaceHolder1_Label16').hide();

            $("#ContentPlaceHolder1_DropDownListClass").change(function () {                
                var sel = $(this).val();

                if (sel === '2') {
                    $('#ContentPlaceHolder1_rbtn_singelgender').show();
                    $('#ContentPlaceHolder1_Label16').show();
                }
                else {
                    $('#ContentPlaceHolder1_rbtn_singelgender').hide();
                    $('#ContentPlaceHolder1_Label16').hide();
                }
                
            });
        });
        
        $(function () {
            var availableTags = [ <%= SuggestionList %>];

            $("#<%= TextBoxContact.ClientID %>").autocomplete({
                source: availableTags
            });
        });
    </script>
    <style>
        #ContentPlaceHolder1_PanelStartStop1 {
            float: left;
            width: 48%;
            margin-right: 4%;
        }
        #ContentPlaceHolder1_PanelStartStop2 {
            float: left;
            width: 48%;
        }
        .hide-me {
            display: none;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Hantera tävling</h1>
    <form id="formTournament" runat="server">
    <asp:ScriptManager ID="ScriptManagerTournament" EnablePartialRendering="true" runat="server"></asp:ScriptManager>
        <asp:Label ID="LabelID" runat="server" Text="" CssClass="hide-me"></asp:Label>
        <asp:Label ID="Label9" runat="server" Text="Välj tävling"></asp:Label><br />
        <asp:DropDownList ID="DropDownListTournament" runat="server" CssClass="form-control"></asp:DropDownList><br />
        <asp:Button ID="ButtonRemove" runat="server" Text="Ta bort" CssClass="btn btn-danger"/><asp:Button ID="ButtonCreateNew" runat="server" Text="Skapa ny" CssClass="btn btn-success btn-right" /><br /><br />
        <asp:Panel ID="PanelNew" runat="server">
            <asp:Label ID="Label13" runat="server" Text="<h2>Information</h2>"></asp:Label><br />
            <asp:Label ID="Label1" runat="server" Text="Namn"></asp:Label><br />
            <asp:TextBox ID="TextBoxName" runat="server" CssClass="form-control" required="required"></asp:TextBox><br />
            <asp:Label ID="Label7" runat="server" Text="Beskrivning"></asp:Label><br />
            <asp:TextBox ID="TextBoxDescription" TextMode="multiline" runat="server" Columns="50" Rows="5" CssClass="form-control"></asp:TextBox><br /><br /><br />
            <asp:Label ID="Label14" runat="server" Text="<h2>Tider</h2>"></asp:Label><br />
            <asp:Label ID="Label8" runat="server" Text="Datum för tävling"></asp:Label><br />
            <asp:TextBox ID="TextBoxDatePicker" runat="server" CssClass="form-control" required="required"></asp:TextBox><br />
            <asp:Panel ID="PanelStartStop1" runat="server">
                <asp:Label ID="Label10" runat="server" Text="Starttid"></asp:Label>
                <asp:TextBox ID="TextBoxStart" runat="server" CssClass="form-control" required="required"></asp:TextBox>
            </asp:Panel>
            <asp:Panel ID="PanelStartStop2" runat="server">
                <asp:Label ID="Label11" runat="server" Text="Sluttid"></asp:Label>
                <asp:TextBox ID="TextBoxStop" runat="server" CssClass="form-control" required="required"></asp:TextBox><br />
            </asp:Panel>
            <asp:Label ID="Label12" runat="server" Text="Tid för att publicera tävling"></asp:Label>
            <asp:TextBox ID="TextBoxPublish" runat="server" CssClass="form-control" required="required"></asp:TextBox><br /><br /><br />
            <asp:Label ID="Label15" runat="server" Text="<h2>Inställningar</h2>"></asp:Label><br />
            <asp:Label ID="Label2" runat="server" Text="Klass"></asp:Label><br />
            <asp:DropDownList ID="DropDownListClass" runat="server" CssClass="form-control"></asp:DropDownList><br />
            <asp:Label ID="Label16" runat="server" Text="Välj kön för singeltävling"></asp:Label><br />
            <asp:RadioButtonList ID="rbtn_singelgender" runat="server" Visible="True">
                <asp:ListItem>Male</asp:ListItem>
                <asp:ListItem>Female</asp:ListItem>
            </asp:RadioButtonList><br />           
            <asp:Label ID="Label5" runat="server" Text="Handikappgräns"></asp:Label><br />
            <asp:TextBox ID="TextBoxHCP" runat="server" CssClass="form-control" required="required"></asp:TextBox><br />
            <asp:Label ID="Label3" runat="server" Text="Antal deltagare"></asp:Label><br />
            <asp:TextBox ID="TextBoxNbrCompetitors" runat="server" CssClass="form-control" required="required"></asp:TextBox><br />
            <asp:Label ID="Label4" runat="server" Text="Antal hål"></asp:Label><br />
            <asp:TextBox ID="TextBoxNbrHoles" runat="server" CssClass="form-control" required="required"></asp:TextBox><br />
            <asp:Label ID="Label6" runat="server" Text="Kontaktperson"></asp:Label><br />

            <asp:TextBox ID="TextBoxContact" runat="server" CssClass="form-control" required="required"></asp:TextBox><br />
            <asp:Button ID="ButtonCreate" runat="server" Text="Skapa" CssClass="btn btn-primary" /><br /><br />
            <asp:Button ID="ButtonUpdate" runat="server" Text="Uppdatera" CssClass="btn btn-primary" /><br /><br />
        </asp:Panel>
            <asp:Panel ID="PanelResponse" runat="server" CssClass="alert alert-success">
                <asp:Label ID="LabelResponse" runat="server" Text=""></asp:Label>
            </asp:Panel>
        <asp:LinkButton ID="LinkButton1" runat="server" CssClass="btn btn-primary" href="tournamentdraw.aspx">Lotta Starttider</asp:LinkButton>
        <asp:Panel ID="PanelBox" runat="server"></asp:Panel>
    </form>
</asp:Content>