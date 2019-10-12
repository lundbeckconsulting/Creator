const RequestModes = {
  SameOrigin: "SAME-ORIGIN",
  NoCors: "NO-CORS",
  Cors: "CORS",
};

const RequestMethods = {
  GET: "GET",
  POST: "POST",
  PUT: "PUT",
  DELETE: "DELETE",
};

const RequestCredentials = {
  Include: "INCLUDE",
  Omit: "OMIT",
  SameOrigin: "SAME-ORIGIN",
};

class RequestResult {
  constructor(response) {
    this.resp = response;
  }

  response() {
    return this.resp;
  }

  success() {
    return this.resp.ok;
  }

  status() {
    return this.resp.status;
  }

  statusText() {
    return this.resp.statusText;
  }
}

const WebRequest = async (controller = "API",
  action, query = {}, data = {}, method = RequestMethods.POST, mode = RequestModes.SameOrigin, creds = RequestCredentials.SameOrigin, headers = {
    "Content-Type": "application/json",
    "Accept": "application/json"
  }) => {
  const GetUrl = () => {
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

  await fetch(GetUrl(), {
    method: method,
    headers: headers,
    // @ts-ignore
    credentials: creds.toLowerCase(),
    // @ts-ignore
    body: data,
    // @ts-ignore
    mode: mode.toLowerCase(),
  })
    .then(response => {
      if (response.ok) {
        return new RequestResult(response);
      } else {
        throw new Error("Request failed: " + response.status + "; " + response.statusText);
      }
    })
    .catch(error => {
      throw new Error("Error: " + error.statusText);
    });
};






























//# sourceMappingURL=WebRequest.js.map
