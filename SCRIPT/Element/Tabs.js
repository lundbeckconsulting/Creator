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
