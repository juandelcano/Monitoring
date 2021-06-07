$(document).ready(function () {

})

function login() {
    debugger;
    var User = new Object();

    User.Email = $('#email').val();
    User.Password = $('#password').val();
    if (email == '' || password == '') {
        Swal.fire({
            position: 'center',
            type: 'error',
            icon: 'error',
            title: 'Gagal',
            text: 'Ada form yang kosong!'
        }).then((result) => {

        })
    }
    else {
        $.ajax({
            url: "/Login/Login",
            type: "POST",
            data: User,
            aysnc: false,
            contentType: "application/json; charset=utf-8",
        }).then(function (data) {
            debugger;
            console.log(data);
        })
        //    success: function (data) {
        //        debugger;
        //        console.log(data);
        //        //window.location.href = '/kinerjabank/uploaddatakinerja'
        //    },
        //    error: function (data) {
        //        Swal.fire({
        //            position: 'center',
        //            type: 'error',
        //            icon: 'error',
        //            title: 'Gagal',
        //            text: 'Gagal masuk menggunakan akun ini!'
        //        }).then((result) => {

        //        })
        //    }
        //})
    }
}