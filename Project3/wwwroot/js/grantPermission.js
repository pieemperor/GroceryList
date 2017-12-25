$(document).ready(function () {

    $(document).on('click', '#grantPermissionButton', function () {
        $("#grantPermissionArea").show(1000);
        $("#grantPermissionButton").hide(1000);
    });

    $(document).on('submit', '#grantPermissionForm', function (event) {
        var $form = $(this);

        $.ajax({
            url: $form.attr("action"),
            type: $form.attr("method"),
            data: $form.serialize(),
            success: function (response) {
                $("#grantPermissionArea").hide(1000);
                $("#grantPermissionButton").show(1000);
                console.log(response);
            },
            error: function (response) {
                console.log(response);
            }
        });

        event.preventDefault();
    });

    $("#grantPermissionArea").hide();

    $(document).on('click', '#cancelButton', function () {
        $("#grantPermissionArea").hide(1000);
        $("#grantPermissionButton").show(1000);
    });

    $(document).on('submit', '.revoke', function (event) {
        $form = $(this);

        $.ajax({
            url: "/GroceryList/Revoke",
            type: "POST",
            data: $form.serialize(),
            success: function (response) {
                console.log(response);
            },
            error: function (response) {
                console.log(response);
            }
        });
        event.preventDefault();
    });
});