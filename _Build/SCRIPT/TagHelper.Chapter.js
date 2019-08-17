$(".chapter").find(".more.invoke").click(function() {
  var $chapter = $(this).closest(".chapter");

  $(this).fadeOut("fast", function() {
    if (this.localName === "a") {
      $($chapter).find(".content.main").fadeIn("slow");
    } else {
      $($chapter).find(".more.wrap").fadeOut("fast", function() {
        $($chapter).find(".content.main").fadeIn("slow");
      });
    }
  });
});

//# sourceMappingURL=TagHelper.Chapter.js.map

//# sourceMappingURL=TagHelper.Chapter.js.map
