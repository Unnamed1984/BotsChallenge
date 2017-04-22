"use strict"

document.getElementById('compilationBtn').onclick = function () {
    var code = document.getElementById('code').value;
    
    console.log(code);
    var request = sendPost("/Game/CompileBot", code);
    request.onreadystatechange = function () { // (3)
        if (request.readyState != 4) return;

        if (request.status != 200) {
            alert(request.status + ': ' + request.statusText);
        } else {
            var result = JSON.parse(request.responseText);
            if (result.IsCodeCorrect) {
                setCodeAsCorrect();
                highLightPanelAsCorrect();
            } else {
                setCodeAsIncorrect(result.Errors);
                highLightPanelAsIncorrect(result.Errors);
            }

            saveErrorsState(controller.getSelectedBot());
        }

    }
}

document.getElementById('runBtn').onclick = function () {
    hideUI();
    //var selected = controller.getSelectedBot();
    //saveCode(selected);

    //var code = [];
   
    //var bots = controller.getBots();
    //for (var i = 0; i < bots.length; i++) {
    //    code.push(bots[i].Code);
    //}

    //console.log(code);
    //console.log(bots.length);
    //var request = sendPost("/Game/CompileBots", { Code: code, BotsCount: bots.length });
    //request.onreadystatechange = function () { // (3)
    //    if (request.readyState != 4) return;

    //    if (request.status != 200) {
    //        alert(request.status + ': ' + request.statusText);
    //    } else {
    //        var result = JSON.parse(request.responseText);
    //        if (result.IsCodeCorrect) {
    //            setCodeAsCorrect();
    //            highLightPanelAsCorrect();
    setReady();
    //        } else {
    //            setCodeAsIncorrect(result.Errors);
    //            highLightPanelAsIncorrect(result.Errors);
    //        }

    //        saveErrorsState(controller.getSelectedBot());
    //    }

    //}
}

function clearErrors() {
    var container = document.getElementById('errors');

    while (container.firstChild) {
        container.removeChild(container.firstChild);
    }
}

function setCodeAsIncorrect(errors) {
    var bot = controller.getSelectedBot();
    bot.content.IsCodeCorrect = false;
    for (var i = 0; i < errors.length; i++) {
        bot.content.Errors.push(errors[i]);
    }
}

function highLightPanelAsIncorrect(errors) {
    document.getElementById('statePanel').classList.remove('panel-default');
    document.getElementById('statePanel').classList.remove('panel-success');
    document.getElementById('statePanel').classList.add('panel-danger');

    clearErrors();
    
    fillErrorsSection(errors);
}

function setCodeAsCorrect() {
    var bot = controller.getSelectedBot();
    bot.content.IsCodeCorrect = true;
    bot.content.Errors = [];
}

function highLightPanelAsCorrect() {
    document.getElementById('statePanel').classList.remove('panel-default');
    document.getElementById('statePanel').classList.remove('panel-danger');
    document.getElementById('statePanel').classList.add('panel-success');

    clearErrors();

    fillErrorsSection([controller.getSelectedBot().content.Name  +'\'s code is correct!']);
}

function setCodeAsDefault() {
    var bot = controller.getSelectedBot();
    console.log(bot);
    bot.content.IsCodeCorrect = null;
    bot.content.Errors = [];
}

function highLightPanelAsDefault() {
    document.getElementById('statePanel').classList.remove('panel-success');
    document.getElementById('statePanel').classList.remove('panel-danger');
    document.getElementById('statePanel').classList.add('panel-default');

    clearErrors();

    fillErrorsSection([controller.getSelectedBot().content.Name + '\'s code has not been compiled yet!']);
}

function fillErrorsSection(errors){
    var errorsNode = document.getElementById('errors');

    for (var i = 0; i < errors.length; i++) {
        var p = document.createElement('p');
        p.innerHTML = errors[i];
        errorsNode.appendChild(p)
    }
}