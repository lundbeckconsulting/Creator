"use strict";

var styles = ["Bar", "Box"];
var barPositions = ["Top", "Bottom"];
var boxPositions = ["Top", "Bottom", "Left", "Right"];
var durations = { Three: 3, Five: 5, Eight: 8, Fourteen: 14, Twenty: 20, Permanent: -99 };

var _iteratorNormalCompletion = true;
var _didIteratorError = false;
var _iteratorError = undefined;

try {
    var _loop = function () {
        var style = _step.value;

        jQuery.each(durations, function (duration, val) {
            var _iteratorNormalCompletion2 = true;
            var _didIteratorError2 = false;
            var _iteratorError2 = undefined;

            try {
                for (var _iterator2 = boxPositions[Symbol.iterator](), _step2; !(_iteratorNormalCompletion2 = (_step2 = _iterator2.next()).done); _iteratorNormalCompletion2 = true) {
                    var position = _step2.value;

                    handleNotifyElement(getElement(style, position, duration), parseInt(val.toString()));
                }
            } catch (err) {
                _didIteratorError2 = true;
                _iteratorError2 = err;
            } finally {
                try {
                    if (!_iteratorNormalCompletion2 && _iterator2["return"]) {
                        _iterator2["return"]();
                    }
                } finally {
                    if (_didIteratorError2) {
                        throw _iteratorError2;
                    }
                }
            }
        });
    };

    for (var _iterator = styles[Symbol.iterator](), _step; !(_iteratorNormalCompletion = (_step = _iterator.next()).done); _iteratorNormalCompletion = true) {
        _loop();
    }
} catch (err) {
    _didIteratorError = true;
    _iteratorError = err;
} finally {
    try {
        if (!_iteratorNormalCompletion && _iterator["return"]) {
            _iterator["return"]();
        }
    } finally {
        if (_didIteratorError) {
            throw _iteratorError;
        }
    }
}

function getElement(style, position, duration) {
    var result = ".notify-" + style + "-" + position + "-" + duration;

    return result.toLowerCase();
}

function isBox(e) {
    return $(e).find(".box").length > 0;
}

function handleNotifyElement(element, dur) {
    if ($(element).hasClass("show")) {
        setInterval(function () {
            $(element).removeClass("show");
        }, dur * 1000);
    } else {
        $(element).on("notify:show", function (e) {
            $(element).fadeIn("slow", function () {
                $(element).addClass("show");

                if (dur != -99) {
                    setTimeout(function () {
                        $(element).removeClass("show");
                    }, dur * 1000);
                }
            });
        });
    }

    $(element).on("notify:close", function () {
        $(element).removeClass("show");
    });
}


"use strict";

$(".open-dialog[data-dialog]").click(function (e) {
    var dialogId = $(this).data("dialog");

    openDialog(dialogId);
});

$("dialog[class^=\"dialog-\"]").find("header .close-command").click(function (e) {
    var dialog = $(this).closest("dialog");

    $(dialog).trigger("command:close");

    closeDialog(dialog.attr("id"));
});

$("dialog[class^=\"dialog-\"]").find("footer .ok-command").click(function () {
    var dialog = $(this).closest("dialog");
    $(dialog).closest("dialog").trigger("command:ok");

    closeDialog(dialog.attr("id"));
});

$("dialog[class^='dialog-']").on("close", function (e) {
    closeDialog($(this).attr("id"));
});

$("dialog[class^='dialog-']").on("open", function (e) {
    openDialog($(this).attr("id"));
});

$("body").on("click", "#dialogBackground", function () {
    $("dialog").removeAttr("open");

    closeDialogBg();
});

$(document).keyup(function (e) {
    if (e.key === "Escape") {
        $("dialog").removeAttr("open");

        closeDialogBg();
    }
});

function openDialog(id) {
    removeDialogBg();

    $("body").append("<div id=\"dialogBackground\"></div>");

    $("#dialogBackground").fadeIn("slow", function () {
        $("#" + id).trigger("command:open");
        $("#" + id).attr("open", "open");
    });
};

function closeDialog(id) {
    $("#" + id).removeAttr("open");
    closeDialogBg();
};

function closeDialogBg() {
    $("#dialogBackground").fadeOut("slow", function () {
        removeDialogBg();
    });
};

function removeDialogBg() {
    $("body").remove("#dialogBackground");
};


"use strict";

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

