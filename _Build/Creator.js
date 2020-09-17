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
    let dialog = $(this).closest("dialog");
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


	//# sourceMappingUrl=Element.Dialog.js.map


﻿var styles = ["Bar", "Box"];
var barPositions = ["Top", "Bottom"];
var boxPositions = ["Top", "Bottom", "Left", "Right"];
var durations = { Three: 3, Five: 5, Eight: 8, Fourteen: 14, Twenty: 20, Permanent: -99 };

for (let style of styles) {
    jQuery.each(durations, function (duration, val) {
        for (let position of boxPositions) {
            handleNotifyElement(getElement(style, position, duration), parseInt(val.toString()));
        }
    });
}

function getElement(style, position, duration) {
    let result = ".notify-" + style + "-" + position + "-" + duration;

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
    }
    else {
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


	//# sourceMappingUrl=Element.Notify.js.map


var tabs = document.querySelectorAll(".tabs > .group > .tab");

tabs.forEach((tab) => {
	tab.addEventListener("click", () => {
		let contentId = tab.getAttribute("data-tab");

		tabs.forEach((t) => {
			t.classList.remove("active");
		});

		tab.classList.add("active");

		if (contentId) {
			document.querySelectorAll(".tabs > .content").forEach((cnt) => {
				if (cnt.id === contentId) {
					cnt.classList.add("active");
				}
				else {
					if (cnt.classList.contains("active")) {
						cnt.classList.remove("active");
					}
				}
			});
		}
	 });
});


	//# sourceMappingUrl=Element.Tabs.js.map


	//# sourceMappingUrl=Creator.js.map
