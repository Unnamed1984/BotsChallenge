/**
 * Created by Paul on 14.04.2017.
 */

define([], function () {
    "use strict";

    class CameraModule {

        constructor(game, controller) {
            this.game = game;
            this.controller = controller;
        }

        focusCameraOnTile(x, y) {
            this.game.camera.focusOnXY(x * tile_size, y * tile_size);
        }

        moveCamera(x, y) {
            this.game.camera.x += x;
            this.game.camera.y += y;
        }

        focusCameraOnSprite(sprite) {
            this.game.camera.focusOn(sprite);
        }

        cameraTickHandler() {
            var cursors = this.game.input.keyboard.createCursorKeys();
            if (this.controller.getGameState() == "ready") {
                return;
            }

            if (cursors.left.isDown) {
                this.moveCamera(-5, 0);
            }
            else if (cursors.right.isDown) {
                this.moveCamera(5, 0);
            }

            if (cursors.up.isDown) {
                this.moveCamera(0, -5);
            }
            else if (cursors.down.isDown) {
                this.moveCamera(0, 5);
            }
        }
    }

    return CameraModule;
});