export function send(url, method, data = {}) {
    if (url == null)
        throw new Error("url is required");

    return $.ajax({
        url: url,
        method: method,
        data: data,
        async: false,
		error: function (jqXHR, exception) {
			console.log(jqXHR.status, exception);
		}
    });
}

export function post(url, data) {
    return send(url, "post", data);
}

export function get(url, data) {
    return send(url, "get", data);
}