define(["modules/game/ajax.module",
        "modules/game/run.module"], function (AjaxModule, RunModule) {
    "use strict"
    
    class CompilationModule {

        constructor(game, controller) {
            this.game = game;
            this.controller = controller;
        }

        get ajaxModule() {
            if (!this.__ajaxModule) {
                this.__ajaxModule = new AjaxModule();
            }
            return this.__ajaxModule;
        }

        get botsModule() {
            if (!this.__botsModule) {
                // due to issue with circular dependencies and its support by require.
                var BotsModule = require("modules/game/bots.module");

                this.__botsModule = new BotsModule(this.game, this.controller);
            }
            return this.__botsModule;
        }

        get runModule() {
            if (!this.__runModule) {
                this.__runModule = new RunModule(this.game, this.controller);
            }
            return this.__runModule;
        }

        compilationHandler() {
            var code = document.getElementById('code').value;
            console.log(this);
            var request = this.ajaxModule.sendPost("/Game/CompileBot", { code: code });
            var self = this;
            request.onreadystatechange = function () { // (3)
                if (request.readyState != 4) return;

                if (request.status != 200) {
                    alert(request.status + ': ' + request.statusText);
                } else {
                    self.processSuccessCompilationResult(request);
                }

            }
        }

        processSuccessCompilationResult(request) {
            var result = JSON.parse(request.responseText);
            
            if (result.IsCodeCorrect) {
                this.setCodeAsCorrect();
                this.highLightPanelAsCorrect();
            } else {
                this.setCodeAsIncorrect(result.Errors);
                this.highLightPanelAsIncorrect(result.Errors);
            }

            this.botsModule.saveErrorsState(this.controller.getSelectedBot());
        }

        runHandler() {
            console.log("run handler");
            this.runModule.hideUI();
            var selected = this.controller.getSelectedBot();
            this.botsModule.saveCode(selected);

            var code = [];

            console.log("pushing to bots");

            var bots = this.controller.getBots();
            for (var i = 0; i < bots.length; i++) {
                code.push(bots[i].Code);
            }

            console.log("Sending request");
            console.log(code);
            console.log(bots.length);
            var request = this.ajaxModule.sendPost("/Game/CompileBots", { Code: code, BotsCount: bots.length });

            var self = this;
            request.onreadystatechange = function () { // (3)
                if (request.readyState != 4) {
                    console.log('hmmm');
                    console.log(request);
                    return;
                }

                if (request.status != 200) {
                    alert(request.status + ': ' + request.statusText);
                } else {
                    var result = JSON.parse(request.responseText);
                    console.log("Request result");
                    console.log(result);
                    if (result.IsCodeCorrect) {
                        self.setCodeAsCorrect();
                        self.highLightPanelAsCorrect();
                        self.runModule.setReady();
                    } else {
                        self.setCodeAsIncorrect(result.Errors);
                        self.highLightPanelAsIncorrect(result.Errors);
                    }

                    self.botsModule.saveErrorsState(self.controller.getSelectedBot());
                }

            }
        }

        clearErrors() {
            var container = document.getElementById('errors');

            while (container.firstChild) {
                container.removeChild(container.firstChild);
            }
        }

        setCodeAsCorrect() {
            var bot = this.controller.getSelectedBot();
            bot.content.IsCodeCorrect = true;
            bot.content.Errors = [];
        }

        setCodeAsIncorrect(errors) {
            var bot = this.controller.getSelectedBot();
            bot.content.IsCodeCorrect = false;
            for (var i = 0; i < errors.length; i++) {
                bot.content.Errors.push(errors[i]);
            }
        }

        setCodeAsDefault() {
            var bot = this.controller.getSelectedBot();
            console.log(bot);
            bot.content.IsCodeCorrect = null;
            bot.content.Errors = [];
        }

        highLightPanelAsIncorrect(errors) {
            document.getElementById('statePanel').classList.remove('panel-default');
            document.getElementById('statePanel').classList.remove('panel-success');
            document.getElementById('statePanel').classList.add('panel-danger');

            this.clearErrors();

            this.fillErrorsSection(errors);
        }

        highLightPanelAsCorrect() {
            document.getElementById('statePanel').classList.remove('panel-default');
            document.getElementById('statePanel').classList.remove('panel-danger');
            document.getElementById('statePanel').classList.add('panel-success');

            this.clearErrors();

            this.fillErrorsSection([this.controller.getSelectedBot().content.Name + '\'s code is correct!']);
        }

        highLightPanelAsDefault() {
            document.getElementById('statePanel').classList.remove('panel-success');
            document.getElementById('statePanel').classList.remove('panel-danger');
            document.getElementById('statePanel').classList.add('panel-default');

            this.clearErrors();

            this.fillErrorsSection([this.controller.getSelectedBot().content.Name + '\'s code has not been compiled yet!']);
        }

        fillErrorsSection(errors) {
            var errorsNode = document.getElementById('errors');

            for (var i = 0; i < errors.length; i++) {
                var p = document.createElement('p');
                p.innerHTML = errors[i];
                errorsNode.appendChild(p)
            }
        }

    }

    return CompilationModule;

});