$(document).ready(function () {
    var url = window.location.href;
    if (url.includes("dashboard")) {
        $('#homeMenu').addClass('active');
        $('#kinerjaMenu').removeClass('active');
    }
    else if (url.includes("kinerja")) {
        $('#homeMenu').removeClass('active');
        $('#kinerjaMenu').addClass('active');
    }
})