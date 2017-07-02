/**
 * Created by Paul on 14.04.2017.
 */

define(["modules/game/camera.module",
        "modules/game/compilation.module"], function (CameraModule, CompilationModule) {
    "use strict"

    class BotsModule {

        constructor(game, controller) {
            this.game = game;
            this.controller = controller;
        }

        get cameraModule() {
            if (!this.__cameraModule) {
                this.__cameraModule = new CameraModule(this.game, this.controller);
            }
            return this.__cameraModule;
        }

        get compilationModule() {
            if (!this.__compilationModule) {
                this.__compilationModule = new CompilationModule(this.game, this.controller);
            }
            return this.__compilationModule;
        }

        onBotDown(sprite, pointer) {
            sprite.animations.play("selected");

            this.cameraModule.focusCameraOnSprite(sprite);

            var selectedBot = this.controller.getSelectedBot();

            if (selectedBot.content != null) {
                selectedBot.content.sprite.animations.stop();
                selectedBot.content.sprite.frame = 0;
                this.deselectListItem(selectedBot.content.Id);

                this.saveCode(selectedBot);
            }

            var bots = this.controller.getBots();

            for (var i = 0; i < bots.length; i++) {
                if (bots[i].sprite.renderOrderID == sprite.renderOrderID) {
                    selectedBot.content = bots[i];
                    this.displayCode(selectedBot.content);
                    this.displayErrorsState(selectedBot.content);
                }
            }

            this.selectListItem(selectedBot.content.id);

            selectedBot.content.sprite.animations.play('selected');
        }

        deselectListItem(id) {
            document.getElementById(id).classList.remove('active');
        }

        selectListItem(id) {
            document.getElementById(id).classList.add('active');
        }

        saveCode(selectedBot) {
            selectedBot.content.Code = document.getElementById('code').value;

            localStorage.setItem('bot' + selectedBot.content.Id, selectedBot.content.Code);
        }

        displayCode(selectedBot) {
            document.getElementById('code').value = selectedBot.Code;
        }

        saveErrorsState(selectedBot) {
            localStorage.setItem('bot_isCodeCorrect' + selectedBot.content.Id, selectedBot.content.IsCodeCorrect);
            localStorage.setItem('bot_errors' + selectedBot.content.Id, JSON.stringify(selectedBot.content.Errors));
        }

        displayErrorsState(selectedBot) {
            var isCorrect = localStorage.getItem('bot_isCodeCorrect' + selectedBot.Id);
            var errors = JSON.parse(localStorage.getItem('bot_errors' + selectedBot.Id));

            switch (isCorrect) {
                case null:
                    this.compilationModule.setCodeAsDefault();
                    this.compilationModule.highLightPanelAsDefault();
                    break;
                case 'false':
                    this.compilationModule.setCodeAsIncorrect(errors);
                    this.compilationModule.highLightPanelAsIncorrect(errors);
                    break;
                case 'true':
                    this.compilationModule.setCodeAsCorrect();
                    this.compilationModule.highLightPanelAsCorrect();
                    break;
            }
        }
    }
    return BotsModule;
});

