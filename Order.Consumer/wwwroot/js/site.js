"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/invoice-hub").build();

connection.on("NewInvoice", function (invoice) {
    console.log(invoice);
});

connection.start().then(function () {
    console.log("connected!");
}).catch(function (err) {
    return console.error(err.toString());
});