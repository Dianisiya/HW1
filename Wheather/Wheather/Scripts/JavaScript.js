$("#but").click(function () {
    var days = $('input[name=radio]:checked').val();
    var city = $("option:selected").val();
    if (!city) city = document.getElementById('cityText').value;
    if(days==1)
    {
        $.get("http://localhost:31657/Home/GetWeatherNow?city=" + city, function (data) {
            $("#infa").html(`
                <h2>${new Date(data.dt * 1000)}</h2>
                <h2>${data.main.temp}</h2>
                <img src="http://openweathermap.org/img/w/${data.weather[0].icon}.png">
                `);
        })
    }
    if(days==3)
    {
        $.get("http://localhost:31657/Home/GetWeatherThreeDays?city=" + city, function (data) {
            console.log(data);
            var text = "";
            for (var i = 0; i < data.list.length; i++) {
                text += `<div>
                    <h2>${(new Date (data.list[i].dt * 1000))}</h2>
                    <h2>${data.list[i].main.temp}</h2>
                    <img src="http://openweathermap.org/img/w/${data.list[i].weather[0].icon}.png"> 
                    </div>`
            }
            $('#infa').html(text)
        });
    }
    if(days==7)
    {
        $.get("http://localhost:31657/Home/GetWeatherSevenDays?city=" + city, function (data) {
            console.log(data);
            var text = "";
            for (var i = 0; i < data.list.length; i++) {
                text += `<div>
                    <h2>${(new Date(data.list[i].dt * 1000))}</h2>
                    <h2>${data.list[i].main.temp}</h2>
                    <img src="http://openweathermap.org/img/w/${data.list[i].weather[0].icon}.png">
                    </div>`
            }
            $('#infa').html(text)
        });
    }
});

