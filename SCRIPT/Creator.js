// log types
export const LogTypes = {
    Default: "DEFAULT",
    Info: "INFO",
    Warn: "WARN",
    Error: "ERROR"
};

// date types
export const DateTypes = {
    Current: "CURRENT",
    Log: "LOG",
    Short: "SHORT",
    Long: "LONG"
};

// supported types by the get function
export const getTypes = {
    Name: "NAME",
    Id: "ID",
    Tag: "TAG",
    Query: "QUERY",
    QueryAll: "QUERYALL"
};

// gets elements based on type and elm value
export const get = (type, elm) {
    let result = null;

    switch (upper(type) {
        case getTypes.Name:
            result = document.getElementsByName(elm);
            break;

        case getTypes.Id:
            result = document.getElementById(elm);
            break;

        case getTypes.Tag:
            result = document.getElementsByTagName(elm);
            break;

        case getTypes.Query:
            result = document.querySelector(elm).elements;
            break;

        case getTypes.QueryAll:
            result = document.querySelectorAll(elm);
            break;

        default:
            throw new Error(type + " not supported");
    }

    return result;
};

// makes an element visible
export const show(type, elm) {
    let result = get(type, elm);

    if (!Null(result)) {
        result.style.transition = "all 100ms";
        result.style.opacity = 1;
    }
};

// hides an element
export const hide(type, elm) {
    let result = get(type, elm);

    if (!Null(result)) {
        result.style.transition = "all 100ms";
        result.style.opacity = 0;
    }
};

// gets date in different formats
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

// appends the element to the found node based on type and typeStr
export const append(type, typeStr, element) {
    let result = get(type, typeStr);

    if (result) {
        result.appendChild(element);
    }

    return result;
};

// performs log to console
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

// trims both start and end
export const trim = (str) => str.trimStart().trimEnd();

// return true if obj is null or undefined
export const Null = (obj) => typeof obj === null || typeof obj === "undefined";

// returns true if value is json
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

// returns true if str is string
export const isString = (str) => typeof str === "string";

// returns true if str is a number
export const isNumeric = (str) => typeof str === "number";

// returns true if str is of type boolean
export const isBool = (str) => typeof str === "boolean";

// turns str to upper case
export const upper = (str) => str.toUpperCase();

// turns str to lower case
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

// converts the first letter to upper case
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

// returns an array based on the list
export const commaListToArray = (list, trimElement) => {
    var result = [];

    for (var str of list.split(",")) {
        const tmp = trimElement ? trim(str) : str;
        result.push(tmp);
    }

    return result;
};
