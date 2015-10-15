﻿var settings = {
    apiServiceBaseUri: 'http://development.com:40678/',
};

var WebAPIClient = function () {
}() || {};

WebAPIClient.authExternalProvider = function (provider) {
    var redirectUri = location.protocol + '//' + location.host + '/authcomplete.html';

    var externalProviderUrl = settings.apiServiceBaseUri + "api/Auth/Login?provider=" + provider + "&returnUrl=" + redirectUri;
    window.location = externalProviderUrl;
    //var oauthWindow = window.open(externalProviderUrl, "Authenticate Account", "location=0,status=0,width=600,height=750");
};

WebAPIClient.displayAuthInfo = function () {
    var fragment = JSON.parse(sessionStorage.getItem('auth'));
    if (fragment) {
        $("#auth").text('AccessToken: ' + fragment.access_token + ', provider: ' + fragment.provider + ', name: ' + fragment.name);
    }
};

WebAPIClient.getValueWithAccessToken = function () {
    var fragment = JSON.parse(sessionStorage.getItem('auth'));

    $.ajax(settings.apiServiceBaseUri + 'api/Values', {
        beforeSend: function (xhr) {
            xhr.setRequestHeader('Authorization', 'Bearer ' + fragment.access_token);
        }
    }).done(function (data) {
        debugger;
    });


    //$.getJSON(settings.apiServiceBaseUri + 'api/Values', function (data) {
    //    debugger;
    //});
}