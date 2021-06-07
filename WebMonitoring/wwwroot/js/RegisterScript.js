
function register() {
    debugger;
    var fname = document.getElementById('fName').value;
    var lname = document.getElementById('lName').value;
    var password = document.getElementById('password').value;
    var email = document.getElementById('email').value;

    debugger;

    if (fname === '' || lname === '' || password === '' || email === '') {
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
        var User = {
            Email: email,
            FirstName: fname,
            LastName: lname,
            Password: password
        };
        $.ajax({
            url: "/Register/RegisterNewUser",
            type: "POST",
            data: User,
            dataType: "json",
            aysnc: false,
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                Swal.fire({
                    position: 'center',
                    type: 'success',
                    icon: 'success',
                    title: 'Berhasil',
                    text: 'Akun anda sudah berhasil ditambahkan!'
                }).then((result) => {

                })
            },
            error: function (data) {
                Swal.fire({
                    position: 'center',
                    type: 'error',
                    icon: 'error',
                    title: 'Gagal',
                    text: 'Terjadi kesalahan saat mendaftarkan akun anda!'
                }).then((result) => {

                })
            }
        })
    }
}