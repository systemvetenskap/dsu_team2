﻿// On document ready
$(document).ready(function () {
    $(".logoutClick").click(function () {
        var request = $.ajax({
            type: "POST",
            url: "logout.aspx",
            traditional: true
        });
        request.done(function (response) {
            location.href = "loginform.aspx";
        });
    });

    if ($(".hiddenPage").html() != "startside.aspx" && $(".hiddenPage").html() != "loginform.aspx" && $(".hiddenPage").html() != "register.aspx" && $(".hiddenPage").html() != "personalverktyg.aspx"
         && $(".hiddenPage").html() != "member.aspx" && $(".hiddenPage").html() != "tournament.aspx" && $(".hiddenPage").html() != "addResults.aspx"
        && $(".hiddenPage").html() != "closeLane.aspx" && $(".hiddenPage").html() != "news.aspx" && $(".hiddenPage").html() != "sendMail.aspx"
        && $(".hiddenPage").html() != "staffmember.aspx" && $(".hiddenPage").html() != "tournamentpers.aspx" && $(".hiddenPage").html() != "booking.aspx"
        && $(".hiddenPage").html() != "unbook.aspx")
    {
        $(".localNavigation").show();
    }


    else if ($(".hiddenPage").html() != "startside.aspx" && $(".hiddenPage").html() != "loginform.aspx" && $(".hiddenPage").html() != "register.aspx" && $(".hiddenPage").html() != "minasidor.aspx"
       && $(".hiddenPage").html() != "booking.aspx" && $(".hiddenPage").html() != "tournamentresult.aspx" && $(".hiddenPage").html() != "tournamententry.aspx" && $(".hiddenPage").html() != "unbook.aspx"
        && $(".hiddenPage").html() != "chatt.aspx") {
        $(".localNavigation2").show();
    }

    if ($(".hiddenPage").length == 0) {
        $(".localNavigation2").show();
    }

    //if ($(".hiddenPage").html() != "startside.aspx" && $(".hiddenPage").html() != "loginform.aspx" && $(".hiddenPage").html() != "register.aspx") {
    //    $(".localNavigation").show();
    //}

    var bookingFind = $("body").find("#ContentPlaceHolder1_bookingtable");

    if (bookingFind.length) {
        //$(".localNavigation").hide();
    }

    $("#ContentPlaceHolder1_TextEmailLogin").keyup(function () {
        var email = $("#ContentPlaceHolder1_TextEmailLogin").val();
        if (isValidEmailAddress(email)) {
            $("#ContentPlaceHolder1_TextEmailLogin").css("border", "1px solid green");
        } else {
            $("#ContentPlaceHolder1_TextEmailLogin").css("border", "1px solid red");
        }
    });

    $("#ContentPlaceHolder1_TextZipCode").attr("maxlength", "6");
    $("#ContentPlaceHolder1_TextZipCode").on("keydown", function (e) {
        if (e.keyCode != 8) {
            if ($(this).val().length == 3) {
                var currentVal = $(this).val();
                currentVal = currentVal + " ";
                $(this).val(currentVal);
            }
        }
    });

    $(document).click(function () {
        var email = $("#ContentPlaceHolder1_TextEmailLogin").val();
        if (isValidEmailAddress(email)) {
            $("#ContentPlaceHolder1_ButtonLogin").attr("disabled", false);
        } else {
            $("#ContentPlaceHolder1_ButtonLogin").attr("disabled", true);
        }
    });

    $("#ContentPlaceHolder1_ButtonLogin").css("disabled", true);

    $(document).keyup(function () {
        var email = $("#ContentPlaceHolder1_TextEmailLogin").val();
        if (isValidEmailAddress(email)) {
            $("#ContentPlaceHolder1_ButtonLogin").attr("disabled", false);
            $("#ContentPlaceHolder1_TextEmailLogin").css("border", "1px solid green");
        } else {
            $("#ContentPlaceHolder1_ButtonLogin").attr("disabled", true);
            $("#ContentPlaceHolder1_TextEmailLogin").css("border", "1px solid red");
        }
    });
});

function isValidEmailAddress(emailAddress) {
    var pattern = new RegExp(/^(("[\w-\s]+")|([\w-]+(?:\.[\w-]+)*)|("[\w-\s]+")([\w-]+(?:\.[\w-]+)*))(@((?:[\w-]+\.)*\w[\w-]{0,66})\.([a-z]{2,6}(?:\.[a-z]{2})?)$)|(@\[?((25[0-5]\.|2[0-4][0-9]\.|1[0-9]{2}\.|[0-9]{1,2}\.))((25[0-5]|2[0-4][0-9]|1[0-9]{2}|[0-9]{1,2})\.){2}(25[0-5]|2[0-4][0-9]|1[0-9]{2}|[0-9]{1,2})\]?$)/i);
    return pattern.test(emailAddress);
};



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

function GetCookieID() {
    var cookieColl = $.cookie("LoginCookie");
    var cookieArr = cookieColl.split('&');
    for (i = 0; i < cookieArr.length; i++) {
        var cookie = cookieArr[i].split('=');
        if (cookie[0].indexOf("_id") !== -1) {
            if (cookie[0] === "_id") {
                var output = cookie[1];
            }
        }
    }
    return output;
}