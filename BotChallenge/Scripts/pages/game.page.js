require(["core/BotsController",
    "modules/game/initializing.module",
    "modules/game/camera.module",
    "modules/game/compilation.module",
    "modules/game/signalR.module",
    "lib/phaser.min"], function (BotsController, InitializingModule, CameraModule, CompilationModule, signalRInitialize) {
        "use strict";

        var game = new Phaser.Game(1200, 400, Phaser.CANVAS, 'canva',
            { preload: preload, create: create, update: update });

        var width = 31;
        var height = 12;
        var controller = new BotsController(width, height);
        var cursors;

        var initModule = new InitializingModule(game, controller);
        var cameraModule = new CameraModule(game, controller);
        var compilationModule = new CompilationModule(game, controller);

        // phaser's functions

        function preload() {
            game.load.image('tilesetimage', '/Content/img/tiles.png', 128);
            game.load.tilemap('tilemap', '/Content/levels/map1.json', null, Phaser.Tilemap.TILED_JSON);

            game.load.spritesheet('bot', '/Content/img/bot1.png', 64, 64);
            game.load.spritesheet('enemyBot', '/Content/img/bot2.png', 64, 64);
        };

        function create() {
            game.scale.scaleMode = Phaser.ScaleManager.SHOW_ALL;
            game.scale.pageAlignHorizontally = true;
            game.scale.pageAlignVertically = true;

            game.world.setBounds(0, 0, 2048, 848);

            var map = initModule.createMap(game);
            initModule.createLayers(map);

            initModule.initializeBots(map);
        };

        function update() {
            cameraModule.cameraTickHandler();
        };

        document.getElementById("compilationBtn").onclick = function() {
            compilationModule.compilationHandler();
        };

        document.getElementById("runBtn").onclick = function () {
            compilationModule.runHandler();
        };

        signalRInitialize(game, controller);
    });