$(document).ready(function () {
    $("#ContentPlaceHolder1_TextSSN").attr("maxlength", "10");
    $("#ContentPlaceHolder1_TextSSN").on("keydown", function (e) {
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
});
