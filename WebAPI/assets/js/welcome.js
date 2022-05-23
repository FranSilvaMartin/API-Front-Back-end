$(document).ready(function () {

    $.ajax({
        url: 'https://localhost:44367/User/All',
        type: 'get',
        contentType: "application/json; charset=utf-8",
        success: function (response) {
            let users = response
            users.forEach(user => {
                console.log(user)
                $("#hola").append('<tr>' + '<td>' + user.userName + '</td><td>' + user.email + '</td><td>' + user.dateBirth + '</td><td>' + user.password + '</td>' + '</tr>')
            });

        },
        error: function () {
            alert("error")
        }
    });
});