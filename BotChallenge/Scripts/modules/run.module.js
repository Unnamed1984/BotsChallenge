function hideUI() {
    console.log('hide');
    document.getElementsByTagName('footer')[0].classList.add('display-none');

    document.getElementById('canva').classList.add('big-canva');
    
    var bots = controller.getBots();
    for (var i = 0; i < bots.length; i++) {
        bots[i].sprite.input.useHandCursor = false;
    }

    var style = { font: 'bold 60pt Arial', fill: '#c9eaf2', align: 'center', wordWrap: true, wordWrapWidth: 600 };
    var text = game.add.text(game.world.centerX, game.world.centerY, "Waiting for another player...", style);
    text.padding.set(10, 16);
    text.anchor.setTo(0.5, 0.5);

    focusCameraOnSprite(text);
}

function setReady() {
    controller.setGameState("ready");

    var login = sessionStorage.getItem('botsLogin');

    var signalRGame = $.connection.gameHub;
    signalRGame.server.setReadyForGame(login);
}