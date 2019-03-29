$(document).ready(function () {
    if ($("footer.creator").hasClass("sticky") || $("footer").hasClass("creator-sticky")) {
        var resizeTimer;

        $(window).on('resize', function (e) {
            clearTimeout(resizeTimer);
            resizeTimer = setTimeout(function () {
                handleResize();
            }, 250);
        });
    }

    function handleResize() {
        if ($(body).height > $(window).height) {
            $(".footer-sticky, .footer.sticky").removeClass("do-sticky");
        }
        else {
            $(".footer-sticky, .footer.sticky").addClass("do-sticky");
        }
    }
});