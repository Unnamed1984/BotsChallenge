requirejs.config({
    baseUrl: "/../Scripts",
    paths: {
        signalRHubs: "../signalr/hubs?noext"
    }
});

require(['pages/registration.page'], function (page) {
    page.initialize();
});
