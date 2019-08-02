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

            response.json()
                .then(function (data) {
                    return RequestResult(response, data);
                });
        }

        static GetUrl(controller, action, query = {}) {
            var result = "/" + controller + "/" + action;

            var i = 0;
            for (var q in query) {
                if (i === 0) {
                    result += "?";
                }
                else {
                    result += "&";
                }

                result += q[0] + "=" + q[1];

                i++;
            }

            return result;
        }
    }
});