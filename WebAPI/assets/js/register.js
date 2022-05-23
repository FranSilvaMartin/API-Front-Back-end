$(document).ready(function () {
    $("#btn-register").click(function () {
        let username = $("#username").val().trim();
        let email = $("#email").val().trim();
        let password = $("#pwd").val().trim();
        let date = $("#date").val().trim();

        let message = 'The user ' + username + ' has been successfully created.';
        
        if (username != "" && email != "" && password != "" && date != "") {
            $.ajax({
                url: 'https://localhost:44367/User/Register',
                type: 'post',
                data: JSON.stringify({ Username: username, Email: email, Password: password, DateBirth: date }),
                contentType: "application/json; charset=utf-8",
                success: function (response) {
                    if (response) {
                        Swal.fire({
                            icon: 'success',
                            title: message,
                            showConfirmButton: true,
                            confirmButtonText: 'Accept'
                        })
                    } else {
                        Swal.fire({
                            title: 'User could not be created',
                            html:
                                '<b>Possible errors list</b><br>' +
                                '- User/email already exists <br>' +
                                '- User/email is not valid <br>' +
                                '- The password must contain 6 characters <br>' + 
                                '- Invalid date of birth',
                            icon: 'error',
                            confirmButtonText: 'Accept'
                        })
                    }
                },
                error: function () {
                    $("#error").fadeIn();
                    Swal.fire({
                        title: 'User could not be created',
                        html:
                            '<b>Possible errors list</b><br>' +
                            '- User/email already exists <br>' +
                            '- User/email is not valid <br>' +
                            '- The password must contain 6 characters <br>' + 
                            '- Invalid date of birth',
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

    $("#btn-back").click(function () {
        window.location = "../index.html";
    });
});