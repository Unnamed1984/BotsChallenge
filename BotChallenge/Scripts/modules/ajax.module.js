function sendGet(url) {
    var xhr = new XMLHttpRequest();
    xhr.open('GET', url, true);
    xhr.send();

    return xhr;
}


function sendPost(url, data) {
    var xhr = new XMLHttpRequest();
    xhr.open('POST', url, true);
    xhr.send(data);

    return xhr;
}