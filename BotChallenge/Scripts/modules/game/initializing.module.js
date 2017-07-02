/**
 * Created by Paul on 14.04.2017.
 */

define(["modules/game/bots.module"], function (BotsModule) {
    "use strict"

    class InitializingModule {

        constructor(game, controller) {
            this.game = game;
            this.controller = controller;

            this.tile_size = 64;
        }

        get botsModule() {
            if (!this.__botsModule) {
                this.__botsModule = new BotsModule(this.game, this.controller);
            }
            return this.__botsModule;
        }

        createLayers(map) {
            // creating layers
            var ground = map.createLayer('Ground');
            //ground.resizeWorld();
            var obstacles = map.createLayer('Obstacles');
            var decorations = map.createLayer('Decorations');
            var collectorItems = map.createLayer('CollectorsItems');
        }

        createMap() {
            // tilemap using
            var map = this.game.add.tilemap('tilemap');
            map.addTilesetImage('BotTestTileset', 'tilesetimage', 64, 64);

            return map;
        }

        // TODO: refactor too long method
        initializeBots(map) {
            var bots = this.controller.getBots();

            // ahchor depends on camera
            for (var i = 0; i < bots.length; i++) {
                var sprite = bots[i].sprite = this.game.add.sprite(bots[i].X * this.tile_size, bots[i].Y * this.tile_size, 'bot');
                sprite.animations.add('selected', [1, 2, 3, 4], 4, true);
                sprite.inputEnabled = true;
                sprite.input.useHandCursor = true;
                var self = this;
                sprite.events.onInputDown.add(function (sprite, pointer) {
                    self.botsModule.onBotDown(sprite, pointer);
                });

                this.initializeCode(bots[i]);
                this.initializeErrors(bots[i]);

                // Click on list item
                var self = this;
                document.getElementById(bots[i].Id).onclick = function (e) {
                    // should change it when here is server side
                    var id = e.target.id;
                    var bots = self.controller.getBots();

                    for (var i = 0; i < bots.length; i++) {
                        if (bots[i].Id == id) {
                            self.botsModule.onBotDown(bots[i].sprite, null);
                        }
                    }
                };
            }

            this.game.camera.focusOn(bots[0].sprite);
            bots[0].sprite.animations.play('selected');
            this.controller.selectedBot.content = bots[0];

            this.botsModule.displayCode(bots[0]);
            this.botsModule.displayErrorsState(bots[0]);

            // Enemy bots
            var enemyBots = this.controller.getEnemyBots();

            for (var i = 0; i < enemyBots.length; i++) {
                var sprite = enemyBots[i].sprite = this.game.add.sprite(enemyBots[i].X * this.tile_size, enemyBots[i].Y * this.tile_size, 'enemyBot');
                sprite.animations.add('fly', [0, 1], 1.5, true);
                sprite.animations.play("fly");
            }
        }

        initializeCode(bot) {
            var code = localStorage.getItem('bot' + bot.Id);

            if (typeof code != 'undefined') {
                bot.Code = code;
            }
        }

        initializeErrors(bot) {
            var isCorrect = localStorage.getItem('bot_isCodeCorrect' + bot.Id, bot.Code);
            var errors = localStorage.getItem('bot_errors' + bot.Id);

            if (errors != undefined) {
                bot.isCorrect = isCorrect;
                bot.errors = JSON.parse(errors);
            }
        }
    };

    return InitializingModule;
});