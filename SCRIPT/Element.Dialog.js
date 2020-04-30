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
