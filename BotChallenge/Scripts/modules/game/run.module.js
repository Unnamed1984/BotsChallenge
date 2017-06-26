define(["modules/game/bots.module", "modules/game/camera.module"], function (BotsModule, CameraModule) {
    "use strict"

    var botsModule = new BotsModule();
    var cameraModule = new CameraModule();

    class RunModule {

        constructor(game, controller) {
            this.game = game;
            this.controller = controller;
        }

        function hideUI() {
            console.log('hide');
            document.getElementsByTagName('footer')[0].classList.add('display-none');

            document.getElementById('canva').classList.add('big-canva');

            var bots = this.controller.getBots();
            for (var i = 0; i < bots.length; i++) {
                bots[i].sprite.input.useHandCursor = false;
            }
        }

        function setReady() {
            this.controller.setGameState("ready");

            var login = sessionStorage.getItem('botsLogin');

            document.getElementById('user_' + login).innerText = "ready";

            var signalRGame = $.connection.gameHub;
            signalRGame.server.setReadyForGame(login);

            addText('Waiting for another player...', '#c9eaf2');
            this.controller.setGameState('gameIsGoing');
        }

        function playMovie(movieParams) {
            console.log(movieParams);

            window.counter = 0;

            deleteText();
            window.intervalId = setInterval(function () {
                if (+window.counter == movieParams.Commands.length - 1) {
                    clearInterval(+window.intervalId);
                    if (movieParams.WinnerName == sessionStorage.getItem('botsLogin')) {
                        addText('VICTORY', 'green');
                    }
                    else {
                        addText('DEFEAT, red');
                    }
                }

                var bots = this.controller.getBots();
                var enemyBots = this.controller.getEnemyBots();

                var login = sessionStorage.getItem('botsLogin');
                var command = movieParams.Commands[+window.counter];
                window.counter++;
                if (command.ActionType == "Move") {
                    var botId = command.BotId;
                    var playerName = command.PlayerName;
                    console.log(command);
                    var stepParams = command.StepParams;

                    if (login == playerName) {
                        console.log('player found');
                        for (var i = 0; i < bots.length; i++) {
                            console.log('move bot');
                            console.log(bots[i]);
                            console.log(botId);
                            if (bots[i].Name == botId) {
                                console.log('bot was found');
                                botsModule.onBotDown(bots[i].sprite);
                                cameraModule.focusCameraOnTile(bots[i].X, bots[i].Y);
                                setTimeout(function () {
                                    bots[i].move(+stepParams[2], +stepParams[3]);
                                }, 2000);
                                break;
                            }
                        }
                    }
                    else {
                        for (var i = 0; i < enemyBots.length; i++) {
                            console.log("enemyBots");
                            console.log(enemyBots[i].Name);
                            if (enemyBots[i].Name == botId) {
                                console.log("enemyBot was found");
                                cameraModule.focusCameraOnTile(enemyBots[i].X, enemyBots[i].Y);
                                setTimeout(function () {
                                    enemyBots[i].move(+stepParams[2], +stepParams[3]);
                                }, 2000);
                                break;
                            }
                        }
                    }

                }
                else {
                    // if shoot...
                }
            }, 5000);
        }

        function addText(text, color) {
            var style = { font: 'bold 60pt Arial', fill: color, align: 'center', wordWrap: true, wordWrapWidth: 600 };
            var text = game.add.text(game.world.centerX, game.world.centerY, text, style);
            text.padding.set(10, 16);
            text.anchor.setTo(0.5, 0.5);

            if (this.controller.label != '') {
                this.controller.label.destroy();
            }

            this.controller.label = text;
            cameraModule.focusCameraOnSprite(text);
        }

        function deleteText() {
            if (this.controller.label != '') {
                this.controller.label.destroy();
            }
        }
    }

    return RunModule;
});