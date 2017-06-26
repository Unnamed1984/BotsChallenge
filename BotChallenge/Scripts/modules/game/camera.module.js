/**
 * Created by Paul on 14.04.2017.
 */

define([], function () {
    "use strict";

    class CameraModule {

        this.focusCameraOnTile = function focusCameraOnTile(x, y) {
            game.camera.focusOnXY(x * tile_size, y * tile_size);
        }

        this.moveCamera = function moveCamera(x, y) {
            game.camera.x += x;
            game.camera.y += y;
        }

        this.focusCameraOnSprite = function focusCameraOnSprite(sprite) {
            game.camera.focusOn(sprite);
        }

        this.cameraTickHandler = cameraTickHandler() {
            if (controller.getGameState() == "ready") {
                return;
            }

            if (cursors.left.isDown) {
                moveCamera(-5, 0);
            }
            else if (cursors.right.isDown) {
                moveCamera(5, 0);
            }

            if (cursors.up.isDown) {
                moveCamera(0, -5);
            }
            else if (cursors.down.isDown) {
                moveCamera(0, 5);
            }
        }
    }

    return CameraModule;
});