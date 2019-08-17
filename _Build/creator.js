$("[class^='align-vertical-']").parent().css("position", "relative");
//# sourceMappingURL=Element.Layout.Other.js.map

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

function CloseModalBackground() {
  $("#modalBackground").fadeOut("fast", function() {
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
  $("[class^=\"modal-\"][open=\"open\"]").each(function() {
    DoCloseModal(this);
  });

  CloseModalBackground();
}

$(".show-modal[data-modal]").click(function(e) {
  var modalId = $(this).data("modal");

  $("#" + modalId).trigger("modal:show");
});

$("[class^=\"modal-\"]").on("modal:show", function(e) {
  var $modal = this;
  var $bg = "<div id=\"modalBackground\"></div>";

  $("body").append($bg);

  $("#modalBackground").fadeIn("slow", function() {
    $($modal).attr("open", "open");
    $($modal).trigger("modal:visible");

    $("html, body").animate({
      scrollTop: 0
    }, 600);
  });
});

$(".hide-modal").click(function(e) {
  CloseModal(e);
});

$("[class^=\"modal-\"]").on("modal:hide", function(e) {
  CloseModal(e);
});

$(document).on("click", "#modalBackground", function() {
  CloseAllModals();
});

$(document).on("keydown", function(e) {
  if (e.keyCode === 27) {
    CloseAllModals();
  }
});

//# sourceMappingURL=TagHelper.Modal.js.map

const doLOG = entry => console.log(entry);
const doINFO = entry => console.info(entry);
const doWARN = entry => console.warning(entry);
const doERROR = entry => console.error(entry);
const doTABLE = entry => console.table(entry);

const Trim = str => str.trimStart().trimEnd();

const IsNull = obj => obj === undefined || obj === null;

const IsJSON = str => {
  var result = true;

  try {
    JSON.parse(str);
  } catch (e) {
    result = false;
  }

  return result;
}

const equal = (val, compare) => val.toUpperCase() === compare.toUpperCase();

const equalList = (val, ...compares) => {
  var result = false;

  compares.map((compare) => {
    if (equal(val, compare)) {
      result = true;
    }
  });

  return result;
};

const Capitalize = str => str.charAt(0).toUpperCase() + string.slice(1);

//# sourceMappingURL=Utility.js.map

const RequestModes = {
  SameOrigin: "same-origin",
  NoCors: "no-cors",
  Cors: "cors"
};

const RequestMethods = {
  GET: "GET",
  POST: "POST",
  PUT: "PUT",
  DELETE: "DELETE"
};

const RequestCredentials = {
  Include: "include",
  Omit: "omit",
  SameOrigin: "same-origin"
};

export const Enums = () => {
  get: () => RequestMethods;
  get: () => RequestModes;
  get: () => RequestCredentials;
}

export const RequestResult = rsp => {
  var response = null;

  get: Response = () => response;
  get: Success = () => this.Response.ok;
  get: Status = () => this.Response.status;
  get: StatusText = () => this.Response.statusText;

  set: Response = rsp => response = rsp;
};

export const WebRequest = async (controller = "API", action, query = {}, data = {}, method = RequestMethods.POST, mode = RequestModes.SameOrigin, creds = RequestCredentials.SameOrigin, headers = {
    "Content-Type": "application/json",
    "Accept": "application/json"
  }) => {
  await fetch(GetUrl(), {
    method: method,
    headers: headers,
    credentials: creds,
    body: data,
    mode: mode
  })
    .then(response => {
      if (response.ok) {
        return RequestResult(response);
      } else {
        throw new Error("Request failed: " + response.status + "; " + response.statusText);
      }
    })
    .catch(error => {
      throw new Error("Error: " + error.statusText, error)
    });

  function GetUrl() {
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

//# sourceMappingURL=WebRequest.js.map

//# sourceMappingURL=Creator.js.map

//# sourceMappingURL=Creator.js.map
