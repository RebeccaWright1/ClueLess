j$(document).ready(function () {
    //$('form').on("click", ".button", function () {
    //    alert("clicked");
    //})

    //$("#SignInButton").click(function () {
    //    var formdata = {
    //        username: $form.find("input[name='username']").val(),
    //        password: $form.find("input[name='password']").val()
    //    }
    //    alert("Hit");
    //    $.ajax({
    //        type: "POST",
    //        url: "User/SignIn",
    //        data: formdata,
    //        datatype: "json",
    //        success: function (result) {
    //            alert("ok"); //or perform redirect
    //        },
    //        error: function (result) {
    //            alert("error")// or show error message
    //        }
    //    });
    //});



    $("#SignInButton").click(function () {
        alert("Hit");
        var username = "System";
        var password = "123qwer";
        var parameter = JSON.stringify({
            "username": "System",
            "password": "123qwer"
        });
        $.ajax({
            type: "POST",
            url: "api/User/SignIn",
            contentType: "application/json",
            data: parameter,
            dataType: "json",
            success: function (result) {
                alert("ok"); //or perform redirect
            },
            error: function (jqXHR, exception) {
                console.log(jqXHR.responseText);
                alert("error")// or show error message
            }
        });
        // Get some values from elements on the page:
        //var $form = $(this),
        //    username = $form.find("input[name='username']").val(),
        //    passwowrd = $form.find("input[name='password']").val(),
        //    url = "User/SignIn";//$form.attr("action");

        //// Send the data using post
        //var posting = $.post(url, { username: username, password: password });
    });

});



