var xValues = ["NPL", "ROE", "ROA", "LDR", "BOPO", "CAR"];
$(document).ready(function () {
    $.ajax(
        {
            url: "/KinerjaBank/GetAllKinerja",
            type: "GET",
            success: function (data) {
                var dataset = getDataset(data);
                drawchart(dataset);
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
})

function drawchart(dataset) {
    new Chart("chartPeriode", {
        type: "line",
        data: {
            labels: xValues,
            datasets: dataset
        },
        options: {
            legend: { display: false },
            title: {
                display: true,
                text: 'Chart Data Kinerja',
                fullSize: true
            }
        }
    })
}

function getDataset(data) {
    var labelList = [];
    var listData = [];
    for (var i = 0; i < data.length; i++) {
        var dataItem = [];
        labelList.push(data[i].sandiBank);
        dataItem.push(data[i].npl * 100);
        dataItem.push(data[i].roe * 100);
        dataItem.push(data[i].roa * 100);
        dataItem.push(data[i].ldr * 100);
        dataItem.push(data[i].bopo * 100);
        dataItem.push(data[i].car * 100);
        listData.push(dataItem);
    }
    var listDataset = [];
    for (var i = 0; i < listData.length; i++) {
        var randomColor = Math.floor(Math.random() * 16777215).toString(16);
        var itemSet = {
            label: labelList[i],
            data: listData[i],
            borderColor: '#' + randomColor,
            fill: true
        }
        listDataset.push(itemSet);
    }
    return listDataset;
}

function lihatData() {
    var periode = document.getElementById('periode').value.toString();
    $.ajax(
        {
            url: "/KinerjaBank/GetKinerjaByPeriode",
            data: { data: periode },
            type: "POST",
            success: function (data) {
                if (data != null) {
                    var dataset = getDataset(data);
                    drawchart(dataset);
                }
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