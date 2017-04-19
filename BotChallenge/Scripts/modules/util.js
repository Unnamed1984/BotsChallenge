/**
 * Created by Paul on 12.04.2017.
 */

function getRandomInteger(min, max) {
    var rand = min - 0.5 + Math.random() * (max - min + 1)
    rand = Math.round(rand);
    return rand;
}
