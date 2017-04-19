//$(function () {
//    // Ссылка на автоматически-сгенерированный прокси хаба
//    var game = $.connection.gameHub;

//    // Функция, вызываемая при подключении нового пользователя
//    game.client.onConnected = function (id, userName, allUsers) {
//    }
//    window.onload = function () {
//        document.getElementById('progress').className += " progress-bar-wide";
//    };

//    document.forms.regForm.addEventListener('submit', function (e) {
//        e.preventDefault();
//        game.client.goForWaiting = function () {
//            console.log('waiting');
//            document.getElementById('regForm').remove();
//            document.getElementById('waitingForm').classList.remove('display-none');
//        };
//    });

//    window.onload = function () {
//        document.getElementById('progress').className += " progress-bar-wide";
//    };
//});