/**
 * Created by Paul on 11.04.2017.
 */
"use strict"

class Bot{
    constructor(x, y, id, health, name) {
        this.health = health;
        this.x = x;
        this.y = y;
        this.sprite = null;
        this.id = id;
        this.code = '';
        this.isCodeCorrect = undefined;
        this.errors = [];
        this.name = name;
    }

    get X(){
        return this.x;
    }

    set X(value){
        this.x = value;
    }

    get Y(){
        return this.y;
    }

    set Y(value){
        this.y = value;
    }

    get Id(){
        return this.id;
    }

    get Code(){
        return this.code;
    }

    set Code(value){
        this.code = value;
    }

    get IsCodeCorrect(){
        return this.isCodeCorrect;
    }

    set IsCodeCorrect(value) {
        this.isCodeCorrect = value;
    }

    get Errors(){
        return this.errors;
    }

    set Errors(value) {
        this.errors = value;
    }

    get Name() {
        return this.name;
    }

    set Name(value) {
        this.name = value;
    }

    move(newX, newY) {
        this.x = newX;
        this.y = newY;

        this.sprite.x = newX * 64;
        this.sprite.y = newY * 64;
    }

    shoot() {

    }



    // set Id(value){
    //     this.Id = value;
    // }

    decreaseHealth(value){
        var delta = health - value;
        if (delta <=0){
            this.health = 0;
        }
        else{
            this.health = delta;
        }
    }

    shoot(){

    }
}