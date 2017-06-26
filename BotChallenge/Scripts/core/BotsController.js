/**
 * Created by Paul on 11.04.2017.
 */
define(["core/Bot"], function (Bot) {
    "use strict"

    class BotsController {
        constructor(width, height) {
            this.botsCollection = [];
            this.enemyBots = [];
            this.selectedBot = { content: null };
            this.gameState = null;
            this.label = '';
            this.init(width, height);
        }

        init(width, height) {
            // here will be some AJAX methods
            var model = window.model;

            var bots = model.FieldState.Bots[model.CurrentLogin];

            for (var i = 0; i < bots.length; i++) {
                this.botsCollection.push(new Bot(bots[i].X, bots[i].Y, bots[i].Id, bots[i].Health, bots[i].Name));
            }

            delete model.FieldState.Bots[model.CurrentLogin];

            var first;
            for (var m in model.FieldState.Bots) {
                if (m) {
                    first = m;
                    break;
                }
            }

            var enemyBotsCol = model.FieldState.Bots[first];

            for (var i = 0; i < enemyBotsCol.length; i++) {
                this.enemyBots.push(new Bot(enemyBotsCol[i].X, enemyBotsCol[i].Y, enemyBotsCol[i].Id, enemyBotsCol[i].Health, enemyBotsCol[i].Name));
            }

            delete window.model;
        }

        getBots() {
            return this.botsCollection;
        }

        getEnemyBots() {
            return this.enemyBots;
        }

        getSelectedBot() {
            return this.selectedBot;
        }

        getGameState() {
            return this.gameState;
        }

        setGameState(state) {
            this.gameState = state;
        }
    }
});