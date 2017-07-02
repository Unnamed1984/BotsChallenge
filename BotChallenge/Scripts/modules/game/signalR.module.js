define(["modules/game/run.module", "signalRHubs"], function (RunModule) {
    return function (game, controller) {
        // Ссылка на автоматически-сгенерированный прокси хаба
        var signalRGame = $.connection.gameHub;

        // Функция, вызываемая при подключении нового пользователя
        signalRGame.client.onConnected = function (id, userName, allUsers) {
            console.log('connected');
            document.getElementById('user' + userName).innerText = "working";
        }

        signalRGame.client.logOut = function () {
            sessionStorage.clear();
            document.cookie = "";
        };

        signalRGame.client.run = function (login1) {
            var bots = controller.getBots();
            console.log('run run');

            var codeCollection = [];
            for (var i = 0; i < bots.length; i++) {
                codeCollection.push(bots[i].Code);
            }

            signalRGame.server.runGameLast({
                Code: codeCollection,
                BotsCount: codeCollection.length
            }, sessionStorage.getItem('botsLogin'));
        }

        signalRGame.client.startGameMovie = function (result) {
            console.log('startMovie');
            var runModule = new RunModule(game, controller);
            runModule.playMovie(result);
        }

        signalRGame.client.disconnect = function (login) {
            document.getElementById('user' + login).innerText = "offline";
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
    };
});