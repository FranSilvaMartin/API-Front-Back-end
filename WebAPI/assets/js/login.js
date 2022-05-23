$(document).ready(function () {
    $("#btn-login").click(function () {
        let username = $("#username").val().trim();
        let password = $("#pwd").val().trim();

        console.log(username);
        console.log(password);

        if (username != "" && password != "") {
            $.ajax({
                url: 'https://localhost:44367/User/Login',
                type: 'post',
                data: JSON.stringify({ Username: username, Password: password }),
                contentType: "application/json; charset=utf-8",
                success: function (response) {
                    if (response) {
                        window.location = "/pages/welcome.html";
                    } else {
                        Swal.fire({
                            title: 'Incorrect login',
                            html:
                                'User/email does not exist or<br>' +
                                'password is incorrect<br>',
                            icon: 'error',
                            confirmButtonText: 'Accept'
                        })
                    }
                },
                error: function () {
                    Swal.fire({
                        title: 'Incorrect login',
                        html:
                        'User/email does not exist or<br>' +
                        'password is incorrect<br>',
                        icon: 'error',
                        confirmButtonText: 'Accept'
                    })
                }
            });
        } else {
            Swal.fire({
                title: 'Check fields',
                text: 'There can be no empty fields',
                icon: 'error',
                confirmButtonText: 'Accept'
            })
        }
    });

    $("#btn-register").click(function () {
        window.location = "/pages/register.html";
    });
});