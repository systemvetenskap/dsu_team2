$(document).ready(function () {

    $("#LabelHiddenLeader").hide();

    $(".menu").click(function (e) {
        $(".menu-popup").slideToggle("slow", function () {

        });
    });

    $(".menu-popup ul li").click(function () {
        $(".menu-popup").slideUp("slow");
    });

    $(".showLeaderBoard").click(function () {
        waitSlideDown(400);
    });

    $(".showLeaderBoardBox").click(function () {
        waitSlideDown(400);
    });

    function waitSlideDown(time) {
        setTimeout(mySlideDown, time);
    }

    function mySlideDown() {
        $(".leaderboard").slideDown("slow");
    }

    $(".closeBox").click(function () {
        $(".leaderboard").slideUp("slow");
    });
});