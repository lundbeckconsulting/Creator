$(function () {
  $("[class^='align-vertical-']").parent().css("position", "relative");
});
//# sourceMappingURL=Element.Layout.Other.js.map


//# sourceMappingURL=Element.Layout.Other.min.js.map

$(function () {
  $(".chapter").find(".more.invoke").click(function () {
    var $chapter = $(this).closest(".chapter");
    $(this).fadeOut("fast", function () {
      if (this.localName === "a") {
        $($chapter).find(".content.main").fadeIn("slow");
      } else {
        $($chapter).find(".more.wrap").fadeOut("fast", function () {
          $($chapter).find(".content.main").fadeIn("slow");
        });
      }
    });
  });
});
//# sourceMappingURL=TagHelper.Chapter.js.map


//# sourceMappingURL=TagHelper.Chapter.min.js.map

$(function () {
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
//# sourceMappingURL=TagHelper.Modal.js.map


//# sourceMappingURL=TagHelper.Modal.min.js.map

$(() => {
  const doLOG = entry => console.log(entry);

  const doWARN = entry => console.warning(entry);

  const doERROR = entry => console.error(entry);
});
//# sourceMappingURL=Utility.js.map


//# sourceMappingURL=Utility.min.js.map

$(() => {
  var RequestModes = {
    SameOrigin: "same-origin",
    NoCors: "no-cors",
    Cors: "cors"
  };
  var RequestMethods = {
    GET: 0,
    POST: 1,
    PUT: 2,
    DELETE: 3
  };
  var RequestCredentials = {
    Include: "include",
    Omit: "omit",
    SameOrigin: "same-origin"
  };

  const RequestResult = (rsp, dt) => {
    var response = rsp;
    var data = dt;
    var success = rsp.ok;
    var status = rsp.status;
    var statusText = rsp.statusText;
  };

  class WebRequest {
    static async Call(controller, action, query = {}, data = {}, method = RequestMethods.POST, mode = RequestModes.SameOrigin, credentials = RequestCredentials.SameOrigin) {
      var response = await fetch(WebRequest.GetUrl(controller, action, query), {
        method: method.toUpperCase(),
        headers: {
          "Content-Type": "application/json"
        },
        credentials: credentials,
        body: JSON.stringify(data),
        mode: mode
      });
      response.json().then(function (data) {
        return RequestResult(response, data);
      });
    }

    static GetUrl(controller, action, query = {}) {
      var result = "/" + controller + "/" + action;
      var i = 0;

      for (var q in query) {
        if (i === 0) {
          result += "?";
        } else {
          result += "&";
        }

        result += q[0] + "=" + q[1];
        i++;
      }

      return result;
    }

  }
});
//# sourceMappingURL=WebRequest.js.map


//# sourceMappingURL=WebRequest.min.js.map

//# sourceMappingURL=Creator.js.map
