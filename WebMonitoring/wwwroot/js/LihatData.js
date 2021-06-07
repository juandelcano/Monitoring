
var table = null;
var dataSet = null;
var dataPeriode = null;

$(document).ready(function () {
    $.ajax(
        {
            url: "/KinerjaBank/GetAllKinerja",
            type: "GET",
            success: function (data) {
                debugger;
                if (dataPeriode != null) {
                    data = dataPeriode;
                }
                dataSet = data;
                table = $('#dataKinerja').DataTable({
                    data: data,
                    columns: [
                        {
                            data: "sandiBank"
                        },
                        {
                            data: "npl",
                            render: function (data, type, row, meta) {
                                return Math.round(data * 100).toFixed(2).toString() + "%";
                            }
                        },
                        {
                            data: "roe",
                            render: function (data) {
                                return Math.round(data * 100).toFixed(2).toString() + "%";
                            }
                        },
                        {
                            data: "roa",
                            render: function (data) {

                                return Math.round(data * 100).toFixed(2).toString() + "%";
                            }
                        },
                        {
                            data: "ldr",
                            render: function (data) {
                                return Math.round(data * 100).toFixed(2).toString() + "%";
                            }
                        },
                        {
                            data: "bopo",
                            render: function (data) {
                                return Math.round(data * 100).toFixed(2).toString() + "%";
                            }
                        },
                        {
                            data: "car",
                            render: function (data) {
                                return Math.round(data * 100).toFixed(2).toString() + "%";
                            }
                        },
                        {
                            data: "npl",
                            render: function (data) {
                                data = Math.round(data * 100).toFixed(2);
                                if (data > 5) {
                                    return '<button style="width: 100%; height: 100%;" class="btn btn-danger" disabled></button>'
                                }
                                else if (data <= 5) {
                                    return '<button style="width: 100%; height: 100%;" class="btn btn-success" disabled></button>';
                                }
                            }
                        },
                        {
                            "render": function (data, type, row, meta) {
                                var index = meta.row + meta.settings._iDisplayStart;
                                var roe = Math.round(dataSet[index].roe * 100).toFixed(2);
                                var roa = Math.round(dataSet[index].roa * 100).toFixed(2);
                                if (roe <= 5 || roa < 1) {
                                    return '<button style="width: 100%; height: 100%;" class="btn btn-danger" disabled></button>';
                                }
                                else if (roe > 5 && roa > 1) {
                                    return '<button style="width: 100%; height: 100%;" class="btn btn-success" disabled></button>';
                                }
                            }
                        },
                        {
                            data: "car",
                            render: function (data) {
                                data = Math.round(data * 100).toFixed(2);
                                if (data < 20) {
                                    return '<button style="width: 100%; height: 100%;" class="btn btn-danger" disabled></button>';
                                }
                                else if (data > 20) {
                                    return '<button style="width: 100%; height: 100%;" class="btn btn-success" disabled></button>';
                                }
                            }
                        },
                        {
                            "data": "bopo",
                            render: function (data) {
                                data = Math.round(data * 100).toFixed(2);
                                if (data > 80) {
                                    return '<button style="width: 100%; height: 100%;" class="btn btn-danger" disabled></button>';
                                }
                                else if (data < 80) {
                                    return '<button style="width: 100%; height: 100%;" class="btn btn-success" disabled></button>';
                                }
                            }
                        },
                        {
                            "data": "ldr",
                            render: function (data) {
                                data = Math.round(data * 100).toFixed(2);
                                if (data > 94 || data < 82) {
                                    return '<button style="width: 100%; height: 100%;" class="btn btn-danger" disabled></button>';
                                }
                                else if (data >= 82 && data <= 94) {
                                    return '<button style="width: 100%; height: 100%;" class="btn btn-success" disabled></button>';
                                }
                            }
                        },
                        {
                            "render": function (data, type, row, meta) {
                                var index = meta.row + meta.settings._iDisplayStart;
                                var merah = getMerah(dataSet[index]);
                                if (merah > 2) {
                                    return '<button style="width: 100%; height: 100%;" class="btn btn-danger" disabled></button>';
                                }
                                else if (merah == 2) {
                                    return '<button style="width: 100%; height: 100%;" class="btn btn-warning" disabled></button>';
                                }
                                else if (merah <= 1) {
                                    return '<button style="width: 100%; height: 100%;" class="btn btn-success" disabled></button>';
                                }
                                return "Coba";
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
                    text: 'Terjadi kesalahan saat mengunggah data!'
                }).then((result) => {
                    window.location.href = window.location.href;
                })
            }
        }
    );

});

function lihatData() {
    debugger;
    table.destroy();
    var periode = document.getElementById('periode').value.toString();
    $.ajax(
        {
            url: "/KinerjaBank/GetKinerjaByPeriode",
            data: { data: periode },
            type: "POST",
            success: function (data) {
                $('#table').removeAttr('hidden');
                table = $('#dataKinerja').DataTable({
                    data: data,
                    columns: [
                        {
                            data: "sandiBank"
                        },
                        {
                            data: "npl",
                            render: function (data, type, row, meta) {
                                return Math.round(data * 100).toFixed(2).toString() + "%";
                            }
                        },
                        {
                            data: "roe",
                            render: function (data) {
                                return Math.round(data * 100).toFixed(2).toString() + "%";
                            }
                        },
                        {
                            data: "roa",
                            render: function (data) {

                                return Math.round(data * 100).toFixed(2).toString() + "%";
                            }
                        },
                        {
                            data: "ldr",
                            render: function (data) {
                                return Math.round(data * 100).toFixed(2).toString() + "%";
                            }
                        },
                        {
                            data: "bopo",
                            render: function (data) {
                                return Math.round(data * 100).toFixed(2).toString() + "%";
                            }
                        },
                        {
                            data: "car",
                            render: function (data) {
                                return Math.round(data * 100).toFixed(2).toString() + "%";
                            }
                        },
                        {
                            data: "npl",
                            render: function (data) {
                                data = Math.round(data * 100).toFixed(2);
                                if (data > 5) {
                                    return '<button style="width: 100%; height: 100%;" class="btn btn-danger" disabled></button>'
                                }
                                else if (data <= 5) {
                                    return '<button style="width: 100%; height: 100%;" class="btn btn-success" disabled></button>';
                                }
                            }
                        },
                        {
                            "render": function (data, type, row, meta) {
                                var index = meta.row + meta.settings._iDisplayStart;
                                var roe = Math.round(dataSet[index].roe * 100).toFixed(2);
                                var roa = Math.round(dataSet[index].roa * 100).toFixed(2);
                                if (roe <= 5 || roa < 1) {
                                    return '<button style="width: 100%; height: 100%;" class="btn btn-danger" disabled></button>';
                                }
                                else if (roe > 5 && roa > 1) {
                                    return '<button style="width: 100%; height: 100%;" class="btn btn-success" disabled></button>';
                                }
                            }
                        },
                        {
                            data: "car",
                            render: function (data) {
                                data = Math.round(data * 100).toFixed(2);
                                if (data < 20) {
                                    return '<button style="width: 100%; height: 100%;" class="btn btn-danger" disabled></button>';
                                }
                                else if (data > 20) {
                                    return '<button style="width: 100%; height: 100%;" class="btn btn-success" disabled></button>';
                                }
                            }
                        },
                        {
                            "data": "bopo",
                            render: function (data) {
                                data = Math.round(data * 100).toFixed(2);
                                if (data > 80) {
                                    return '<button style="width: 100%; height: 100%;" class="btn btn-danger" disabled></button>';
                                }
                                else if (data < 80) {
                                    return '<button style="width: 100%; height: 100%;" class="btn btn-success" disabled></button>';
                                }
                            }
                        },
                        {
                            "data": "ldr",
                            render: function (data) {
                                data = Math.round(data * 100).toFixed(2);
                                if (data > 94 || data < 82) {
                                    return '<button style="width: 100%; height: 100%;" class="btn btn-danger" disabled></button>';
                                }
                                else if (data >= 82 && data <= 94) {
                                    return '<button style="width: 100%; height: 100%;" class="btn btn-success" disabled></button>';
                                }
                            }
                        },
                        {
                            "render": function (data, type, row, meta) {
                                var index = meta.row + meta.settings._iDisplayStart;
                                var merah = getMerah(dataSet[index]);
                                if (merah > 2) {
                                    return '<button style="width: 100%; height: 100%;" class="btn btn-danger" disabled></button>';
                                }
                                else if (merah == 2) {
                                    return '<button style="width: 100%; height: 100%;" class="btn btn-warning" disabled></button>';
                                }
                                else if (merah <= 1) {
                                    return '<button style="width: 100%; height: 100%;" class="btn btn-success" disabled></button>';
                                }
                                return "Coba";
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
                    text: 'Tidak ada data yang tersedia pada periode terpilih!'
                }).then((result) => {
                    window.location.href = window.location.href;
                })
            }
        }
    );
}

function getMerah(data) {
    var sum = 0;
    debugger;
    if (Math.round(data.npl * 100).toFixed(2) > 5) {
        sum++;
    }
    if (Math.round(data.roe * 100).toFixed(2) <= 5 || Math.round(data.roa * 100).toFixed(2) < 1) {
        sum++;
    }
    if (Math.round(data.car * 100).toFixed(2) < 20) {
        sum++;
    }
    if (Math.round(data.bopo * 100).toFixed(2) > 80) {
        sum++;
    }
    if (Math.round(data.ldr * 100).toFixed(2) > 94 || Math.round(data.ldr * 100).toFixed(2) < 82) {
        sum++;
    }
    console.log(sum);
    return sum;
}
