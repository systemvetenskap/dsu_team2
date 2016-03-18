// On document ready
$(document).ready(function () {
    $(".logoutClick").click(function () {
        var request = $.ajax({
            type: "POST",
            url: "logout.aspx",
            traditional: true
        });
        request.done(function (response) {
            alert("Du har nu loggat ut!");
            location.href = "loginform.aspx";
        });
    });
});
function makeBiggerRegister()
{
    if (document.getElementById("ContentPlaceHolder1_PanelMySideUpdate").style.height == "auto")
    {
        document.getElementById("ContentPlaceHolder1_PanelMySideUpdate").style.height = "20px";
    }
    else
    {
        document.getElementById("ContentPlaceHolder1_PanelMySideUpdate").style.height = "auto";
    }
  
}