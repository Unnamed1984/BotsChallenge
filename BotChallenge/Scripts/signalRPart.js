$(function () {
    // Ссылка на автоматически-сгенерированный прокси хаба
    var game = $.connection.gameHub;

    // Функция, вызываемая при подключении нового пользователя
    game.client.onConnected = function (id, userName, allUsers) {
    }
    window.onload = function () {
        document.getElementById('progress').className += " progress-bar-wide";
    };

    game.client.goForWaiting = function () {
        console.log('waiting');
        document.getElementById('regForm').remove();
        document.getElementById('waitingForm').classList.remove('display-none');
    };
    
    game.client.goForGame = function () {
        console.log('game is on');
        var element = document.getElementById('progress');
        element.className += " progress-bar-success";
        element.classList.remove("active");
        element.classList.remove("progress-bar-striped");
        element.textContent = "Ready";

        document.getElementById('cancel-btn').className += " display-none";

        setTimeout(function () {
            document.getElementById('waitingForm').remove();
            document.getElementById('game').classList.remove('display-none');
        }, 2000);
    };

    game.client.logOut = function () {
        sessionStorage.clear();
    };

    var login = sessionStorage.getItem('botsLogin');
    console.log(login);

    if (login) {
        $.connection.hub.start().done(function () {
            game.server.connect(login);
            document.getElementById('regForm').remove();
            document.getElementById('waitingForm').classList.remove('display-none');
        });
    }

    console.log(document.getElementById('cancel-btn'));
    document.getElementById('cancel-btn').onclick = function () {
        console.log('cancel');
        console.log(sessionStorage.getItem('botsLogin'));
        game.server.cancelWaiting(sessionStorage.getItem('botsLogin'));
    };

    document.forms.regForm.addEventListener('submit', function (e) {
        e.preventDefault();

        $.connection.hub.start().done(function () {
            var name = document.getElementById('login').value;

            sessionStorage.setItem('botsLogin', name);
            console.log(name);
            game.server.connect(name);
        });
    });
});