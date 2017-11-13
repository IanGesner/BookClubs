$(document).ready(function () {

    $.ajax({
        type: "GET",
        url: "/Users/NotificationCount",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            alert(response.responseText);
            //$("#addFriend").replaceWith("<p>Friend request sent.</p>")
        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
})