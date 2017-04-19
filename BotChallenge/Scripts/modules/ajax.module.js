function sendGet(url) {
    var xhr = new XMLHttpRequest();
    xhr.open('GET', url, true);
    xhr.send();

    return xhr;
}


function sendPost(url, data) {
    var body = "code=" + data;
    var xhr = new XMLHttpRequest();
    xhr.open('POST', url, true);
    xhr.setRequestHeader('Content-Type', 'application/x-www-form-urlencoded')
    xhr.send(body);

    return xhr;
}