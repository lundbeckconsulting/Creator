$(function () {
    $(".show-modal[data-modal]").click(function (e) {
        let modalId = $(this).data("modal");

        $("#" + modalId).trigger("modal:show");
    });

    $("[class^='modal']").on("modal:show", function (e) {
        let $modal = this;

        let $bg = "<div id=\"modalBackground\"></div>";

        $("body").append($bg);

        $("#modalBackground").fadeIn("fast", function () {
            $($modal).fadeIn("fast");
        });
    });

    $(".hide-modal").click(function (e) {
        CloseModal();
    });

    $("[class^='modal']").on("modal:hide", function (e) {
        CloseModal();
    });

    $(document).on("click", "#modalBackground", function () {
        CloseModal();
    });

    $(document).on("keydown", function (e) {
        if (e.keyCode === 27) {
            CloseModal();
        } 
    });

    function CloseModal() {
        $("[class^='modal']").hide();
        $("#modalBackground").fadeOut("slow", function () {
            $("#modalBackground").remove();
        });
    }
});