$(document).ready(function () {
    var dialog, form;

    dialog = $("#dialog-form").dialog({
        autoOpen: false,
        width: 310,
        modal: true,
        buttons: {
            "Send Request": sendRequest,
            Cancel: function () {
                dialog.dialog("close");
            }
        },
        close: function () {
            form[0].reset();
        }
    });


    form = dialog.find("form").on("submit", function (event) {
        event.preventDefault();
        sendRequest();
    });

    function sendRequest() {
        var data = { id: $("#user-id").val(), message: $("#message").val() };

        $.ajax({
            type: "POST",
            url: "/Profiles/SendRequest",
            data: JSON.stringify(data),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                $(".friend-status").replaceWith('<p class="friend-status glyphicon-export">Friend request sent.</p>')
            },
            failure: function (response) {
                alert("sendRequest() failure: " + response.responseText);
            },
            error: function (response) {
                handleError(response);
            }
        });

        dialog.dialog("close");
    }

    function acceptRequest() {
        var data = { id: $("#user-id").val() };

        $.ajax({
            type: "POST",
            url: "/Profiles/AcceptRequest",
            data: JSON.stringify(data),
            contentType: "application/json; charset=utf-8",
            dataType: "text json",
            success: function (response) {
                var friendStatus = $(".friend-status");
                friendStatus.replaceWith('<span id="friends" class="friend-status">Friends</span>');
                friendStatus.append('<i class=glyphicon glyphicon-check></i>');
            },
            failure: function (response) {
                alert("acceptRequest() failure: " + response.responseText);
            },
            error: function (response) {
                handleError(response);
            }
        });
    }

    function openDialog() {
        $.ajax({
            type: "POST",
            async: true,
            url: "/Profiles/VerifyLoggedOn",
            contentType: "application/json; charset=utf-8",
            success: function (response) {
                dialog.dialog("open");
            },
            failure: function (response) {
                alert("verifyLoggedOn() failure: " + response.responseText);
            },
            error: function (response) {
                handleError(response);
            }
        });
    }

    function handleError(response)
    {
        if (response.status == 403) {
            var responseText = $.parseJSON(response.responseText);
            window.location = responseText.LogOnUrl;
        }
        else {
            alert("Error: " + response.responseText);
        }
    }

    $("#viewFriendsBtn").click(function () {
        $("#viewFriends").toggle();
    });

    $("#viewGroupsBtn").click(function () {
        $("#viewGroups").toggle();
    });

    $("#add-friend").click(function () {
        openDialog();
    });

    $("#accept-request").click(function () {
        acceptRequest();
    });
});