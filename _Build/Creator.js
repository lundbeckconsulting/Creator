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


	

﻿/*
    @Author     Stein Lundbeck
    @Date       29.08.2020
 */

get("Id", "creatorBarsMenu").onclick((e) => {
    let bg = document.createElement("div");
    let closeIcon = document.createElement("i");
    closeIcon.id = "closeMenuIcon";
    bg.id = "barsMenuBackground";

    bg.appendChild(closeIcon);
    document.body.appendChild(bg);
    bg.classList.add("fade-in");
});

get("Id", "#burgerMenuBackground").onclick((e) => {
    closeMenu();
});

document.body.onclick((e) => {
    closeMenu();
});

function closeMenu() {
    let bg = get("Id", "burgerMenuBackground");
    bg.classList.add("fade-out");
    document.body.removeChild(bg);
}


	

﻿$(document).ready(() => {
    function CloseModalBackground() {
        $("#modalBackground").fadeOut("fast", function () {
            $("#modalBackground").remove();
        });
    }

    function TriggerCloseModal($modal) {
        $($modal).trigger("modal:hidden");
    }

    function DoCloseModal($modal) {
        $($modal).removeAttr("open");
        TriggerCloseModal($modal);
    }

    function CloseModal(e, hideBG = true) {
        var $modal = $(e.currentTarget).closest("[class^=\"modal-\"]");
        DoCloseModal($modal);

        if (hideBG) {
            CloseModalBackground();
        }
    }

    function CloseAllModals() {
        $("[class^=\"modal-\"][open=\"open\"]").each(function () {
            DoCloseModal(this);
        });

        CloseModalBackground();
    }

    $(".show-modal[data-modal]").click(function (e) {
        var modalId = $(this).data("modal");

        $("#" + modalId).trigger("modal:show");
    });

    $("[class^=\"modal-\"]").on("modal:show", function (e) {
        // eslint-disable-next-line consistent-this
        var $modal = this;
        var $bg = "<div id=\"modalBackground\"></div>";

        $("body").append($bg);

        $("#modalBackground").fadeIn("slow", function () {
            $($modal).attr("open", "open");
            $($modal).trigger("modal:visible");

            $("html, body").animate({
                scrollTop: 0
            }, 600);
        });
    });

    $(".hide-modal").click(function (e) {
        CloseModal(e);
    });

    $("[class^=\"modal-\"]").on("modal:hide", function (e) {
        CloseModal(e);
    });

    $(document).on("click", "#modalBackground", function () {
        CloseAllModals();
    });

    $(document).on("keydown", function (e) {
        if (e.keyCode === 27) {
            CloseAllModals();
        }
    });
});

	

	//# sourceMappingUrl=Creator.js.map
