$(document).ready(function () {
    $(document).on('submit', '.checkOff', function (event) {
        $form = $(this);
        $.ajax({
            url: $form.attr("action"),
            type: $form.attr("method"),
            data: $form.serialize(),
            success: function (response) {
                console.log(response);
            },
            failure: function (response) {
                console.log(response);
            }
        });
        event.preventDefault();
    });

   $(".checkOff").on("change", "input:checkbox", function () {
       $(this).closest("form").submit();
   });

});