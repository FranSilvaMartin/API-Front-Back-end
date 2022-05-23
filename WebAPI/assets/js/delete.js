let username = "";
let email = "";
let password = "";
let dateBirth = "";

$(document).ready(function () {
    $(".search-icon").click(function () {
        username = $(".search-input").val().trim();

        if (document.getElementById("dato")) {
            document.getElementById("dato").remove();
        }

        if (username != "") {
            $.ajax({
                url: 'https://localhost:44367/User/Get/username?username=' + username,
                type: 'get',
                contentType: "application/json; charset=utf-8",
                success: function (response) {
                    if (response == undefined) {
                        $("#error").fadeIn();
                        $("#todo").fadeOut();
                    } else {
                        $("#todo").fadeIn();
                        $("#error").fadeOut();
                        username = response.userName;
                        email = response.email;
                        password = response.password.substring(0, 20) + '**********';
                        password2 = response.password;
                        dateBirth = response.dateBirth;
                        var div = document.createElement("div");
                        div.innerHTML = '<h3 class="mb-4 text-left">Data found</h3>' +
                            '<p class="ml-4 mb-2"><i class="fas fa-user-circle mr-2"></i> ' + username + '</p>' +
                            '<p class="ml-4 mb-2"><i class="fa fa-envelope mr-2"></i> ' + email + '</p>' +
                            '<p class="ml-4 mb-2"><i class="fab fa-expeditedssl"></i> ' + password + '</p>' +
                            '<p class="ml-4 mb-2"><i class="fab fa-safari"></i> ' + dateBirth + '</p>';
                        div.setAttribute("id", "dato");
                        var todo = document.getElementById("info");
                        todo.appendChild(div);
                        $("#botones").fadeIn();
                    }
                },
                error: function () {
                    $("#error").fadeIn();
                    $("#todo").fadeOut();
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

    $("#btn-borrar").click(function () {

        Swal.fire({
            title: 'Are you sure?',
            text: "You won't be able to revert this!",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Yes, delete it!'
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({
                    url: 'https://localhost:44367/User/Delete?username=' + username,
                    type: 'delete',
                    contentType: "application/json; charset=utf-8",
                    success: function (response) {
                        if (response) {
                            Swal.fire(
                                'Deleted!',
                                'User has been deleted.',
                                'success'
                            )
                            $("#todo").fadeOut();
                        }
                    },
                    error: function () {
                        $("#error").fadeIn();
                        $("#todo").fadeOut();
                    }
                });
            }
        })
    });

    $("#btn-modificar").click(function () {
        $("#buscador").fadeOut();
        $("#tablaModificar").fadeIn();
        $("#botones").fadeOut();
        let fecha = new Date(invertirCadena(dateBirth));
        let number = fecha.getFullYear();
        let mes = 0;
        let dia = 0;
        console.log(fecha.getMonth());
        if (fecha.getMonth() < 10) {
            mes = "0" + (fecha.getMonth() + 1);
        } else if (fecha.getMonth() == 11) {
            mes = 12;
        } else {
            mes = fecha.getMonth();
        }

        if (fecha.getDate() < 10) {
            dia = "0" + (fecha.getDate());
        } else {
            dia = fecha.getDate();
        }

        document.getElementById("tituloModificando").innerHTML += " (" + username + ")";
        document.getElementById("username").value = username;
        document.getElementById("email").value = email;
        document.getElementById("pwd").value = password2;
        document.getElementById("date").value = number + "-" + mes + "-" + dia;
    });

    $("#btn-cancelar").click(function () {
        $("#buscador").fadeIn();
        $("#tablaModificar").fadeOut();
        $("#botones").fadeIn();
    });

    $("#btn-actualizar").click(function () {
        let newUser = $("#username").val().trim();
        let newEmail = $("#email").val().trim();
        let newPassword = $("#pwd").val().trim();
        let newDateBirth = $("#date").val().trim();

        Swal.fire({
            title: 'Do you want to save the changes?',
            showDenyButton: true,
            showCancelButton: true,
            confirmButtonText: 'Save',
            denyButtonText: `Don't save`,
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({
                    url: 'https://localhost:44367/User/Update?currentUser=' + username,
                    type: 'post',
                    contentType: "application/json; charset=utf-8",
                    data: JSON.stringify({ userName: newUser, email: newEmail, password: newPassword, dateBirth: newDateBirth }),
                    success: function (response) {
                        if (response) {
                            Swal.fire('Saved!', '', 'success')
                            $("#tablaModificar").fadeOut();
                            $("#buscador").fadeIn();
                            $("#todo").fadeOut();
                        } else {
                            Swal.fire('Changes are not saved', '', 'info')
                        }
                    },
                    error: function () {
                        alert("Error");
                    }
                });
            } else if (result.isDenied) {
                Swal.fire('Changes are not saved', '', 'info')
            }
        })
    });
});

function invertirCadena(cad) {
    return cad.split("/").reverse().join("/");
}