define(["signalRHubs"], function () {
    // Ссылка на автоматически-сгенерированный прокси хаба
    var game = $.connection.registrationHub;

    // Функция, вызываемая при подключении нового пользователя
    game.client.onConnected = function (id, userName, allUsers) {
    }

    window.onload = function () {
        document.getElementById('progress').className += " progress-bar-wide";
    };

    game.client.goForWaiting = function () {
        document.getElementById('regForm').remove();
        document.getElementById('waitingForm').classList.remove('display-none');
    };

    game.client.goForGame = function (login) {
        var element = document.getElementById('progress');
        element.className += " progress-bar-success";
        element.classList.remove("active");
        element.classList.remove("progress-bar-striped");
        element.textContent = "Ready";

        document.getElementById('cancel-btn').className += " display-none";

        document.cookie = "Login=" + login;

        setTimeout(function () {
            location.href = "/Game/Game";
        }, 2000);
    };

    game.client.logOut = function () {
        sessionStorage.clear();
        document.cookie = "";
    };

    var login = sessionStorage.getItem('botsLogin');

    if (login) {
        $.connection.hub.start().done(function () {
            game.server.connect(login);
            document.getElementById('regForm').remove();
            document.getElementById('waitingForm').classList.remove('display-none');
        });
    }

    function cancelHandler() {
        game.server.cancelWaiting(sessionStorage.getItem('botsLogin'));
    };

    function submitHandler(e) {
        e.preventDefault();

        $.connection.hub.start().done(function () {
            var name = document.getElementById('login').value;

            sessionStorage.setItem('botsLogin', name);
            game.server.connect(name);
        });
    }

    return {
        cancelHandler: cancelHandler,
        submitHandler: submitHandler
    };

});