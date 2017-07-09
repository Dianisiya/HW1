/// <reference path="jquery-2.1.4.min.js" />
$("#but").click(function () {
    var days = $('input[name=radio]:checked').val();
    var city = $("option:selected").val();
    if (!city) city = document.getElementById('cityText').value;
    if(days==1) {
        $.get("http://localhost:31657/Home/GetWeatherNow?city=" + city,
            function(data) {
                $("#infa").html(data);
            });
    }
    if(days==3)
    {
        $.get("http://localhost:31657/Home/GetWeatherThreeDays?city=" + city,
            function (data) {
                $("#infa").html(data);
            });
    }
    if (days == 7) {
        $.get("http://localhost:31657/Home/GetWeatherSevenDays?city=" + city,
            function (data) {
                $("#infa").html(data);
            });
    }
});

