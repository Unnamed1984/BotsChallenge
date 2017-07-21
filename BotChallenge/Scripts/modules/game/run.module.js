define(["modules/game/camera.module"], function (CameraModule) {
    "use strict"

    class RunModule {

        constructor(game, controller) {
            this.game = game;
            this.controller = controller;
        }

        get cameraModule() {
            if (!this.__cameraModule) {
                this.__cameraModule = new CameraModule(this.game, this.controller);
            }
            return this.__cameraModule;
        }

        get botsModule() {
            if (!this.__botsModule) {
                // due to issue with circular dependencies and its support by require.
                var BotsModule = require("modules/game/bots.module");
                this.__botsModule = new BotsModule(this.game, this.controller);
            }
            return this.__botsModule;
        }

        hideUI() {
            console.log('hide');
            document.getElementsByTagName('footer')[0].classList.add('display-none');

            document.getElementById('canva').classList.add('big-canva');

            var bots = this.controller.getBots();
            for (var i = 0; i < bots.length; i++) {
                bots[i].sprite.input.useHandCursor = false;
            }
        }

        setReady() {
            this.controller.setGameState("ready");

            var login = sessionStorage.getItem('botsLogin');

            document.getElementById('user_' + login).innerText = "ready";

            var signalRGame = $.connection.gameHub;
            signalRGame.server.setReadyForGame(login);

            this.addText('Waiting for another player...', '#c9eaf2');
            this.controller.setGameState('gameIsGoing');
        }

        playMovie(movieParams) {
            console.log(movieParams);

            window.counter = 0;

            this.deleteText();
            var self = this;
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

                var bots = self.controller.getBots();
                var enemyBots = self.controller.getEnemyBots();

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
                                self.botsModule.onBotDown(bots[i].sprite);
                                self.cameraModule.focusCameraOnTile(bots[i].X, bots[i].Y);
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
                                self.cameraModule.focusCameraOnTile(enemyBots[i].X, enemyBots[i].Y);
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

        addText(text, color) {
            var style = { font: 'bold 60pt Arial', fill: color, align: 'center', wordWrap: true, wordWrapWidth: 600 };
            var text = this.game.add.text(this.game.world.centerX, this.game.world.centerY, text, style);
            text.padding.set(10, 16);
            text.anchor.setTo(0.5, 0.5);

            if (this.controller.label != '') {
                this.controller.label.destroy();
            }

            this.controller.label = text;
            this.cameraModule.focusCameraOnSprite(text);
        }

        deleteText() {
            if (this.controller.label != '') {
                this.controller.label.destroy();
            }
        }
    }

    return RunModule;
});