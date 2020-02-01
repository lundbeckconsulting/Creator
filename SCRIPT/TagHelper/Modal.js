$(document).ready(() => {
    function CloseModalBackground() {
        $("#modalBackground").fadeOut("fast", function () {
            $("#modalBackground").remove();
        });
    }

    function TriggerCloseModal($modal) {
        $($modal).trigger("modal:hidden");
    }

    function DoCloseModal($modal) {
        $($modal).removeAttr("open");
        TriggerCloseModal($modal);
    }

    function CloseModal(e, hideBG = true) {
        var $modal = $(e.currentTarget).closest("[class^=\"modal-\"]");
        DoCloseModal($modal);

        if (hideBG) {
            CloseModalBackground();
        }
    }

    function CloseAllModals() {
        $("[class^=\"modal-\"][open=\"open\"]").each(function () {
            DoCloseModal(this);
        });

        CloseModalBackground();
    }

    $(".show-modal[data-modal]").click(function (e) {
        var modalId = $(this).data("modal");

        $("#" + modalId).trigger("modal:show");
    });

    $("[class^=\"modal-\"]").on("modal:show", function (e) {
        // eslint-disable-next-line consistent-this
        var $modal = this;
        var $bg = "<div id=\"modalBackground\"></div>";

        $("body").append($bg);

        $("#modalBackground").fadeIn("slow", function () {
            $($modal).attr("open", "open");
            $($modal).trigger("modal:visible");

            $("html, body").animate({
                scrollTop: 0
            }, 600);
        });
    });

    $(".hide-modal").click(function (e) {
        CloseModal(e);
    });

    $("[class^=\"modal-\"]").on("modal:hide", function (e) {
        CloseModal(e);
    });

    $(document).on("click", "#modalBackground", function () {
        CloseAllModals();
    });

    $(document).on("keydown", function (e) {
        if (e.keyCode === 27) {
            CloseAllModals();
        }
    });
});