"use strict";

$(document).ready(function () {
    var $modalBlack;
    var modalWild = "[class^='modal']",
        modalWildVisible = modalWild + ":visible";

    (function Init() {
        InitModal();
    })();

    function InitModal() {
        if ($(modalWild).length > 0) {
            var $elm = document.createElement("div");
            $elm.id = C.Const.ModalBlackoutName;

            $("body").prepend($elm);

            $modalBlack = $("#" + C.Const.ModalBlackoutName);
        }
    }

    $(".modal").on("modal:show", function (event) {
        var $modal = this;

        $modalBlack.fadeIn("slow", function () {
            $($modal).fadeIn("fast", function () {
                $($modal).trigger("modal:visible");
            });
        });
    });

    $(".modal").on("modal:hide", function (event) {
        CloseAllModals();
    });

    $(".show-modal").click(function (event) {
        event.preventDefault();

        var idModal = "#" + $(this).data("modal");

        $(idModal).trigger("modal:show");
    });

    $(".close-modal").click(function (event) {
        event.preventDefault();

        $(".modal").trigger("modal:hide");
    });

    $("#" + C.Const.ModalBlackoutName).click(function () {
        CloseAllModals();
    });

    $(document).keyup(function (key) {
        if (key.keyCode === 27) {
            if ($modalBlack.is(":visible")) {
                CloseAllModals();
            }
        }
    });

    function CloseModal($modal) {
        var callback = arguments.length <= 1 || arguments[1] === undefined ? null : arguments[1];

        $($modal).fadeOut("fast", function () {
            $($modal).trigger("modal:close");

            if ($(modalWildVisible).length == 0) {
                $modalBlack.fadeOut("slow", function () {
                    fireCallback();
                });
            } else {
                fireCallback();
            }

            function fireCallback() {
                if (callback != null) {
                    callback();
                }
            }
        });
    }

    function CloseAllModals() {
        var $modals = $(modalWildVisible);

        $.each($modals, function (key, value) {
            CloseModal(this);
        });
    }
});

