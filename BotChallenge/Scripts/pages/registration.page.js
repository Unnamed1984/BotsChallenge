define(["modules/registration/registration.module", "signalRHubs"], function (regModule) {
    console.log($.connection.registrationHub);

    return {
        initialize: function () {
            document.getElementById("cancel-btn").onclick = regModule.cancelHandler;
            document.forms.regForm.addEventListener('submit', regModule.submitHandler);
        }
    };
});