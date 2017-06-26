require(["modules/registration/registration.module", "signalRHubs"], function (regModule) {
    console.log($.connection.registrationHub);

    document.getElementById("cancel-btn").onclick = regModule.cancelHandler;
    document.forms.regForm.addEventListener('submit', regModule.submitHandler);
});
