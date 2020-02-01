$(document).ready(() => {
    const LogTypes = {
        Default: "DEFAULT",
        Info: "INFO",
        Warn: "WARN",
        Error: "ERROR",
        Table: "TABLE",
    };

    const DateTypes = {
        Current: "CURRENT",
        Log: "LOG",
        Short: "SHORT",
        Long: "LONG",
    };

    const dates = (type = DateTypes.Short) => {
        var result = Empty;
        const current = new Date();

        if (equal(type, DateTypes.Current)) {
            result = current;
        } else if (equal(type, DateTypes.Log)) {
            result = "[" + current.getUTCHours() + ":" + current.getUTCMinutes() + "." + current.getUTCSeconds() + "." + current.getUTCMilliseconds() + " - " + current.getUTCDay() + "." + current.getUTCMonth() + "." + current.getUTCFullYear() + "]";
        } else if (equal(type, DateTypes.Short)) {
            result = current.getUTCDay() + "." + current.getUTCMonth() + "." + current.getUTCFullYear();
        } else if (equal(type, DateTypes.Long)) {
            result = dates(DateTypes.Short) + " " + current.getUTCHours() + ":" + current.getUTCMinutes() + "." + current.getUTCSeconds();
        }

        return result;
    };
    const doLog = (type = LogTypes.Default, entry = EmptyString) => {
        if (equal(type, LogTypes.Default)) {
            console.log(dates(DateTypes.Log), entry);
        } else if (equal(type, LogTypes.Info)) {
            console.info(dates(DateTypes.Log), entry);
        } else if (equal(type, LogTypes.Warn)) {
            console.warn(dates(DateTypes.Log), entry);
        } else if (equal(type, LogTypes.Error)) {
            console.error(dates(DateTypes.Log), entry);
        } else if (equal(type, LogTypes.Table)) {
            console.table(dates(DateTypes.Log), entry);
        }
    };
    const LOG = (entry = EmptyString) => doLog(LogTypes.Default, entry);
    const INFO = (entry = EmptyString) => doLog(LogTypes.Info, entry);
    const WARN = (entry = EmptyString) => doLog(LogTypes.Warn, entry);
    const ERROR = (entry = EmptyString) => doLog(LogTypes.Error, entry);
    const TABLE = (entry = EmptyString) => doLog(LogTypes.Table, entry);
    const trim = (str = EmptyString) => str.trimStart().trimEnd();
    const isNull = (obj = Empty) => typeof obj === "object" || typeof obj === "undefined";
    const isJSON = (obj = Empty) => {
        var result = EmptyBoolTrue;

        try {
            JSON.parse(obj);
        } catch (e) {
            result = false;
        }

        return result;
    };
    const isString = (val = Empty) => typeof val === "string";
    const isNumeric = (val = Empty) => typeof val === "number";
    const isNegative = (sum = EmptyNumber) => isNumeric(sum) && sum < 0;
    const isPositive = (sum = EmptyNumber) => isNumeric(sum) && !isNegative(sum);
    const isChar = (val = Empty) => isString(val) && val.length === 1;
    const isBool = (val = EmptyBool) => typeof val === "boolean";
    const isFunction = (val = Empty) => typeof val === "function";
    const isObject = (val = Empty) => typeof val === "object";
    const upper = (str = EmptyString) => str.toUpperCase();
    const lower = (str = EmptyString) => str.toLowerCase();
    const upperLocale = (str = EmptyString) => str.toLocaleUpperCase();
    const lowerLocale = (str = EmptyString) => str.toLocaleLowerCase();
    const equals = (val = Empty, ...compares) => {
        var result = EmptyBool;

        compares.map(compare => {
            if (isString(val)) {
                if (upper(val) === upper(compare)) {
                    result = true;
                }
            } else {
                if (val === compare) {
                    result = true;
                }
            }
        });

        return result;
    };
    const equal = (val = Empty, compare) => equals(val, compare);
    const capitalize = (str = EmptyString) => str.charAt(0).toUpperCase() + str.slice(1);
    const camelCase = (str = EmptyString) => {
        var result = EmptyString;
        var count = EmptyCount;

        for (var s of str.split(" ")) {
            if (count > 0) {
                result += " ";
            }

            result += capitalize(s);

            count++;
        }

        return result;
    };
    const camelCaseCombined = (str = EmptyString) => camelCase(str).replace(" ", "");
    const commaListToArray = (list = EmptyString, trimElement = EmptyBoolTrue) => {
        var result = EmptyStringArray;

        for (var str of list.split(",")) {
            const tmp = trimElement ? trim(str) : str;
            result.push(tmp);
        }

        return result;
    };
    const arrayToCommaList = (arr = EmptyStringArray) => {
        var result = EmptyString;
        var count = EmptyCount;

        for (var o of arr) {
            if (count > 0) {
                result += ", ";
            }

            result += o.toString();

            count++;
        }

        return result;
    };
    const Empty = null;
    const EmptyUndefined = undefined;
    const EmptyString = "";
    const EmptyNumber = -1;
    const EmptyArray = [];
    const EmptyStringArray = [EmptyString];
    const EmptyNumberArray = [EmptyNumber];
    const EmptyJSON = {};
    const EmptyJSONArray = [{}];
    const EmptyBool = false;
    const EmptyBoolTrue = !EmptyBool;
    const EmptyCount = 0;
});