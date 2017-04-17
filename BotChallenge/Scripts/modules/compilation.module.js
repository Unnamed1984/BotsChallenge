document.getElementById('compilationBtn').onclick = function () {
    var code = document.getElementById('code').value;
    
    var request = sendPost("/Game/CompileBot", code);
    request.onreadystatechange = function () { // (3)
        if (request.readyState != 4) return;

        if (request.status != 200) {
            alert(request.status + ': ' + request.statusText);
        } else {
            var result = JSON.parse(request.responseText);
            console.log(result);
            if (result.IsCodeCorrect) {
                console.log('Correct');
                document.getElementById('statePanel').classList.remove('panel-default');
                document.getElementById('statePanel').classList.add('panel-success');
            } else {
                console.log('Incorrect');
                document.getElementById('statePanel').classList.remove('panel-default');
                document.getElementById('statePanel').classList.add('panel-danger');

                var errorsNode = document.getElementById('errors');
                clearErrors();

                console.log(result.Errors);
                for (var i = 0; i < result.Errors.length; i++) {
                    var p = document.createElement('p');
                    p.innerHTML = result.Errors[i];
                    errorsNode.appendChild(p)
                }
            }
        }

    }
}

function clearErrors() {
    var container = document.getElementById('errors');

    while (container.firstChild) {
        container.removeChild(container.firstChild);
    }
}