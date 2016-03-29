<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/headSite.Master" CodeBehind="closeLane.aspx.cs" Inherits="GolfBokning.stangbanan" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Mina sidor</title>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/css/bootstrap.min.css" />
    <link rel="stylesheet" href="StyleSheet.css" />

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
            $('#ContentPlaceHolder1_TextBoxStartdatum').datetimepicker({
                altField: "#ContentPlaceHolder1_TextBoxStarttid",
                dateFormat: 'yy-mm-dd',
                timeFormat: 'HH:mm',
                timeOnlyTitle: 'Välj starttid',
                timeText: 'Tid: ',
                hourText: 'Timme: ',
                minuteText: 'Minut: ',
                currentText: 'NU',
                closeText: 'OK',
            });
        });

        $(function () {
            $('#ContentPlaceHolder1_TextBoxSlutdatum').datetimepicker({
                altField: "#ContentPlaceHolder1_TextBoxSluttid",
                dateFormat: 'yy-mm-dd',
                timeFormat: 'HH:mm',
                timeOnlyTitle: 'Välj starttid',
                timeText: 'Tid: ',
                hourText: 'Timme: ',
                minuteText: 'Minut: ',
                currentText: 'NU',
                closeText: 'OK',
            });
        });
    </script>

</asp:Content>



<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" EnablePartialRendering="true" />
        
          <div class="containerMinaSidor">
           <div class="mainContent"> 

                  <div class="stangbanan"> 
                    <h3>Stäng banan</h3>

                    <div class="banastarttid">
                        <div class="startdatum">
                            <label>Startdatum: <span class="required">*</span></label><br />
                            <asp:TextBox ID="TextBoxStartdatum" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>

                        <div class="starttid">
                            <label>Starttid: <span class="required">*</span></label><br />
                            <asp:TextBox ID="TextBoxStarttid" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>

                    <div class="banasluttid">
                        <div class="slutdatum">
                            <label>Slutdatum: <span class="required">*</span></label><br />
                            <asp:TextBox ID="TextBoxSlutdatum" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>

                        <div class="sluttid">
                            <label>Sluttid: <span class="required">*</span></label><br />
                            <asp:TextBox ID="TextBoxSluttid" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                      <div class="ButtonClose">
                        <br /><asp:Button ID="ButtonCLoseLane" runat="server" Text="Stäng banan" CssClass="btn btn-primary" OnClick="ButtonCLoseLane_Click" />
                         <br /><asp:Label ID="LabelResponse" runat="server" Text=""></asp:Label>
                      </div>
                  </div>
            </div>
          </div>
   </form>
</asp:Content>
