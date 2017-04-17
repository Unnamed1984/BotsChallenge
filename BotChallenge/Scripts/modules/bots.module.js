/**
 * Created by Paul on 14.04.2017.
 */

function onBotDown(sprite, pointer){
    sprite.animations.play("selected");

    focusCameraOnSprite(sprite);

    var selectedBot = controller.getSelectedBot();
    if (selectedBot.content != null){
        selectedBot.content.sprite.animations.stop();
        selectedBot.content.sprite.frame = 0;
        deselectListItem(selectedBot.content.Id);

        saveCode(selectedBot);
    }

    var bots = controller.getBots();

    for (var i=0; i<bots.length; i++){
        if (bots[i].sprite.renderOrderID == sprite.renderOrderID){
            selectedBot.content = bots[i];
            displayCode(selectedBot.content);
            displayErrorsState(selectedBot.content);
        }
    }

    selectListItem(selectedBot.content.id);

    selectedBot.content.sprite.animations.play('selected');


    // День Космонавтики!!!!!!!

    // setTimeout(function () {
    //     setInterval(function () {
    //         sprite.y -= 10;
    //     }, 100);
    // }, 1000);
}

function deselectListItem(id){
    document.getElementById(id).classList.remove('active');
}

function selectListItem(id){
    document.getElementById(
        id).classList.add('active');
}

function saveCode(selectedBot){
    selectedBot.content.Code = document.getElementById('code').value;

    localStorage.setItem('bot' + selectedBot.content.Id, selectedBot.content.Code);
}

function displayCode(selectedBot){
    document.getElementById('code').value = selectedBot.Code;
}

function saveErrorsState(selectedBot) {
    localStorage.setItem('bot_isCodeCorrect' + selectedBot.content.Id, selectedBot.content.IsCodeCorrect);
    localStorage.setItem('bot_errors' + selectedBot.content.Id, JSON.stringify(selectedBot.content.Errors));
}

function displayErrorsState(selectedBot) {
    var isCorrect = localStorage.getItem('bot_isCodeCorrect' + selectedBot.Id);
    var errors = JSON.parse(localStorage.getItem('bot_errors' + selectedBot.Id));

    switch (isCorrect) {
        case null:
            setCodeAsDefault();
            highLightPanelAsDefault();
            break;
        case 'false':
            setCodeAsIncorrect(errors);
            highLightPanelAsIncorrect(errors);
            break;
        case 'true':
            setCodeAsCorrect();
            highLightPanelAsCorrect();
            break;
    }
}

