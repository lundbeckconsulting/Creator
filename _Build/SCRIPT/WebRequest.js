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

//# sourceMappingURL=WebRequest.js.map
