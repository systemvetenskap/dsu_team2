$(document).ready(function () {
    $("#ContentPlaceHolder1_TextBoxSSN").attr("maxlength", "10");
    $("#ContentPlaceHolder1_TextBoxSSN").on("keydown", function (e) {
        if (e.keyCode != 8) {
            if ($(this).val().length == 4) {
                var currentVal = $(this).val();
                currentVal = currentVal + "-";
                $(this).val(currentVal);
            } else if ($(this).val().length == 7) {
                var currentVal = $(this).val();
                currentVal = currentVal + "-";
                $(this).val(currentVal);
            }
        }
    });
    $("#ContentPlaceHolder1_TextZipcode").attr("maxlength", "6");
    $("#ContentPlaceHolder1_TextZipcode").on("keydown", function (e) {
        if (e.keyCode != 8) {
            if ($(this).val().length == 3) {
                var currentVal = $(this).val();
                currentVal = currentVal + " ";
                $(this).val(currentVal);
            }
        }
    });
    function isNumberKey(evt) {
        var charCode = (evt.which) ? evt.which : event.keyCode
        if (charCode > 31 && (charCode < 48 || charCode > 57))
            return false;

        return true;
    }
    function isValidEmailAddress(emailAddress) {
        var pattern = new RegExp(/^(("[\w-\s]+")|([\w-]+(?:\.[\w-]+)*)|("[\w-\s]+")([\w-]+(?:\.[\w-]+)*))(@((?:[\w-]+\.)*\w[\w-]{0,66})\.([a-z]{2,6}(?:\.[a-z]{2})?)$)|(@\[?((25[0-5]\.|2[0-4][0-9]\.|1[0-9]{2}\.|[0-9]{1,2}\.))((25[0-5]|2[0-4][0-9]|1[0-9]{2}|[0-9]{1,2})\.){2}(25[0-5]|2[0-4][0-9]|1[0-9]{2}|[0-9]{1,2})\]?$)/i);
        return pattern.test(emailAddress);
    };

    $(".uc-all").keyup(function () {
        var txt = $(this).val();
        $(this).val(txt.replace(/^(.)|\s(.)/g, function ($1) { return $1.toUpperCase(); }));
    });

    $(".uc-first").keyup(function () {
        var txt = $(this).val();
        $(this).val(txt.replace(/^(.)|\s(.)/g, function ($1) { return $1.toUpperCase(); }));
    });

    $("#ContentPlaceHolder1_TextZipCode").keyup(function () {
        this.value = this.value.replace(/[a-z]/g, '');
    });

    $("#ContentPlaceHolder1_TextBoxSSN").keyup(function () {
        this.value = this.value.replace(/[a-z]/g, '');
        this.value = this.value.replace(/\s+/g, '');
        this.value = this.value.replace(/[^\w-\d]/g, '');
    });

    $("#ContentPlaceHolder1_TextPhone").keyup(function () {
        this.value = this.value.replace(/[a-z]/g, '');
        this.value = this.value.replace(/\s+/g, '');
        this.value = this.value.replace(/[^\w\d]/g, '');
    });

    function waitLittleBit() {
        this.value = this.value.replace(/[a-z]/g, '');
        this.value = this.value.replace(/\s+/g, '');
        this.value = this.value.replace(/[^\w-\d]/g, '');
    }

    $("#ContentPlaceHolder1_TextPlace").keyup(function () {
        this.value = this.value.replace(/[0-9]/g, '');
    });

    $("#ContentPlaceHolder1_TextEmail").keyup(function () {
        var email = $("#ContentPlaceHolder1_TextEmail").val();
        if (isValidEmailAddress(email)) {
            $("#ContentPlaceHolder1_TextEmail").css("border", "1px solid green");
        } else {
            $("#ContentPlaceHolder1_TextEmail").css("border", "1px solid red");
        }
    });
    $(document).click(function () {
        var email = $("#ContentPlaceHolder1_TextEmail").val();
        if (isValidEmailAddress(email)) {
            $("#ContentPlaceHolder1_ButtonUppdate").attr("disabled", false);
        } else {
            $("#ContentPlaceHolder1_ButtonUppdate").attr("disabled", true);
        }
    });
    $(document).keyup(function () {
        var email = $("#ContentPlaceHolder1_TextEmail").val();
        if (isValidEmailAddress(email)) {
            $("#ContentPlaceHolder1_ButtonUppdate").attr("disabled", false);
            $("#ContentPlaceHolder1_TextEmail").css("border", "1px solid green");
        } else {
            $("#ContentPlaceHolder1_ButtonUppdate").attr("disabled", true);
            $("#ContentPlaceHolder1_TextEmail").css("border", "1px solid red");
        }
    });
});
