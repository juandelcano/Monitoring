
function uploadFiles(inputId) {
    var input = document.getElementById(inputId);
    var files = input.files;
    debugger;
    var formData = new FormData();
    formData.append("fileToUpload", files[0]);

    $.ajax(
        {
            url: "/KinerjaBank/UploadDataKinerja",
            data: formData,
            processData: false,
            contentType: false,
            type: "POST",
            success: function (data) {
                debugger;
                Swal.fire({
                    position: 'center',
                    type: 'success',
                    icon: 'success',
                    title: 'Berhasil',
                    text: 'Data monioting bank berhasil diunggah!'
                }).then((result) => {
                    window.location.href = window.location.href;
                })
            },
            error: function (data) {
                Swal.fire({
                    position: 'center',
                    type: 'error',
                    icon: 'error',
                    title: 'Gagal',
                    text: 'Terjadi kesalahan saat mengunggah data!'
                }).then((result) => {
                    window.location.href = window.location.href;
                })
            }
        }
    );
}


function setName(elementId) {
    var input = document.getElementById(elementId);
    var files = input.files;
    document.getElementById('custom-file-label').innerHTML = files[0].name;
    debugger;
    var formData = new FormData();
    formData.append("fileToUpload", files[0]);

    $.ajax(
        {
            url: "/KinerjaBank/ViewDataKinerja",
            data: formData,
            processData: false,
            contentType: false,
            type: "POST",
            success: function (data) {
                $('#table').removeAttr('hidden');
                $('#dataKinerja').DataTable({
                    data: data,
                    columns: [
                        {
                            data: "periode",
                            render: function (data) {
                                return data.slice(0, -12);
                            }
                        },
                        {
                            data: "sandiBank",
                        },
                        {
                            data: "kreditKol1",
                            render: function (data) {
                                return comafy(data);
                            }
                        },
                        {
                            data: "kreditKol2",
                            render: function (data) {
                                return comafy(data);
                            }
                        },
                        {
                            data: "kreditKol3",
                            render: function (data) {
                                return comafy(data);
                            }
                        },
                        {
                            data: "kreditKol4",
                            render: function (data) {
                                return comafy(data);
                            }
                        },
                        {
                            data: "kreditKol5",
                            render: function (data) {
                                return comafy(data);
                            }
                        },
                        {
                            data: "laba",
                            render: function (data) {
                                return comafy(data);
                            }
                        },
                        {
                            data: "modal",
                            render: function (data) {
                                return comafy(data);
                            }
                        },
                        {
                            data: "totalAset",
                            render: function (data) {
                                return comafy(data);
                            }
                        },
                        {
                            data: "atmr",
                            render: function (data) {
                                return comafy(data);
                            }
                        },
                        {
                            data: "bebanOperasional",
                            render: function (data) {
                                return comafy(data);
                            }
                        },
                        {
                            data: "pendapatanOperasional",
                            render: function (data) {
                                return comafy(data);
                            }
                        },
                        {
                            data: "danaPihakKetiga",
                            render: function (data) {
                                return comafy(data);
                            }
                        }
                    ]
                })
            },
            error: function (data) {
                Swal.fire({
                    position: 'center',
                    type: 'error',
                    icon: 'error',
                    title: 'Gagal',
                    text: 'Terjadi kesalahan saat memuat data!'
                }).then((result) => {
                    window.location.href = window.location.href;
                })
            }
        }
    );

}

function comafy(num) {
    var str = num.toString().split('.');
    if (str[0].length >= 5) {
        str[0] = str[0].replace(/(\d)(?=(\d{3})+$)/g, '$1,');
    }
    if (str[1] && str[1].length >= 5) {
        str[1] = str[1].replace(/(\d{3})/g, '$1 ');
    }
    return str.join('.');
}