var C = {
    Query:  {
        exists: function (name) {
            let result = location.search.indexOf(name + "=") > 0;

            return result;
        },
        param: function (name) {
            name = name.replace(/[\[]/, '\\[').replace(/[\]]/, '\\]');

            var regex = new RegExp('[\\?&]' + name + '=([^&#]*)');
            var result = regex.exec(location.search);

            return result === null ? '' : decodeURIComponent(result[1].replace(/\+/g, ' '));
        }
    },
    DateTime: {
        Current: {
            date: function () {
                return new Date();
            },
            time: function () {
                return C.DateTime.Current.date().getTime();
            },
            long: function () {
                return C.DateTime.toLongDate(C.DateTime.date());
            },
            short: function () {
                return C.DateTime.toShortDate(C.DateTime.date());
            }
        },
        toLongDate: function (date) {
            let result = C.DateTime.toShortDate(date) + " " + date.getUTCHours() + ":" + date.getUTCMinutes() + "." + date.getUTCSeconds();

            return result;
        },
        toShortDate: function (date) {
            let result = date.getUTCDay() + "." + (date.getUTCMonth() + 1) + "." + date.getUTCFullYear();

            return result;
        },
        subtractDays: function (days) {
            let result = C.DateTime.Current.date;

            result.setDate(result.getDate() - days);

            return result;
        }
    },
    Common: {
        siteId: function () {
            let result = $("#lc-common-site-id").val();

            return parseInt(result);
        },
        appId: function (callback) {
            if (C.Common.siteId() > 0) {
                C.Ajax.Invoke(null, "Site", "GetSite", { "id": C.Common.siteId() }, null, "GET", function (result) {
                    if (!C.State.isEmpty(callback)) {
                        callback(result.id);
                    }
                });
            }
        },
        isDevelopment: function () {
            let result = -1;
            let $elm = $("#lc-common-is-development");

            if (!C.State.isEmpty($elm)) {
                if ($.isNumeric($elm.val())) {
                    result = parseInt($elm.val());
                }
            }

            return C.Cast.toBool(result);
        },
        creds: function (callback) {
            let url = "/SECURE/creds.json";

            if (C.Utility.fileExists(url)) {
                C.API.Ajax.GetJSON(url, success);
            }
            else {
                C.Console.error("creds.json doesn't exists. \"" + url + "\"");
            }

            function success(data) {
                callback(data);
            }
        }
    },
    Utility: {
        randomInterval: function (min, max) {
            return Math.floor(Math.random() * (max - min + 1) + min);
        },
        fileExists: function (url) {
            let result = false;
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
        basic: function (content) {
            C.Console.invoke(C.Const.ConsoleTypes.BASIC, content);
        },
        debug: function (content) {
            C.Console.invoke(C.Const.ConsoleTypes.DEBUG, content);
        },
        error: function (content) {
            C.Console.invoke(C.Const.ConsoleTypes.ERROR, content);
        },
        invoke: function (type, content) {
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
        info: function (content) {
            C.Log.invoke(C.Const.LogTypes.INFO, content);
        },
        warning: function (content) {
            C.Log.invoke(C.Const.LogTypes.WARNING, content);
        },
        exception: function (content, params = {}) {
            C.Log.invoke(C.Const.LogTypes.EXCEPTION, content, params);
        },
        invoke: function (type, content, params = {}) {

        }
    },
    State: {
        isEmpty: function (val) {
            let result = true;

            if (typeof val !== "undefined" && val !== null) {
                result = val.length === 0;
            }

            return result;
        },
        isEqual: function (val1, val2) {
            return val1 === val2;
        },
        isEmail: function (email) {
            let result = false;
            let reg = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;

            if (reg.test(email)) {
                result = true;
            }

            return result;
        },
        isURL: function (url) {
            let result = false;
            let reg = /^(?:http(s)?:\/\/)?[\w.-]+(?:\.[\w\.-]+)+[\w\-\._~:/?#[\]@!\$&'\(\)\*\+,;=.]+$/;

            if (reg.test(url)) {
                result = true;
            }

            return result;
        },
        isBool: function (val) {
            let result = false;

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
        roundUp: function (val) {
            let result = this;
            let separator;

            if (!$.isNumeric(val)) {
                C.Console.error("RoundUp", "Supplied value \"" + val + "\" not numeric");
            }
            else {
                if (val.indexOf(".") > 0) {
                    separator = ".";
                }
                else if (val.indexOf(",") > 0) {
                    separator = ",";
                }

                if (!separator.IsNull()) {
                    let tmp1, tmp2;

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
        toBool: function (val) {
            let result = false;

            switch (String(val).toLowerCase()) {
                case "true":
                case "1":
                    result = true;
                    break;
            }

            return result;
        },
        toCapitalCase: function (val) {
            let result;

            if (!LC.PUB.State.isEmpty(val)) {
                result = val.substring(0, 1).toUpperCase() + val.substring(1);
            }

            return result;
        },
        toCamelCase: function (val) {
            let result;
            let vals = val.Split(" ");
            let i = 0;

            $.each(vals, function (val) {
                let word = C.State.toCapitalCase(val);

                result += i === 0 ? word : " " + word;

                i++;
            });

            return result;
        },
        toCamelCaseMerged: function (val) {
            return C.Cast.ToCamelCase(val).replace(" ", "");
        }
    },
    Cookie: {
        Get: function (name) {
            return Cookies.Get(name);
        },
        Set: function (name, value, expire = 180) {
            Cookies.Set(name, value, { expires: expire, path: "" });
        },
        Delete: function (name) {
            Cookies.Remove(name, { expires: -1, path: "" });
        },
        DeleteAll: function () {
            let cookies = [];

            let i = 1;

            $.each(Cookies.get(), function (cookie) {
                let name = cookie.name;

                C.Console.basic("Processing " + name);

                C.Cookie.Delete(name);
                result.push(name);

                i++;
            });

            C.Console.basic(i + " cookies deleted");
        }
    },
    Ajax: {
        Invoke: function (host = null, controller = null, action = null, query = {}, data = {}, type = "GET", callback = null) {
            let url = C.Ajax.GetUrl(host, controller, action, query);
            let timeStart = new Date();

            $.ajax({
                type: type.toUpperCase(),
                url: url,
                data: JSON.stringify(data),
                dataType: "JSON",
                contentType: "application/json",
                success: function (result) {
                    C.Console.basic({ duration: (new Date() - timeStart) / 1000, url: url, data: data });

                    if (!C.State.isEmpty(callback)) {
                        callback(result);
                    }
                },
                error: function (xhr, status, error) {
                    C.Console.error({ error: error });
                    C.Console.error({ status: status });
                    C.Console.error({ xhr: xhr });
                }
            });
        },
        GetJSON: function (host = null, controller = null, action = null, query = {}, callback) {
            $.getJSON(C.Ajax.GetUrl(host, controller, action, query), success);

            function success(data) {
                callback(data);
            }
        },
        GetUrl: function (host, controller, action, query = {}) {
            let result = "";

            if (!C.State.isEmpty(host)) {
                result = host;
            }

            if (!C.State.isEmpty(controller)) {
                result += "/" + controller;
            }

            if (!C.State.isEmpty(action)) {
                result += "/" + action;
            }

            let tmp;
            let i = 0;

            $.each(query, function (key, value) {
                if (i === 0) {
                    tmp = "?";
                }
                else {
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
        let result = C.Cast.toBool(this.val());

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
        let result = 0;

        if (this.IsEmail()) {
            result = 1;
        }

        return result;
    };

    $.fn.IsURL = function () {
        return C.State.isURL(this.val());
    };

    $.fn.IsURLAsInt = function () {
        let result = 0;

        if (this.IsURL()) {
            result = 1;
        }

        return result;
    };

    $.fn.IsEmpty = function () {
        let result = C.State.isEmpty(this.val());

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

    $.fn.SetCookie = function (name, expires = 180) {
        C.Cookie.Set(name, this.val(), expires);
    };
}(jQuery));