/**
 * Created by Paul on 14.04.2017.
 */

function focusCameraOnTile(x, y){
    game.camera.focusOnXY(x*tile_size, y*tile_size);
}

function moveCamera(x, y){
    game.camera.x += x;
    game.camera.y += y;
}

function focusCameraOnSprite(sprite){
    game.camera.focusOn(sprite);
}

function cameraController(){
    if (cursors.left.isDown)
    {
        moveCamera(-5, 0);
    }
    else if (cursors.right.isDown)
    {
        moveCamera(5, 0);
    }

    if (cursors.up.isDown)
    {
        moveCamera(0, -5);
    }
    else if (cursors.down.isDown){
        moveCamera(0, 5);
    }
}