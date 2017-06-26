/**
 * Created by Paul on 14.04.2017.
 */

define([], function () {
    "use strict"

    class InitializingModule {

        constructor(game, controller) {
            this.game = game;
            this.controller = controller;
        }

        this.createLayers = function createLayers(map) {
            // creating layers
            var ground = map.createLayer('Ground');
            //ground.resizeWorld();
            var obstacles = map.createLayer('Obstacles');
            var decorations = map.createLayer('Decorations');
            var collectorItems = map.createLayer('CollectorsItems');
        }

        this.createMap = function createMap() {
            // tilemap using
            var map = this.game.add.tilemap('tilemap');
            map.addTilesetImage('BotTestTileset', 'tilesetimage', 64, 64);

            return map;
        }

        // TODO: refactor too long method
        this.initializeBots = function initializeBots(map) {
            var bots = this.controller.getBots();

            // ahchor depends on camera
            for (var i = 0; i < bots.length; i++) {
                var sprite = bots[i].sprite = game.add.sprite(bots[i].X * tile_size, bots[i].Y * tile_size, 'bot');
                sprite.animations.add('selected', [1, 2, 3, 4], 4, true);
                sprite.inputEnabled = true;
                sprite.input.useHandCursor = true;
                sprite.events.onInputDown.add(onBotDown);
                initializeCode(bots[i]);
                initializeErrors(bots[i]);

                // Click on list item
                document.getElementById(bots[i].Id).onclick = function (e) {
                    // should change it when here is server side
                    var id = e.target.id;
                    var bots = controller.getBots();

                    for (var i = 0; i < bots.length; i++) {
                        if (bots[i].Id == id) {
                            onBotDown(bots[i].sprite, null);
                        }
                    }
                };
            }

            this.game.camera.focusOn(bots[0].sprite);
            bots[0].sprite.animations.play('selected');
            this.controller.selectedBot.content = bots[0];
            displayCode(bots[0]);
            displayErrorsState(bots[0]);

            // Enemy bots
            var enemyBots = this.controller.getEnemyBots();

            for (var i = 0; i < enemyBots.length; i++) {
                var sprite = enemyBots[i].sprite = game.add.sprite(enemyBots[i].X * tile_size, enemyBots[i].Y * tile_size, 'enemyBot');
                sprite.animations.add('fly', [0, 1], 1.5, true);
                sprite.animations.play("fly");
            }
        }

        this.initializeCode = function initializeCode(bot) {
            var code = localStorage.getItem('bot' + bot.Id);

            if (typeof code != 'undefined') {
                bot.Code = code;
            }
        }

        this.initializeErrors = function initializeErrors(bot) {
            var isCorrect = localStorage.getItem('bot_isCodeCorrect' + bot.Id, bot.Code);
            var errors = localStorage.getItem('bot_errors' + bot.Id);

            if (errors != undefined) {
                bot.isCorrect = isCorrect;
                bot.errors = JSON.parse(errors);
            }
        }
    };

});