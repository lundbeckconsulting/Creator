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


"use strict";

$(document).ready(function () {
    var resizeTimer;
    var $media = $("#creator-media");
    var $mediaOrientation = $("#creator-media-orientation");

    (function Init() {
        InitMedia();
    })();

    $(window).on('resize', function (e) {
        HandleResize();
    });

    function InitMedia() {
        $media.data("base", $media.css("z-index"));
        $mediaOrientation.data("base", $media.css("z-index"));

        HandleResize(true);
    };

    function HandleResize() {
        var forceTrigger = arguments.length <= 0 || arguments[0] === undefined ? false : arguments[0];

        clearTimeout(resizeTimer);

        resizeTimer = setTimeout(function () {
            var runTrigger = false;

            if ($media.data("base") !== $media.css("z-index")) {
                $media.data("base", $media.css("z-index"));

                runTrigger = true;
            }

            if ($mediaOrientation.data("base") !== $mediaOrientation.css("z-index")) {
                $mediaOrientation.data("base", $mediaOrientation.css("z-index"));

                runTrigger = true;
            }

            if (runTrigger || forceTrigger) {
                var sizeInt = $media.css("z-index");
                var size = "xs";
                var orientation = "landscape";

                switch (parseInt($media.css("z-index"))) {
                    case 1:
                        size = "sm";
                        break;

                    case 2:
                        size = "md";
                        break;

                    case 3:
                        size = "lg";
                        break;

                    case 4:
                        size = "xl";
                        break;
                }

                switch ($mediaOrientation.css("z-index")) {
                    case 1:
                        orientation = "portrait";
                        break;

                    case 2:
                        orientation = "landscape";
                        break;
                }

                HandleRows(size);

                $media.trigger("resize", [sizeInt, size, orientation]);
            }
        }, 250);
    }

    function HandleRows(size) {}
});


'use strict';

var C = {
    Query: {
        exists: function exists(name) {
            var result = location.search.indexOf(name + "=") > 0;

            return result;
        },
        param: function param(name) {
            name = name.replace(/[\[]/, '\\[').replace(/[\]]/, '\\]');

            var regex = new RegExp('[\\?&]' + name + '=([^&#]*)');
            var result = regex.exec(location.search);

            return result === null ? '' : decodeURIComponent(result[1].replace(/\+/g, ' '));
        }
    },
    DateTime: {
        Current: {
            date: function date() {
                return new Date();
            },
            time: function time() {
                return C.DateTime.Current.date().getTime();
            },
            long: function long() {
                return C.DateTime.toLongDate(C.DateTime.date());
            },
            short: function short() {
                return C.DateTime.toShortDate(C.DateTime.date());
            }
        },
        toLongDate: function toLongDate(date) {
            var result = C.DateTime.toShortDate(date) + " " + date.getUTCHours() + ":" + date.getUTCMinutes() + "." + date.getUTCSeconds();

            return result;
        },
        toShortDate: function toShortDate(date) {
            var result = date.getUTCDay() + "." + (date.getUTCMonth() + 1) + "." + date.getUTCFullYear();

            return result;
        },
        subtractDays: function subtractDays(days) {
            var result = C.DateTime.Current.date;

            result.setDate(result.getDate() - days);

            return result;
        }
    },
    Common: {
        siteId: function siteId() {
            var result = $("#lc-common-site-id").val();

            return parseInt(result);
        },
        appId: function appId(callback) {
            if (C.Common.siteId() > 0) {
                C.Ajax.Invoke(null, "Site", "GetSite", { "id": C.Common.siteId() }, null, "GET", function (result) {
                    if (!C.State.isEmpty(callback)) {
                        callback(result.id);
                    }
                });
            }
        },
        isDevelopment: function isDevelopment() {
            var result = -1;
            var $elm = $("#lc-common-is-development");

            if (!C.State.isEmpty($elm)) {
                if ($.isNumeric($elm.val())) {
                    result = parseInt($elm.val());
                }
            }

            return C.Cast.toBool(result);
        },
        creds: function creds(callback) {
            var url = "/SECURE/creds.json";

            if (C.Utility.fileExists(url)) {
                C.API.Ajax.GetJSON(url, success);
            } else {
                C.Console.error("creds.json doesn't exists. \"" + url + "\"");
            }

            function success(data) {
                callback(data);
            }
        }
    },
    Utility: {
        randomInterval: function randomInterval(min, max) {
            return Math.floor(Math.random() * (max - min + 1) + min);
        },
        fileExists: function fileExists(url) {
            var result = false;
            var xhr = new XMLHttpRequest();
            xhr.open('HEAD', url, false);
            xhr.send();

            if (xhr.status !== 404) {
                result = true;
            }

            xhr = null;

            return result;
        }
    },
    Console: {
        basic: function basic(content) {
            C.Console.invoke(C.Const.ConsoleTypes.BASIC, content);
        },
        debug: function debug(content) {
            C.Console.invoke(C.Const.ConsoleTypes.DEBUG, content);
        },
        error: function error(content) {
            C.Console.invoke(C.Const.ConsoleTypes.ERROR, content);
        },
        invoke: function invoke(type, content) {
            if (C.Common.isDevelopment()) {
                switch (type) {
                    case C.Const.ConsoleTypes.BASIC:
                        console.log(content);
                        break;

                    case C.Const.ConsoleTypes.DEBUG:
                        console.debug(content);
                        break;

                    case C.Const.ConsoleTypes.ERROR:
                        console.error(content);
                        break;
                }
            }
        }
    },
    Log: {
        info: function info(content) {
            C.Log.invoke(C.Const.LogTypes.INFO, content);
        },
        warning: function warning(content) {
            C.Log.invoke(C.Const.LogTypes.WARNING, content);
        },
        exception: function exception(content) {
            var params = arguments.length <= 1 || arguments[1] === undefined ? {} : arguments[1];

            C.Log.invoke(C.Const.LogTypes.EXCEPTION, content, params);
        },
        invoke: function invoke(type, content) {
            var params = arguments.length <= 2 || arguments[2] === undefined ? {} : arguments[2];
        }
    },
    State: {
        isEmpty: function isEmpty(val) {
            var result = true;

            if (typeof val !== "undefined" && val !== null) {
                result = val.length === 0;
            }

            return result;
        },
        isEqual: function isEqual(val1, val2) {
            return val1 === val2;
        },
        isEmail: function isEmail(email) {
            var result = false;
            var reg = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;

            if (reg.test(email)) {
                result = true;
            }

            return result;
        },
        isURL: function isURL(url) {
            var result = false;
            var reg = /^(?:http(s)?:\/\/)?[\w.-]+(?:\.[\w\.-]+)+[\w\-\._~:/?#[\]@!\$&'\(\)\*\+,;=.]+$/;

            if (reg.test(url)) {
                result = true;
            }

            return result;
        },
        isBool: function isBool(val) {
            var result = false;

            switch (val.toLowerCase()) {
                case "true":
                case "false":
                case "1":
                case "0":
                    result = true;
                    break;
            }

            return result;
        }
    },
    Math: {
        roundUp: function roundUp(val) {
            var result = this;
            var separator = undefined;

            if (!$.isNumeric(val)) {
                C.Console.error("RoundUp", "Supplied value \"" + val + "\" not numeric");
            } else {
                if (val.indexOf(".") > 0) {
                    separator = ".";
                } else if (val.indexOf(",") > 0) {
                    separator = ",";
                }

                if (!separator.IsNull()) {
                    var tmp1 = undefined,
                        tmp2 = undefined;

                    tmp1 = parseInt(val.substring(0, val.indexOf(separator)));
                    tmp2 = parseInt(val.substring(val.indexOf(separator)));

                    if (tmp >= 50) {
                        result = tmp1 + 1;
                    }
                }
            }

            return result;
        }
    },
    Cast: {
        toBool: function toBool(val) {
            var result = false;

            switch (String(val).toLowerCase()) {
                case "true":
                case "1":
                    result = true;
                    break;
            }

            return result;
        },
        toCapitalCase: function toCapitalCase(val) {
            var result = undefined;

            if (!LC.PUB.State.isEmpty(val)) {
                result = val.substring(0, 1).toUpperCase() + val.substring(1);
            }

            return result;
        },
        toCamelCase: function toCamelCase(val) {
            var result = undefined;
            var vals = val.Split(" ");
            var i = 0;

            $.each(vals, function (val) {
                var word = C.State.toCapitalCase(val);

                result += i === 0 ? word : " " + word;

                i++;
            });

            return result;
        },
        toCamelCaseMerged: function toCamelCaseMerged(val) {
            return C.Cast.ToCamelCase(val).replace(" ", "");
        }
    },
    Cookie: {
        Get: function Get(name) {
            return Cookies.Get(name);
        },
        Set: function Set(name, value) {
            var expire = arguments.length <= 2 || arguments[2] === undefined ? 180 : arguments[2];

            Cookies.Set(name, value, { expires: expire, path: "" });
        },
        Delete: function Delete(name) {
            Cookies.Remove(name, { expires: -1, path: "" });
        },
        DeleteAll: function DeleteAll() {
            var cookies = [];

            var i = 1;

            $.each(Cookies.get(), function (cookie) {
                var name = cookie.name;

                C.Console.basic("Processing " + name);

                C.Cookie.Delete(name);
                result.push(name);

                i++;
            });

            C.Console.basic(i + " cookies deleted");
        }
    },
    Ajax: {
        Invoke: function Invoke() {
            var host = arguments.length <= 0 || arguments[0] === undefined ? null : arguments[0];
            var controller = arguments.length <= 1 || arguments[1] === undefined ? null : arguments[1];
            var action = arguments.length <= 2 || arguments[2] === undefined ? null : arguments[2];
            var query = arguments.length <= 3 || arguments[3] === undefined ? {} : arguments[3];
            var data = arguments.length <= 4 || arguments[4] === undefined ? {} : arguments[4];
            var type = arguments.length <= 5 || arguments[5] === undefined ? "GET" : arguments[5];
            var callback = arguments.length <= 6 || arguments[6] === undefined ? null : arguments[6];

            var url = C.Ajax.GetUrl(host, controller, action, query);
            var timeStart = new Date();

            $.ajax({
                type: type.toUpperCase(),
                url: url,
                data: JSON.stringify(data),
                dataType: "JSON",
                contentType: "application/json",
                success: function success(result) {
                    C.Console.basic({ duration: (new Date() - timeStart) / 1000, url: url, data: data });

                    if (!C.State.isEmpty(callback)) {
                        callback(result);
                    }
                },
                error: function error(xhr, status, _error) {
                    C.Console.error({ error: _error });
                    C.Console.error({ status: status });
                    C.Console.error({ xhr: xhr });
                }
            });
        },
        GetJSON: function GetJSON(host, controller, action, query, callback) {
            if (host === undefined) host = null;
            if (controller === undefined) controller = null;
            if (action === undefined) action = null;
            if (query === undefined) query = {};

            $.getJSON(C.Ajax.GetUrl(host, controller, action, query), success);

            function success(data) {
                callback(data);
            }
        },
        GetUrl: function GetUrl(host, controller, action) {
            var query = arguments.length <= 3 || arguments[3] === undefined ? {} : arguments[3];

            var result = "";

            if (!C.State.isEmpty(host)) {
                result = host;
            }

            if (!C.State.isEmpty(controller)) {
                result += "/" + controller;
            }

            if (!C.State.isEmpty(action)) {
                result += "/" + action;
            }

            var tmp = undefined;
            var i = 0;

            $.each(query, function (key, value) {
                if (i === 0) {
                    tmp = "?";
                } else {
                    tmp += "&";
                }

                tmp += key + "=" + value;

                i++;
            });

            if (!C.State.isEmpty(tmp)) {
                result += tmp;
            }

            return result.toLowerCase();
        }
    },
    Const: {
        ModalBlackoutName: "modalBlackout",
        ConsoleTypes: {
            BASIC: 0,
            DEBUG: 1,
            ERROR: 2
        },
        MediaSizes: {
            XS: 0,
            MD: 1,
            LG: 2,
            XL: 3
        },
        MediaOrientation: {
            Portrait: 0,
            Landscape: 1
        },
        LogTypes: {
            INFO: 0,
            WARNING: 1,
            EXCEPTION: 2
        }
    }
};

(function ($) {
    $.fn.ToBool = function () {
        var result = C.Cast.toBool(this.val());

        return result;
    };

    $.fn.IsNumeric = function () {
        return $.isNumeric(this.val());
    };

    $.fn.IsEqual = function (val) {
        return C.State.isEqual(val, this.val());
    };

    $.fn.IsBool = function () {
        return C.State.isBool(this.val());
    };

    $.fn.IsEmail = function () {
        return C.State.isEmail(this.val());
    };

    $.fn.IsEmailAsInt = function () {
        var result = 0;

        if (this.IsEmail()) {
            result = 1;
        }

        return result;
    };

    $.fn.IsURL = function () {
        return C.State.isURL(this.val());
    };

    $.fn.IsURLAsInt = function () {
        var result = 0;

        if (this.IsURL()) {
            result = 1;
        }

        return result;
    };

    $.fn.IsEmpty = function () {
        var result = C.State.isEmpty(this.val());

        return result;
    };

    $.fn.ToCapitalCase = function () {
        return C.Cast.toCapitalCase(this.val());
    };

    $.fn.ToCamelCase = function () {
        return C.Cast.toCamelCase(this.val());
    };

    $.fn.ToCamelCaseMerged = function () {
        return C.Cast.toCamelCaseMerged(this.val());
    };

    $.fn.StartsWith = function (str) {
        return this.val().startsWith(str);
    };

    $.fn.EndsWith = function (str) {
        return this.val().endsWith(str);
    };

    $.fn.SetCookie = function (name) {
        var expires = arguments.length <= 1 || arguments[1] === undefined ? 180 : arguments[1];

        C.Cookie.Set(name, this.val(), expires);
    };
})(jQuery);