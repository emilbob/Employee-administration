$(document).ready(function () {


    var zaposleniEndPoint = "/api/zaposleni/";
    var host = window.location.host;
    var token = sessionStorage.getItem("token");
    var headers = {};

    $("#info2").append("Korisnik nije prijavljen na sistem").css("display", "block");
    $("#regpri").css("display", "none");
    $("#pocetak").css("display", "none");
    $("#odjava").css("display", "none");
    $("#pretraga").css("display", "none");
    $("#izmena").css("display", "none");
    $("#dodavanje").css("display", "none");

    loadZaposleni();
    loadKompanije(); 

    //if (token != null) {

    //    filledUser(sessionStorage.getItem("username"));
    //}


    $("#btnregpri").click(function () {
        $("#regpri").css("display", "block");
        $("#btnregpri").css("display", "none");
        
        $("#info2").empty().append("Registracija i prijava korisnika");
    });



    function loadZaposleni() {
        var requestUrl = 'http://' + host + zaposleniEndPoint;
        $.getJSON(requestUrl, setZaposleni);
    };

    function setZaposleni(data, status) {

        var $container = $("#tablecont");
        $container.empty();

        if (status === "success") {

            var div = $("<div></div>");
            var h3 = $("<h3 >Zaposleni</h3>");
            div.append(h3);

            var table = $("<table class='table table-bordered'></table>");
            if (token) {
                var header = $("<thead ><tr><td>Ime i prezime</td><td>Godina Rodjenja</td><td>Godina Zaposlenja</td><td>Kompanija</td><td>Plata</td><td>Brisanje</td><td>Izmena</td></tr></thead>");
            } else {
                var header = $("<thead ><tr><td>Ime i prezime</td><td>Godina Rodjenja</td><td>Godina Zaposlenja</td><td>Kompanija</td><td>Plata</td></tr></thead>");
            }

            table.append(header);
            tbody = $("<tbody></tbody>");
            for (i = 0; i < data.length; i++) {

                var row = "<tr>";

                var displayData = "<td>" + data[i].ImePrezime + "</td><td>" + data[i].GodinaRodjenja + "</td><td>" + data[i].GodinaZaposlenja + "</td><td>" + data[i].Kompanija.Naziv + "</td><td>" + data[i].Plata + "</td>";

                var stringId = data[i].Id.toString();
                var displayDelete = "<td><button id=btnDelete class='btnDelete' name=" + stringId + ">Obrisi</button></td>";
                var displayEdit = "<td><button class='btnEdit' name=" + stringId + ">Izmeni</button></td>";

                if (token) {
                    row += displayData + displayDelete + displayEdit + "</tr>";
                } else {
                    row += displayData + "</tr>";
                }

                tbody.append(row);
            }
            table.append(tbody);

            div.append(table);

            $container.append(div);

            //btn edit

            $(".btnEdit").click(function (e) {
                console.log('edit event with id =  ', e.target.name);

                if (token) {
                    headers.Authorization = 'Bearer ' + token;
                }

                $.ajax({
                    type: "GET",
                    url: 'http://' + host + zaposleniEndPoint,
                    data: { id: e.target.name },
                    headers: headers

                }).done(function (data) {
                    console.log(data);
                    $("#izmenakompanije").val(data.KompanijaId);
                    $("#izmenaimena").val(data.ImePrezime);
                    $("#izmenagodrodjenja").val(data.GodinaRodjenja);
                    $("#izmenagodzaposlenja").val(data.GodinaZaposlenja);
                    $("#izmenaplate").val(data.Plata);
                    $("#editid").val(data.Id);
                });

            });

            //brisanje


            $(".btnDelete").click(function (e) {
                console.log('delete event with id =  ', e.target.name);

                var deleteID = this.name;

                if (token) {
                    headers.Authorization = 'Bearer ' + token;
                }

                $.ajax({
                    type: "DELETE",
                    url: 'http://' + host + zaposleniEndPoint + deleteID.toString(),
                    //data: { id: e.target.name },
                    headers: Headers
                }).done(function (data, status) {
                    console.log("Uspesno brisanje");
                    loadZaposleni();

                }).fail(function (data) {
                    console.log(data);
                });
            });


        }
        else {
            var div = $("<div></div>");
            var h1 = $("<h1>Greška prilikom preuzimanja zaposlenog!</h1>");
            div.append(h1);
            $container.append(div);
        }
    }


    // registracija i prijava 

    $("#regpri button").click(function (e) {
        e.preventDefault();
        if ($(this).attr("value") === "regist") {

            var email = $("#korime").val();
            var loz = $("#loz").val();
            var loz2 = $("#loz2").val();


            var sendData = {
                "Email": email,
                "Password": loz,
                "ConfirmPassword": loz
            };

            $.ajax({
                type: "POST",
                url: 'http://' + host + "/api/Account/Register",
                data: sendData

            }).done(function (data) {
                

                $('#regpri').trigger("reset");

            }).fail(function (data) {
                console.log(data);
            });
        }
        if ($(this).attr("value") === "prij") {

            var email2 = $("#korime").val();
            var loz = $("#loz").val();


            var sendData2 = {
                "grant_type": "password",
                "username": email2,
                "password": loz
            };

            $.ajax({
                "type": "POST",
                "url": 'http://' + host + "/Token",
                "data": sendData2

            }).done(function (data) {
                console.log(data);
                $("#info").empty().append("Prijavljen korisnik: " + data.userName);
                $("#info2").css("display", "none");
                $("#regpri").css("display", "none");
                $("#pocetak").css("display", "none");
                $("#odjava").css("display", "block");
                $("#pretraga").css("display", "block");
                $("#izmena").css("display", "block");
                $("#dodavanje").css("display", "block");
                token = data.access_token;
                loadZaposleni();

                //filledUser(data.userName);

                sessionStorage.setItem("token", token);
                sessionStorage.setItem("username", data.userName);

            }).fail(function (data) {
                console.log(data);
            });
        }
    });

    // odjava korisnika sa sistema
    $("#odjava").click(function () {
        token = null;
        headers = {};

        $("#info").append("Korisnik nije prijavljen na sistem").css("display", "block");
        $("#info2").empty().append("Korisnik nije prijavljen na sistem").css("display", "block");
        $("#info").css("display", "none");
        $("#btnregpri").css("display", "block");
        $("#regpri").css("display", "none");
        $("#pocetak").css("display", "none");
        $("#odjava").css("display", "none");
        $("#izmena").css("display", "none");
        $("#pretraga").css("display", "none");
        $("#dodavanje").css("display", "none");
        loadZaposleni();

    });

    //preatraga

    $("#pretraga").submit(function (e) {
        e.preventDefault();

        var pocetak = $("#od").val();
        var kraj = $("#do").val();



        var sendData = {
            "pocetak": pocetak,
            "kraj": kraj
        };

        if (token) {
            headers.Authorization = 'Bearer ' + token;
        }

        $.ajax({
            type: "GET",
            url: 'http://' + host + "/api/pretraga/",
            data: sendData,
            headers: headers
        }).done(function (data, status) {
            console.log(data);
            setZaposleni(data, status);
            $("#odjava").css("display", "none");
            $("#pocetak").css("display", "block");
        }).fail(function (data) {
            console.log(data);
        });
    });

    loadZaposleni();

    //dropdown

    function loadKompanije() {

        console.log("Load kompanije");
        var requestUrl = 'http://' + host + "/api/kompanije";
        $.getJSON(requestUrl, setKompanije);
    };

    function setKompanije(data, status) {
        if (status === "success") {

            for (i = 0; i < data.length; i++) {

                var option = "<option value=" + data[i].Id + ">" + data[i].Naziv + "</option>";


                //$("#dodklub").append(option);
                $("#izmenakompanije").append(option);
                $("#dodavanjekompanije").append(option);
            }


        }
    }

    //odustajanje

    $("#izmenaodustajanje").click(function () {
        $('#izmena').trigger("reset");
        $('#dodavanje').trigger("reset");
    });

    $("#izmenaodustajanje2").click(function () {
        
        $('#dodavanje').trigger("reset");
    });

    //pocetak

    $("#pocetak").click(function () {

        loadZaposleni();
        $("#pocetak").css("display", "none");
        $("#odjava").css("display", "block");
        $('#pretraga').trigger("reset");
    });




    //izmena


    $("#izmena").submit(function (e) {
        e.preventDefault();

        var kompanija = $("#izmenakompanije").val();
        var ime = $("#izmenaimena").val();
        var godrodjenja = $("#izmenagodrodjenja").val();
        var godzaposlenja = $("#izmenagodzaposlenja").val();
        var plata = $("#izmenaplate").val();
        var id = $("#editid").val();

        var editID = this.name;

        var sendData = {
            "KompanijaId": kompanija,
            "ImePrezime": ime,
            "GodinaRodjenja": godrodjenja,
            "GodinaZaposlenja": godzaposlenja,
            "Plata": plata,
            "Id": id
        };

        if (token) {
            headers.Authorization = 'Bearer ' + token;
        }

        $.ajax({
            type: "PUT",
            url: 'http://' + host + zaposleniEndPoint + id.toString(),
            data: sendData,
            headers: headers
        }).done(function (data, status) {
            console.log(data);
            loadZaposleni();
            $('#izmena').trigger("reset");
        }).fail(function (data) {
            console.log(data);
        });
    });

     //dodavanje

    $("#dodavanje").submit(function (e) {
        e.preventDefault();

        var kompanija = $("#dodavanjekompanije").val();
        var ime = $("#dodavanjeimena").val();
        var godrodjenja = $("#dodavanjegodrodjenja").val();
        var godzaposlenja = $("#dodavanjegodzaposlenja").val();
        var plata = $("#dodavanjeplate").val();
        var id = $("#dodajid").val();

        var sendData = {
            "KompanijaId": kompanija,
            "ImePrezime": ime,
            "GodinaRodjenja": godrodjenja,
            "GodinaZaposlenja": godzaposlenja,
            "Plata": plata,
            "Id": id
        };

        if (token) {
            headers.Authorization = 'Bearer ' + token;
        }

        $.ajax({
            type: "POST",
            url: 'http://' + host + "/api/zaposleni",
            data: sendData,
            headers: headers
        }).done(function (data, status) {
            console.log(data);
            loadZaposleni();
            $("#dodavanje").trigger("reset");
        }).fail(function (data) {
            console.log(data);
        });

    });


});