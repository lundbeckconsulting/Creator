/*
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


	//# sourceMappingUrl=TagHelper.BarsMenu.js.map
