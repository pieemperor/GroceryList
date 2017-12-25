$(document).ready(function () {

    $(document).on('click', '#addGroceryItem', function () {
        $("#addGrocery").show(1000);
        $("#addGroceryItem").hide(1000);
    });

    $(document).on('submit', '#addGroceryForm', function (event) {
        var $form = $(this);

        $.ajax({
            url: $form.attr("action"),
            type: $form.attr("method"),
            data: $form.serialize(),
            success: function (response) {
                $("#addGrocery").hide(1000);
                $("#addGroceryItem").show(1000);
                console.log(response);
            },
            error: function (response) {
                console.log(response);
            }
        });

        event.preventDefault();
    });

    $(document).on('submit', '.remove', function (event) {
        var $form = $(this);
        $.ajax({
            url: "/GroceryList/RemoveGrocery",
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

    $("#addGrocery").hide();

    $(document).on('click', '#cancelButton', function () {
        $("#addGrocery").hide(1000);
        $("#addGroceryItem").show(1000);
    });
});