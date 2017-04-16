/**
 * Created by Paul on 31.03.2017.
 */

// https://habrahabr.ru/post/236809/
// enemies = game.add.group();enemies.setAll('inputEnabled', true);enemies.setAll('input.useHandCursor', true);

"use strict";

var game = new Phaser.Game(1200, 400, Phaser.CANVAS, 'canva', { preload: preload, create: create, update: update });
var tile_size = 64;
var width = 31;
var height = 12;
var controller = new BotsController(width, height);
var cursors;

// phaser's functions

function preload() {
    game.load.image('tilesetimage','/Content/img/tiles.png', 128);
    game.load.tilemap('tilemap','/Content/levels/map1.json',null,Phaser.Tilemap.TILED_JSON);

    game.load.spritesheet('bot', '/Content/img/bot1.png', 64, 64);
    game.load.spritesheet('enemyBot', '/Content/img/bot2.png', 64, 64);
};

function create() {
    game.scale.scaleMode = Phaser.ScaleManager.SHOW_ALL;
    game.scale.pageAlignHorizontally = true;
    game.scale.pageAlignVertically = true;

    game.world.setBounds(0, 0, 2048, 848);

    var map = createMap(game);
    createLayers(map);

    // focusCameraOnTile(12, 4);
    cursors = game.input.keyboard.createCursorKeys();

    initializeBots(map);
};

function update() {
    cameraController();
};
