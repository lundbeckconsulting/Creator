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


	//# sourceMappingUrl=Element.Dialog.js.map
