/**
 * Created by Paul on 11.04.2017.
 */
"use strict"

class BotsController{
    constructor(width, height){
        this.botsCollection = [];
        this.enemyBots = [];
        this.selectedBot = { content: null };
        this.init(width, height);
    }

    init(width, height){
        // here will be some AJAX methods
        for (var i=0; i<4; i++){
            var randX = getRandomInteger(0, width);
            var randY = getRandomInteger(0, height);
            this.botsCollection.push(new Bot(randX, randY, i));

            randX = getRandomInteger(0, width);
            randY = getRandomInteger(0, height);
            this.enemyBots.push(new Bot(randX, randY));
        }
    }
    
    getBots(){
        return this.botsCollection;
    }

    getEnemyBots(){
        return this.enemyBots;
    }

    getSelectedBot(){
        return this.selectedBot;
    }
}