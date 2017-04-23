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
        var bots = controller.getBots();
        console.log('run run');

        var codeCollection = [];
        for (var i=0; i<bots.length; i++){
            codeCollection.push(bots[i].Code);
        }

        signalRGame.server.runGameLast({
            Code: codeCollection,
            BotsCount: codeCollection.length
        }, sessionStorage.getItem('botsLogin'));
    }

    signalRGame.client.startGameMovie = function (result) {
        console.log('startMovie');
        playMovie(result);
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