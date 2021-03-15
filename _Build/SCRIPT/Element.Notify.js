const _notifyDurations = { "three": 3, "five": 5, "eight": 8, "fourteen": 14, "twenty": 20, "permanent": -99 };

const parseElement = (elm) => {
    let tmp = elm.getAttribute("class");
    let val;
    let result = {};

    for (let str of tmp.split(" ")) {
        if (str.startsWith("notify-")) {
            val = str;
        }
    };

    if (val.length > 0) {
        let vals = val.split("-");

        if (vals.length === 4) {
            result = {
                "type": vals[1],
                "position": vals[2],
                "duration": vals[3],
                "element": elm
            };
        };
    };

    return result;
};

const showElement = (elm) => {
    let notify = parseElement(elm);

    setTimeout(() => {
        notify.element.classList.add("show");

        if (notify.duration !== "permanent") {
            setTimeout(() => {
                notify.element.classList.remove("show");
            }, _notifyDurations[notify.duration] * 1000);
        }
    }, 800);
};

document.querySelectorAll("[class^=notify-]").forEach((elm) => {
    elm.addEventListener("cmd:open", (e) => {
        showElement(elm);
    });

    elm.dispatchEvent(new Event("cmd:open"));
});


	//# sourceMappingUrl=Element.Notify.js.map
