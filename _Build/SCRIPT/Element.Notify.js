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

