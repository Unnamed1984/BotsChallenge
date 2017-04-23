function hideUI() {
    console.log('hide');
    document.getElementsByTagName('footer')[0].classList.add('display-none');

    document.getElementById('canva').classList.add('big-canva');
    
    var bots = controller.getBots();
    for (var i = 0; i < bots.length; i++) {
        bots[i].sprite.input.useHandCursor = false;
    }
}

function setReady() {
    controller.setGameState("ready");

    var login = sessionStorage.getItem('botsLogin');

    var signalRGame = $.connection.gameHub;
    signalRGame.server.setReadyForGame(login);

    addText('Waiting for another player...');

    controller.setGameState('gameIsGoing');
}

function playMovie(movieParams) {
    console.log(movieParams);

    window.counter = 0;

    window.intervalId = setInterval(function () {
        deleteText();
        if (+window.counter == movieParams.Commands.length - 1) {
            clearInterval(+window.intervalId);
        }

        var bots = controller.getBots();
        var enemyBots = controller.getEnemyBots();

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
                        onBotDown(bots[i].sprite);
                        focusCameraOnTile(bots[i].X, bots[i].Y);
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
                        focusCameraOnTile(enemyBots[i].X, enemyBots[i].Y);
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

function addText(text) {
    var style = { font: 'bold 60pt Arial', fill: '#c9eaf2', align: 'center', wordWrap: true, wordWrapWidth: 600 };
    var text = game.add.text(game.world.centerX, game.world.centerY, text, style);
    text.padding.set(10, 16);
    text.anchor.setTo(0.5, 0.5);

    if (controller.label != '') {
        controller.label.destroy();
    }

    controller.label = text;
    focusCameraOnSprite(text);
}

function deleteText() {
    if (controller.label != '') {
        controller.label.destroy();
    }
}