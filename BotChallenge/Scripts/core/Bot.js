/**
 * Created by Paul on 11.04.2017.
 */
"use strict"

class Bot{
    constructor(x, y, id){
        this.health = 100;
        this.x = x;
        this.y = y;
        this.sprite = null;
        this.id = id;
        this.code = '';
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