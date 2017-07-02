define([], function () {

    class AjaxModule {

        sendGet(url) {
            var xhr = new XMLHttpRequest();
            xhr.open('GET', url, true);
            xhr.send();

            return xhr;
        }

        sendPost(url, data) {
            var xhr = new XMLHttpRequest();
            xhr.open('POST', url, true);
            xhr.setRequestHeader('Content-Type', 'application/json')
            xhr.send(JSON.stringify(data));

            return xhr;
        }
    }

    return AjaxModule;
});