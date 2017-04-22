$(function () {
    // Ссылка на автоматически-сгенерированный прокси хаба
    var signalRGame = $.connection.gameHub;

    // Функция, вызываемая при подключении нового пользователя
    signalRGame.client.onConnected = function (id, userName, allUsers) {
        console.log('connected');
    }

    signalRGame.client.logOut = function () {
        sessionStorage.clear();
        document.cookie = "";
    };

    signalRGame.client.run = function (login1) {
        console.log(login1);
    }

    signalRGame.client.disconnect = function () {
        console.log('disconnected');
    }

    signalRGame.client.displayUnauthorizedMessage = function () {
        console.log('Unathorized');
    }

    var login = sessionStorage.getItem('botsLogin');

    if (login) {
        $.connection.hub.start().done(function () {
            signalRGame.server.connect(login);
        });
    }
});