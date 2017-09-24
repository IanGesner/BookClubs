$(document).ready(function () {
    var dialog, form;

    dialog = $("#dialog-form").dialog({
        autoOpen: false,
        //height: 400,
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

        var data = { id: $("#userId").val(), message: $("#message").val() };

        $.ajax({
            type: "POST",
            url: "/Profiles/SendRequest",
            data: JSON.stringify(data),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                $("#addFriend").replaceWith("<p>Friend request sent.</p>")
            },
            failure: function (response) {
                alert(response.responseText);
            },
            error: function (response) {
                alert(response.responseText);
            }
        });

        dialog.dialog("close");
    }

    $("#viewFriendsBtn").click(function () {
        $("#viewFriends").toggle();
    });

    $("#viewGroupsBtn").click(function () {
        $("#viewGroups").toggle();
    });

    $("#addFriend").click(function () {
        dialog.dialog("open");
    });

});