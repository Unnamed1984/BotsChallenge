/**
 * Created by Paul on 14.04.2017.
 */

function createLayers(map){
    // creating layers
    var ground = map.createLayer('Ground');
    //ground.resizeWorld();
    var obstacles = map.createLayer('Obstacles');
    var decorations = map.createLayer('Decorations');
    var collectorItems = map.createLayer('CollectorsItems');
}

function createMap(game){
    // tilemap using
    var map = game.add.tilemap('tilemap');
    map.addTilesetImage('BotTestTileset','tilesetimage', 64, 64);

    return map;
}

function initializeBots(map){
    var bots = controller.getBots();

    // ahchor depends on camera
    for (var i=0; i<bots.length; i++){
        var sprite = bots[i].sprite = game.add.sprite(bots[i].X*tile_size, bots[i].Y*tile_size, 'bot');
        sprite.animations.add('selected', [1, 2, 3, 4], 4, true);
        sprite.inputEnabled = true;
        sprite.input.useHandCursor = true;
        sprite.events.onInputDown.add(onBotDown);
        initializeCode(bots[i]);

        // Click on list item
        document.getElementById('botList' + i).onclick = function (e){
            // should change it when here is server side
            var id = e.target.id[e.target.id.length-1];
            var bots = controller.getBots();

            for (var i=0; i<bots.length; i++){
                if (bots[i].Id == id){
                    onBotDown(bots[i].sprite, null);
                }
            }
        };
    }

    game.camera.focusOn(bots[0].sprite);
    bots[0].sprite.animations.play('selected');
    controller.selectedBot.content = bots[0];
    displayCode(bots[0]);

    // Enemy bots
    var enemyBots = controller.getEnemyBots();

    for (var i=0; i<enemyBots.length; i++){
        var sprite = enemyBots[i].sprite = game.add.sprite(enemyBots[i].X*tile_size, enemyBots[i].Y*tile_size, 'enemyBot');
        sprite.animations.add('fly', [0, 1], 1.5, true);
        sprite.animations.play("fly");
    }
}

function initializeCode(bot){
    var code = localStorage.getItem('bot' + bot.Id);

    if (typeof code != 'undefined'){
        bot.Code = code;
    }
}