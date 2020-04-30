$("#burgerIcon").click(function () {
    $("body").append("<div id=\"burgerMenuBackground\"></div>");
    
    $("#burgerMenuBackground").fadeIn("slow", function () {
        $("#burgerMenu").fadeIn("slow", function () {
            $("#burgerMenuBackground").append("<span class=\"close-burger-menu\">X</span>");
        }); 
    });
});

$("body").on("click", "#burgerMenuBackground", function () {
    closeMenu();
});

$("body").on("click", ".close-burger-menu", function () {
    closeMenu();
});

function closeMenu() {
    $("#burgerMenu").fadeOut("slow", function () {
        $("#burgerMenuBackground").fadeOut("fast", function () {
            $("#burgerMenuBackground").remove(".close-burger-menu");
            $("body").remove("#burgerMenuBackground");
        });
    });
}
