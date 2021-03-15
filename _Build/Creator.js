const closeDialogs = () => {
    document.querySelectorAll("dialog").forEach(elm => {
        elm.removeAttribute("open");

        if (document.getElementById("dialogBackground") !== null) {
            document.getElementById("dialogBackground").remove();
        }
    });
};

const openDialog = (id) => {
    let bg = document.createElement("div");
    bg.id = "dialogBackground";
    bg.classList.add("show-force");
    bg.addEventListener("click", event => {
        closeDialogs();
    });
    document.body.appendChild(bg);

    document.getElementById(id).setAttribute("open", "open");
};

document.querySelectorAll(".open-dialog[data-dialog]").forEach(elm => {
    elm.addEventListener("click", event => {
        let dialogId = elm.getAttribute("data-dialog");

        if (dialogId !== null) {
            openDialog(dialogId);
        }
    });
});

window.addEventListener("keyup", event => {
    if (event.key === "Escape") {
        closeDialogs();
    }
});

document.querySelectorAll("dialog").forEach(elm => {
    elm.querySelector("header .close-command").addEventListener("click", event => {
        closeDialogs();
    });
});

document.querySelectorAll("dialog").forEach(elm => {
    elm.addEventListener("cmd:close", event => {
        closeDialogs();
    });

    elm.addEventListener("cmd:show", event => {
        openDialog(elm.id);
    });
});

document.querySelectorAll("dialog").forEach(elm => {
    elm.querySelector("footer .ok-command").addEventListener("click", event => {
        closeDialogs();
    });
});


	

﻿const _notifyDurations = { "three": 3, "five": 5, "eight": 8, "fourteen": 14, "twenty": 20, "permanent": -99 };

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


	

	//# sourceMappingUrl=Creator.js.map
