export const LogTypes = {
    Default: "DEFAULT",
    Info: "INFO",
    Warn: "WARN",
    Error: "ERROR"
};

export const DateTypes = {
    Current: "CURRENT",
    Log: "LOG",
    Short: "SHORT",
    Long: "LONG"
};

export const getTypes = {
    Name: "NAME",
    Id: "ID",
    Tag: "TAG",
    Query: "QUERY",
    QueryAll: "QUERYALL"
};

export const get = (type, data) {
    let result = null;

    switch (upper(type) {
        case getTypes.Name:
            result = document.getElementsByName(data);
            break;

        case getTypes.Id:
            result = document.getElementById(data);
            break;

        case getTypes.Tag:
            result = document.getElementsByTagName(data);
            break;

        case getTypes.Query:
            result = document.querySelector(data).elements;
            break;

        case getTypes.QueryAll:
            result = document.querySelectorAll(data);
            break;

        default:
            throw new Error(type + " not supported");
    }

    return result;
};

export const show(type, data) {
    let result = get(type, data);

    if (!Null(result)) {
        result.style.transition = "all 100ms";
        result.style.opacity = 1;
    }
};

export const hide(type, data) {
    let result = get(type, data);

    if (!Null(result)) {
        result.style.transition = "all 100ms";
        result.style.opacity = 0;
    }
};

export const getDate = (type = DateTypes.Short) => {
    var result = null;
    const current = new Date();

    if (equal(type, DateTypes.Current)) {
        result = current;
    } else if (equal(type, DateTypes.Log)) {
        result = "[" + current.getUTCHours() + ":" + current.getUTCMinutes() + "." + current.getUTCSeconds() + "." + current.getUTCMilliseconds() + " - " + current.getUTCDay() + "." + current.getUTCMonth() + "." + current.getUTCFullYear() + "]";
    } else if (equal(type, DateTypes.Short)) {
        result = current.getUTCDay() + "." + current.getUTCMonth() + "." + current.getUTCFullYear();
    } else if (equal(type, DateTypes.Long)) {
        result = getDate(DateTypes.Short) + " " + current.getUTCHours() + ":" + current.getUTCMinutes() + "." + current.getUTCSeconds();
    }

    return result;
};

export const log = (type = LogTypes.Default, entry = EmptyString) => {
    if (equal(type, LogTypes.Default)) {
        console.log(getDate(DateTypes.Log), entry);
    } else if (equal(type, LogTypes.Info)) {
        console.info(getDate(DateTypes.Log), entry);
    } else if (equal(type, LogTypes.Warn)) {
        console.warn(getDate(DateTypes.Log), entry);
    } else if (equal(type, LogTypes.Error)) {
        console.error(getDate(DateTypes.Log), entry);
    }
    else {
        throw new Error(type + " not supported");
    }
};

export const trim = (str) => str.trimStart().trimEnd();
export const Null = (obj) => typeof obj === null || typeof obj === "undefined";
export const isJSON = (json) => {
    var result = false;

    try {
        JSON.parse(json);
        result = true;
    } catch (e) {
        result = false;
    }

    return result;
};
export const isString = (str) => typeof str === "string";
export const isNumeric = (str) => typeof str === "number";
export const isBool = (str) => typeof str === "boolean";
export const upper = (str) => str.toUpperCase();
export const lower = (str) => str.toLowerCase();
export const equal = (val, compare) => equals(val, compare);
export const equals = (val, ...compares) => {
    var result = false;

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
export const capitalize = (str) => str.charAt(0).toUpperCase() + str.slice(1);
export const camelCase = (str) => {
    var result = "";
    var count = -1;

    for (var s of str.split(" ")) {
        if (count > 0) {
            result += " ";
        }

        result += capitalize(s);

        count++;
    }

    return result;
};
export const commaListToArray = (list, trimElement) => {
    var result = [];

    for (var str of list.split(",")) {
        const tmp = trimElement ? trim(str) : str;
        result.push(tmp);
    }

    return result;
};
