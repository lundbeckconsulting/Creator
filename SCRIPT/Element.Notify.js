var styles = ["Bar", "Box"];
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